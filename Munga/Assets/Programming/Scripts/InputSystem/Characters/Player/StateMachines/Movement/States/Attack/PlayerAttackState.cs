using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Assets.Scripts.Manager;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerAttackState : PlayerMovementState
    {
        private float startTime;
        private bool isMoving; // 이동중인상태에서 공격 상태에 할당되었는가

        private int consecutiveDashesUsed;

        private int attackCount;
        public PlayerAttackState(PlayerMovementStateMachine playerMovementStateMachine) : base(
            playerMovementStateMachine) { }
        
        public override void Enter()
        {
            base.Enter();
            
            attackCount = 0;
            StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
            BattleManager.Instance.TimeSlopStart();
        }

        public override void Exit()
        {
            base.Exit();
            
            SetBaseRotationData();
            stateMachine.ReusableData.MovementSpeedModifier = groundedData.WalkData.SpeedModifier;
            BattleManager.Instance.TimeSlopEnd();
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Float();
            RotateTowardsTargetRotation();
        }
        
        #region ::: 위치 고정을 위해서 GroundState 에 있는 내용 가져옴 :::
        private void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ResizableCapsuleCollider.CapsuleColliderData.Collider.bounds.center;
            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);
            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, stateMachine.Player.ResizableCapsuleCollider.SlopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);
                if (slopeSpeedModifier == 0f)
                    return;
                float distanceToFloatingPoint = stateMachine.Player.ResizableCapsuleCollider.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;
                if (distanceToFloatingPoint == 0f)
                    return;
                
                float amountToLift = distanceToFloatingPoint * stateMachine.Player.ResizableCapsuleCollider.SlopeData.StepReachForce - GetPlayerVerticalVelocity().y;
                Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
                stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }
        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            float slopeSpeedModifier = groundedData.SlopeSpeedAngles.Evaluate(angle);

            if (stateMachine.ReusableData.MovementOnSlopesSpeedModifier != slopeSpeedModifier)
            {
                stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;

                UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
            }

            return slopeSpeedModifier;
        }
        
        #endregion
        
        private void Dash()
        {
            Vector3 dashDirection = stateMachine.Player.transform.forward;

            dashDirection.y = 0f;
            
            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                dashDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
            }
            stateMachine.Player.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);
        }

        protected void StartAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, false);
        }
        
        
        #region ::: override :::

        protected override void OnAttackStarted(InputAction.CallbackContext context)
        {
            //DebugManager.instance.Log("OnStartAttack", DebugManager.TextColor.Yellow);
            switch (attackCount)
            {
                case 0:
                    //DebugManager.instance.Log("OnStartAttack case 0");
                    BattleManager.Instance.TimeSlopStart();
                    StartAnimation(stateMachine.Player.AnimationData.SecondAttackParameterHash);
                    attackCount++;
                    
                    break;
                case 1:
                    //DebugManager.instance.Log("OnStartAttack case 1");
                    BattleManager.Instance.TimeSlopStart();
                    StartAnimation(stateMachine.Player.AnimationData.ThirdAttackParameterHash);
                    attackCount++;
                    
                    break;
                default:
                    //DebugManager.instance.Log("OnStartAttack default");
                    break;
            }
        }

        public override void OnAnimationEnterEvent()
        {
            AttackMove();   
        }

        private void AttackMove()
        {
            stateMachine.Player.transform.DOLocalMove(
                PlayerVector(), 0.2f);
        }
        private Vector3 PlayerVector()
        {
            // 현재위치 + forward 방향으로 이동
            Vector3 returnVector = stateMachine.Player.transform.position +
                                   stateMachine.Player.transform.forward;
            return returnVector;
        }

        public override void OnAnimationTransitionEvent()
        {
            //DebugManager.instance.Log("Override Trans", DebugManager.TextColor.Blue);
            // 애니메이션이 끝나게 될 시점에서 실행됨.
            if (attackCount == 0)
            {
            }
            else if (attackCount == 1)
            {
                StopAnimation(stateMachine.Player.AnimationData.SecondAttackParameterHash);
            }
            else if (attackCount == 2)
            {
                StopAnimation(stateMachine.Player.AnimationData.SecondAttackParameterHash);
                StopAnimation(stateMachine.Player.AnimationData.ThirdAttackParameterHash);
            }
            StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
        
        #endregion
        
        /// <summary>  연속 공격 업데이트 </summary>
        private bool IsConsecutive()
        {
            return Time.time < startTime + attackData.TimeToBeConsiderConsecutive;
        }
    }
}