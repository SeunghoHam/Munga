using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

public class MonsterUnit : UnitBase
{
    public MonsterUnit(string name, int attackDamage, int maxHp) : base(name, attackDamage, maxHp)
    {
    }
    
    private void OnEnable()
    {
        //BattleManager.Instance.AddListMonsterUnit(this);
    }

    /// <summary>
    /// 이 오브텍트를 BattleManager의 _activeMonsterList 에서 Remoeve
    /// </summary>
    public void RemoveThisUnitInList()
    {
        BattleManager.Instance.RemoveMonsterUnit(this);
    }


    public override void Attack()
    {
        base.Attack();
    }
    public override void TakeDamage()
    {
        base.TakeDamage();
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }


}
