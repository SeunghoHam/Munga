using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerBasicAttackingState : PlayerAttackState
    {
        private int attackCount;
        
        public PlayerBasicAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(
            playerMovementStateMachine)
        {
            
        }

        public override void Enter()
        {
            StartAnimation(stateMachine.Player.AnimationData.BasicAttackParameterHash);
            
            base.Enter();

            BasicAttack();
        }
        public override void Exit()
        {
            StopAnimation(stateMachine.Player.AnimationData.BasicAttackParameterHash);
            
            base.Exit();
        }

        protected override void OnAttackStarted(InputAction.CallbackContext context)
        {
            base.OnAttackStarted(context);
            Debug.Log("먼가먼가 공격");
        }

        private void BasicAttack()
        {
            DebugManager.instance.Log("basicAttack 1", DebugManager.TextColor.Blue);
            if (attackCount == 0)
            {
                //DebugManager.instance.Log("basicAttack 1",DebugManager.TextColor.Blue);
            }
        }
    }
}