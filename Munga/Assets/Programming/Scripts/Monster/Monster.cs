using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public abstract class Monster : MonoBehaviour
{
    protected enum MonsterState
    {
        NONE,
        IDLE,
        MOVE,
        ACTION
    }

    public GameObject target;
    protected NavMeshAgent mAgent;
    protected Animator mAnimator;

    [SerializeField] protected List<Collider> attackRangeList = new List<Collider>();

    protected MonsterState state
    {
        get; private set;
    }

    public bool IsDead
    {
        get; protected set;
    }

    public bool IsActioning
    {
        get
        {
            return state == MonsterState.ACTION;
        }
    }

    public bool CanAction
    {
        get; protected set;
    }

    [Header("Status")]
    [SerializeField] protected int hp;
    [SerializeField] protected int power;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackCoolTime;

    public float DistanceToTarget
    {
        get
        {
            if (target == null) return 0;
            return Vector3.Distance(transform.position, target.transform.position);
        }
    }




    protected virtual void Awake()
    {
        if(mAgent == null)  mAgent = GetComponent<NavMeshAgent>();
        if (mAnimator == null) mAnimator = GetComponentInChildren<Animator>();
        Init();
    }

    protected virtual void Init()
    {
        CanAction = true;
        state = MonsterState.NONE;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    public virtual void Hit(int _damage)
    {
        hp -= _damage;
        if(hp <= 0)
        {
            hp = 0;
            Dead();
        }
    }

    protected virtual void Dead()
    {
        IsDead = true;
    }

    protected void ChangeState(MonsterState _state)
    {
        if (state == _state) return;

        state = _state;

        switch (state)
        {
            case MonsterState.NONE:
                {
                    StateNoneInit();
                    break;
                }
            case MonsterState.IDLE:
                {
                    StateIdleInit();
                    break;
                }
            case MonsterState.MOVE:
                {
                    StateMoveInit();
                    break;
                }
            case MonsterState.ACTION:
                {
                    StateActionInit();
                    break;
                }
        }
    }

    protected virtual void StateNoneInit()
    {
        mAgent.speed = 0;
    }

    protected virtual void StateIdleInit()
    {
        mAgent.speed = 0;
        mAnimator.SetBool("IsMove", false);
    }

    protected virtual void StateMoveInit()
    {
        mAgent.speed = speed;
        mAnimator.SetBool("IsMove", true);
    }

    protected virtual void StateActionInit()
    {
        mAgent.speed = 0;
        ActionStart();
    }

    #region Action

    public void ActionStart()
    {
        CanAction = false;
    }

    public void ActionEnd()
    {
        ChangeState(MonsterState.NONE);
        StartCoroutine(AttackCoolTimer());
    }

    IEnumerator AttackCoolTimer()
    {
        if(attackCoolTime == 0) yield break;
        yield return new WaitForSeconds(attackCoolTime);
        CanAction = true;
        yield break;
    }

    public void AttackHitCheck(int _number)
    {
        if (attackRangeList.Count <= _number) return;

        var hits = Physics.OverlapBox(attackRangeList[_number].transform.position, attackRangeList[_number].bounds.size * 0.5f);
        foreach (var target in hits)
        {
            DebugManager.instance.Log(target.name, DebugManager.TextColor.Pink);
            //성공시 실패카운트 초기화
        }
        //조건 넣어서 실패카운트
        //meleeAttackFailCount++;
    }


    public abstract void Idle();
    public abstract void Move();
    public abstract void Attack();

    #endregion

    public abstract void SearchTarget();
}
