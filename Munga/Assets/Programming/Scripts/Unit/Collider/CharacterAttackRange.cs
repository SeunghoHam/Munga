using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CharacterAttackRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            BattleManager.Instance.AddListMonsterUnit(other.GetComponent<MonsterUnit>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            BattleManager.Instance.RemoveMonsterUnit(other.GetComponent<MonsterUnit>());
        }
    }
}
