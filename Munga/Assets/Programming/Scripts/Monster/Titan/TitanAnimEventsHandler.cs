using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAnimEventsHandler : MonoBehaviour
{
    [SerializeField] Titan mTitan;
    void Awake()
    {
        if (mTitan == null)
            mTitan = this.gameObject.transform.parent.GetComponent<Titan>();
    }


    public void EventRangeAttackShot()
    {
        mTitan.RangeAttackShot();
    }

    public void EventRangeAttackInst()
    {
        mTitan.RangeAttackInst();
    }
}
