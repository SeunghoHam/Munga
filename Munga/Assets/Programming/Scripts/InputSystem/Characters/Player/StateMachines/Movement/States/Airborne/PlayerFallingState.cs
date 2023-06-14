using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private Vector3 playerPositionOnEnter;

        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            playerPositionOnEnter = stateMachine.Player.transform.position;

            ResetVerticalVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            DebugManager.instance.Log("FallingExit");
            StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        private void LimitVerticalVelocity()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();
            
            if (playerVerticalVelocity.y >= -airborneData.FallData.FallSpeedLimit)
            {
                return;
            }

            Vector3 limitedVelocityForce = new Vector3(0f, -airborneData.FallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

            stateMachine.Player.Rigidbody.AddForce(limitedVelocityForce, ForceMode.VelocityChange);
        }

        protected override void ResetSprintState()
        {
        }

        protected override void OnContactWithGround(Collider collider)
        {
            // 근데 이거 호출되는 위치가 GroundCheck가 아니지 않나?
            // Roll 이 빠지면서 조금 수정이 필요한가?
            float fallDistance = playerPositionOnEnter.y - stateMachine.Player.transform.position.y;
            if (fallDistance < airborneData.FallData.MinimumDistanceToBeConsideredHardFall)
            {
                DebugManager.instance.Log("[FallState] LightLanding ", DebugManager.TextColor.Pink);
                stateMachine.ChangeState(stateMachine.LightLandingState);
                return;
            }

            if (stateMachine.ReusableData.ShouldWalk && !stateMachine.ReusableData.ShouldSprint || stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                DebugManager.instance.Log("[FallState] HardLanding ", DebugManager.TextColor.Pink);
                stateMachine.ChangeState(stateMachine.HardLandingState);
                return;
            }
            DebugManager.instance.Log("[FallState] RollLanding ", DebugManager.TextColor.Pink);
            //stateMachine.ChangeState(stateMachine.RollingState);
        }
    }
}