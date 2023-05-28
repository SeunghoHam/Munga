using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerBasicAttackingState : PlayerAttackState
    {
        // AttackCount 에 따라서 다른 AnimatorParameterHash 재생되게 해야함
        private int attackCount;
        public PlayerBasicAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(
            playerMovementStateMachine)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
            
            StartAnimation(stateMachine.Player.AnimationData.FirstAttackParameterHash);
            
            //BasicAttack();
        }
        public override void Exit()
        {
            StopAttackAnimation();
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
                StartAnimation(stateMachine.Player.AnimationData.FirstAttackParameterHash);
                attackCount++;
            }
            else if (attackCount == 1)
            {
                StartAnimation(stateMachine.Player.AnimationData.SecondAttackParameterHash);
                attackCount++;
            }
            else if (attackCount == 2)
            {
                StartAnimation(stateMachine.Player.AnimationData.ThirdAttackParameterHash);
                attackCount++;
            }
        }

        private void StopAttackAnimation()
        {
            // attackCount == 0 일수가 있나? 없다.
            if (attackCount == 1)
            {
                StopAnimation(stateMachine.Player.AnimationData.FirstAttackParameterHash);
            }
            else if (attackCount == 2)
            {
                StopAnimation(stateMachine.Player.AnimationData.SecondAttackParameterHash);
            }
            else if (attackCount == 3)
            {
                StopAnimation(stateMachine.Player.AnimationData.ThirdAttackParameterHash);
            }
        }
    }
}