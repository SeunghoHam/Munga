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

    //���ǵ�
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
        //Ÿ�ٿ��� ���� �̻� �°�
        //�����̻� ���� ����?
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
        //Ÿ���� ���� ���� ���� �ִ°�
        //���� ���� Ƚ���� ���� �̻��ΰ�
        return false;
    }
    public bool CheckRangeAttackDifficult()
    {
        //Ÿ���� �� �� �ִ°��� �ִ°�
        //���� ���� Ƚ���� ���� �̻��ΰ�
        return false;
    }
}
