using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NodeCreator;

public class TitanBT : BaseBT
{
    Titan mTitan;
    TitanAI mAI;

    protected override void Awake()
    {
        mTitan = GetComponent<Titan>();
        mAI = GetComponent<TitanAI>();
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
            CreateAction(mTitan.Idle);

        moveNode =
            CreateAction(mTitan.Move);

        actionNode =
            CreateSelector
            (
                CreateSequence
                (
                    CreateCondition(mAI.CheckTargetInMeleeStateRange),//타겟이 일정 거리 이내에 있는가
                    CreateSelector //근거리
                    (
                        CreateSequence
                        (
                            CreateCondition(mAI.CheckMeleeAttackDifficult), // 연속으로 근접공격 실패
                            CreateAction(mTitan.RangeAttack)
                        ),
                        CreateSequence
                        (
                            CreateCondition(mAI.CheckTargetInMeleeAttackRange), // 근접공격 범위 이내에 있는가
                            CreateSelector
                            (
                                CreateSequence
                                (
                                    CreateCondition(mAI.CheckDoCounter), //반격조건
                                    CreateAction(mTitan.CounterAttack)
                                ),
                                CreateAction(mTitan.Attack)
                            )
                        )
                    )
                ),
                CreateSelector //원거리
                        (
                            CreateSequence
                            (
                                CreateCondition(mAI.CheckRangeAttackDifficult), //돌진 조건
                                CreateAction(mTitan.DashAttack)
                            ),
                            CreateAction(mTitan.RangeAttack)
                        )
            );
        base.NodeInit();
    }

    protected override bool BehaviorCondition()
    {
        return mAI.CheckTarget();
    }

    protected override bool ActionCondition()
    {
        return mAI.CheckDoAttack();
    }

    protected override bool MoveCondition()
    {
        return !mAI.CheckTargetInMeleeAttackRange();
    }

}
