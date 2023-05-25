using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

public class CharacterUnit : UnitBase
{
    public CharacterUnit(string name, int attackDamage, int maxHp) : base(name, attackDamage, maxHp)
    {
        name = DataManager.Instance.UserData.userName;
        attackDamage = 1;
        maxHp = 10;
    }

    private BoxCollider _basicAttackRange;

    protected override void Awake()
    {
        _basicAttackRange = _attackRangeList[0];
    }
    
    public override void Attack()
    {
        base.Attack();
        //DebugManager.instance.Log("캐릭터 [공격]", DebugManager.TextColor.Blue);
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        //DebugManager.instance.Log("캐릭터 [피격]", DebugManager.TextColor.Blue);
    }
}
