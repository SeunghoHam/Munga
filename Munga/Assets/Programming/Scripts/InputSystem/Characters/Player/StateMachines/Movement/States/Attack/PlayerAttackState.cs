using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerAttackState : PlayerMovementState
    {
        private float startTime;
        private bool isMoving; // 이동중인상태에서 공격 상태에 할당되었는가

        private int consecutiveDashesUsed;
        public PlayerAttackState(PlayerMovementStateMachine playerMovementStateMachine) : base(
            playerMovementStateMachine) { }
        
        public override void Enter()
        {
            base.Enter();
            StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
            
            //Debug.Log("state MovementSpeedModifier =  " + GetMovementSpeed());
            
            //stateMachine.Player.Rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            //stateMachine.Player.Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            
            //stateMachine.Player.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            
            //Dash();
            //isMoving = stateMachine.ReusableData.MovementInput != Vector2.zero;
            //UpdateConsecutiveDashes();
            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

            stateMachine.ReusableData.MovementSpeedModifier = groundedData.WalkData.SpeedModifier;

            
            stateMachine.Player.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            SetBaseRotationData();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Float();
            RotateTowardsTargetRotation();
        }
        private void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ResizableCapsuleCollider.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, stateMachine.Player.ResizableCapsuleCollider.SlopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

                if (slopeSpeedModifier == 0f)
                {
                    return;
                }

                float distanceToFloatingPoint = stateMachine.Player.ResizableCapsuleCollider.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f)
                {
                    return;
                }

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

        private void UpdateConsecutiveDashes()
        {
            if (!IsConsecutive())
            {
                consecutiveDashesUsed = 0;
            }

            ++consecutiveDashesUsed;

            if (consecutiveDashesUsed == groundedData.DashData.ConsecutiveDashesLimitAmount)
            {
                consecutiveDashesUsed = 0;

                stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, groundedData.DashData.DashLimitReachedCooldown);
            }
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

        public override void OnAnimationEnterEvent()
        {
            
            //stateMachine.ChangeState(stateMachine.SprintingState);
        }
        protected override void OnAttackStarted(InputAction.CallbackContext context)
        {
            //stateMachine.ChangeState(stateMachine.AttackState);
            DebugManager.instance.Log("OnSTtartAttack", DebugManager.TextColor.Yellow);
            
            
        }

        private void SpeedLock()
        {
            // 현재 속도가 있는 상태라면 현재 위치에 고정되게 하고싶음 
            DebugManager.instance.Log("현재 속도가 없는상태");
            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                //stateMachine.ChangeState(stateMachine.HardStoppingState);
                stateMachine.ReusableData.MovementSpeedModifier = 0f;
                DebugManager.instance.Log("현재 속도가 있어서 초기화 상태");
                return;
            }
        }
        public override void OnAnimationTransitionEvent()
        { 
            // AttackAnimation 끝날 때 실행됨
            //DebugManager.instance.Log("공격 Transition Event");
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
        #endregion
        /// <summary>  연속 공격 업데이트 </summary>
        private void UpdateConsecutiveAttacks()
        {
            
        }
        private bool IsConsecutive()
        {
            return Time.time < startTime + attackData.TimeToBeConsiderConsecutive;
        }
    }
}