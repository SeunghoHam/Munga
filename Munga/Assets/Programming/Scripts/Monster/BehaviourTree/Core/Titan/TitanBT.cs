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
                    CreateCondition(mAI.CheckTargetInMeleeStateRange),//Ÿ���� ���� �Ÿ� �̳��� �ִ°�
                    CreateSelector //�ٰŸ�
                    (
                        CreateSequence
                        (
                            CreateCondition(mAI.CheckMeleeAttackDifficult), // �������� �������� ����
                            CreateAction(mTitan.RangeAttack)
                        ),
                        CreateSequence
                        (
                            CreateCondition(mAI.CheckTargetInMeleeAttackRange), // �������� ���� �̳��� �ִ°�
                            CreateSelector
                            (
                                CreateSequence
                                (
                                    CreateCondition(mAI.CheckDoCounter), //�ݰ�����
                                    CreateAction(mTitan.CounterAttack)
                                ),
                                CreateAction(mTitan.Attack)
                            )
                        )
                    )
                ),
                CreateSelector //���Ÿ�
                        (
                            CreateSequence
                            (
                                CreateCondition(mAI.CheckRangeAttackDifficult), //���� ����
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
