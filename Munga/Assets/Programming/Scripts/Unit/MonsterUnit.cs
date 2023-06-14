using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

public class MonsterUnit : UnitBase
{
   [SerializeField] private GameObject pinObject;
   private FloatingTextCreator TextCreator;
   public MonsterUnit(string name, int attackDamage, int maxHp) : base(name, attackDamage, maxHp)
    {
        // DataManager에서 받아올 수 있게 해야함
        name = "말파이트";
        attackDamage = 1;
        maxHp = 10;
    }
    
    private void OnEnable()
    {
        BattleManager.Instance.MonsterActive(this);
        pinObject.SetActive(false);
        TextCreator = this.GetComponent<FloatingTextCreator>();
    }
    
    public Transform GetPinObject()
    {
        return pinObject.transform;
    }
    
    public void PinActive(bool isActive)
    {
        if(isActive)
            pinObject.SetActive(true);
        else
            pinObject.SetActive(false);
    }
    public override void Attack()
    {
        base.Attack();
    }
    public override void TakeDamage()
    {
        base.TakeDamage();
        DebugManager.instance.Log("MonsterTakeDamage");
        TextCreator.Create(100);
    }
}
