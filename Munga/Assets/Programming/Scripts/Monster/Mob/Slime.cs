using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public override void Idle()
    {
        ChangeState(MonsterState.IDLE);
    }

    public override void Move()
    {
        ChangeState(MonsterState.MOVE);
        mAgent.destination = target.transform.position;
    }

    public override void Attack()
    {
        ChangeState(MonsterState.ACTION);
        mAnimator.SetTrigger("Attack");
    }

    public override void SearchTarget()
    {
        throw new System.NotImplementedException();
    }
}
