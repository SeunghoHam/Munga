using System.Collections;
using System.Collections.Generic;
using GenshinImpactMovementSystem;
using UnityEngine;

public class PlayerAttackState : PlayerMovementState
{
    public PlayerAttackState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    
    
}
