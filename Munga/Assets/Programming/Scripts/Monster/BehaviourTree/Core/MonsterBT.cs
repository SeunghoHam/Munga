using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NodeCreator;

public class MonsterBT : BaseBT
{
    [SerializeField] Monster mMonster;
    [SerializeField] MonsterAI mAI;

    protected override void Awake()
    {
        mMonster = GetComponent<Monster>();
        mAI = GetComponent<MonsterAI>();

        base.Awake();
    }

    protected override void NodeInit()
    {
        allstopNode =
            CreateSelector
            (
                CreateCondition(mAI.IsDead),
                CreateCondition(mAI.IsActioning)
            );

        standbyNode =
            CreateSequence
            (
                CreateAction(mMonster.Idle),
                CreateAction(mMonster.SearchTarget)
            );

        moveNode =
            CreateAction(mMonster.Move);

        actionNode =
            CreateAction(mMonster.Attack);

        base.NodeInit();
    }
    protected override bool BehaviorCondition()
    {
        return mAI.CheckTarget();
    }

    protected override bool MoveCondition()
    {
        return true;
    }

    protected override bool ActionCondition()
    {
        return mAI.CheckDoAttack() && mAI.TargetInRange();
    }

}
