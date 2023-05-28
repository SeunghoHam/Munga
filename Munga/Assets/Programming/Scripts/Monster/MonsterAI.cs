using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    Monster mMonster;

    [SerializeField] float attackCoolTime;
    [SerializeField] float attackDistance;

    private void Awake()
    {
        mMonster = GetComponent<Monster>();
    }

    public bool IsDead()
    {
        return mMonster.IsDead;
    }

    public bool IsActioning()
    {
        return mMonster.IsActioning;
    }

    public bool CheckTarget()
    {
        return mMonster.target != null;
    }

    public bool CheckDoAttack()
    {
        return mMonster.CanAction;
    }

    public bool TargetInRange()
    {
        if (CheckTarget()) return false;

        return mMonster.DistanceToTarget < attackDistance;
    }
}
