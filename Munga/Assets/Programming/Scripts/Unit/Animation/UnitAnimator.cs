using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;


public class UnitAnimator : MonoBehaviour
{
    public UnitType unitType;
    
    public void AnimationDamagePoint()
    {
        // Called by Character Animator
        if (unitType == UnitType.Character)
        {
            BattleManager.Instance.CharacterAttack();
        }
        // Called by Monster Animator
        else if (unitType == UnitType.Monster)
        {
            BattleManager.Instance.MonsterAttack();
        }
        else
        {
            DebugManager.instance.Log("[Unit Animation] UnitType 설정이 잘못되었음");
        }
    }
}
