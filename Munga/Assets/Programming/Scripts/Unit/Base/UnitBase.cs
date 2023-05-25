using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public enum UnitType
{
    Character,
    Monster
}
public class UnitBase : MonoBehaviour
{
    private string _name;
    private int _attackDamage;
    private int _maxHp;

    private bool _canTakeDamaged; // 피격 가능상태

    public bool CanTakeDamaged
    {
        get { return _canTakeDamaged; }
        set
        {
            if (value != _canTakeDamaged)
                _canTakeDamaged = value;
        }
    }

    [SerializeField] protected BoxCollider[] _attackRangeList;

    public UnitBase(string name, int attackDamage, int maxHp)
    {
        _name = name;
        _attackDamage = attackDamage;
        _maxHp = maxHp;
    }

    protected virtual void Awake()
    {
        if (_attackRangeList == null)
        {
            Debug.Log("[UnitBase] 공격범위 설정이 되어있지 않아서 반환시킴");
            return;
        }

        Debug.Log("[UnitBase] 공격범위 설정 완료");
    }

    public virtual void Attack()
    {
    }

    public virtual void TakeDamage()
    {
    }
}