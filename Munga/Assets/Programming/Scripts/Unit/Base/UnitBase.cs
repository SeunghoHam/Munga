using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType
{
    Character,
    Monster
}
public class UnitBase : MonoBehaviour
{
    // Info
    private string _name;

    // Damage
    private int _attackDamage;

    // HP
    private int _maxHp;
    private int _curHP;
    

    #region ::: State :::
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
    #endregion
    
    
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
        CanTakeDamaged = true;
    }

    public virtual void Attack()
    {
        
    }

    public virtual void TakeDamage()
    {
        // 감소 과정 필요
    }

    protected virtual void Dead()
    {
        // TakeDamage에서 호출되도록
    }
    

    protected  virtual void OnTriggerEnter(Collider other)
    {
        
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        
    }
}