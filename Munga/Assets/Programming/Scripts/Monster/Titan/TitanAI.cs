using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAI : MonoBehaviour
{
    Titan mTitan;

    [SerializeField] float attackCoolTime;
    [SerializeField] float meleeAttackDistance;
    [SerializeField] float meleeStateDistance;

    private void Awake()
    {
        mTitan = GetComponent<Titan>();
    }

    //조건들
    public bool IsDead()
    {
        return mTitan.IsDead;
    }

    public bool IsActioning()
    {
        return mTitan.IsActioning;
    }

    public bool CheckTarget()
    {
        return mTitan.target != null;
    }

    public bool CheckDoAttack()
    {
        return mTitan.CanAction;
    }

    public bool CheckDoCounter()
    {
        //타겟에게 일정 이상 맞고
        //일정이상 공격 실패?
        return false;
    }

    public bool CheckTargetInMeleeStateRange()
    {
        return mTitan.DistanceToTarget < meleeStateDistance;
    }

    public bool CheckTargetInMeleeAttackRange()
    {
        return mTitan.DistanceToTarget < meleeAttackDistance;
    }

    public bool CheckMeleeAttackDifficult()
    {
        //타겟이 갈수 없는 곳에 있는가
        //공격 실패 횟수가 일정 이상인가
        return false;
    }
    public bool CheckRangeAttackDifficult()
    {
        //타겟이 갈 수 있는곳에 있는가
        //공격 실패 횟수가 일정 이상인가
        return false;
    }
}
