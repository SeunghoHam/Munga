using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

public class MonsterUnit : UnitBase
{
    [SerializeField] private GameObject pinObject;
    private FloatingTextCreator TextCreator;

    public DamageType MonsterDamageType; // public 으로 인스펙터에서 지정
    //private int typeValue; // 현재 타입을 반환시킴

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
        if (isActive)
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
        TextCreator.Active(ReturnStateType(), MonsterDamageType, 100);
    }

    private StateType ReturnStateType()
    {
        // 현재 MonsterUnit의 타입과 Plyaer의 현재 타입에 따라서 다르게 보여질거
        DamageType characterType = BattleManager.Instance._characterUnit.currnetWeaponDamageType;

        // 타입 관련 계산식
        StateType stateType; // 현재 몬스터가 입을 데미지의 타입 
        if (MonsterDamageType.GetHashCode() == System.Enum.GetValues(typeof(DamageType)).Length - 1) // 현재 Wind 인 경우
        {
            if (characterType == 0)
            {
                // Weak
                stateType = StateType.Weak;
            }
            else if (characterType == MonsterDamageType - 1)
            {
                // Resist
                stateType = StateType.Resist;
            }
            else
            {
                // Normal
                stateType = StateType.Normal;
            }
        }
        else if (MonsterDamageType == 0) // Fire인 경우
        {
            if (characterType.GetHashCode() == Enum.GetValues(typeof(DamageType)).Length - 1)
            {
                // Resist
                stateType = StateType.Resist;
            }
            else if (characterType == MonsterDamageType + 1)
            {
                // Weak
                stateType = StateType.Weak;
            }
            else
            {
                // Normal
                stateType = StateType.Normal;
            }
        }
        else // Fire, Wind 가 아닌경우
        {
            if (characterType == MonsterDamageType + 1)
            {
                // Weak
                stateType = StateType.Weak;
            }
            else if (characterType == MonsterDamageType - 1)
            {
                // Resist
                stateType = StateType.Resist;
            }
            else
            {
                // Normal
                stateType = StateType.Normal;
            }
        }

        return stateType;
    }

    private bool ReturnState()
    {
        bool state = true;
        return state;
    }
}