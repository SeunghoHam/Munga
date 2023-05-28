using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimEventsHandler : MonoBehaviour
{
    [SerializeField] Monster mMonster;
    void Awake()
    {
        if (mMonster == null)
            mMonster = this.gameObject.transform.parent.GetComponent<Monster>();
    }

    public void EvnetActionEnd()
    {
        mMonster.ActionEnd();
    }

    public void EventAttackHitCheck(int _number)
    {
        mMonster.AttackHitCheck(_number);
    }
}
