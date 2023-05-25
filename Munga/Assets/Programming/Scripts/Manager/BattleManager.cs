using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.Manager
{
    public class BattleManager : UnitySingleton<BattleManager>
    {
        //public MonsterUnit[] _activeMonsterList;
        public CharacterUnit _characterUnit;
        public List<MonsterUnit> _activeMonsterList = new List<MonsterUnit>();
        public List<MonsterUnit> _canAttackMonsterList = new List<MonsterUnit>();

        public void CharacterAttack()
        {
            //if(_characterUnit.)
            DebugManager.instance.Log("캐릭터가 공격했당");
            if (_canAttackMonsterList.Count == 0)
                return;
            for (int i = 0; i < _canAttackMonsterList.Count; i++)
            {
                _canAttackMonsterList[i].TakeDamage();
            }
        }

        public void MonsterAttack()
        {
            DebugManager.instance.Log("몬스터가 공격했당");
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void UnInitialize()
        {
            base.UnInitialize();
        }


        public void AddListMonsterUnit(MonsterUnit unit)
        {
            if (_canAttackMonsterList.Contains(unit)) // 이미 unit 보유중
            {
                Debug.Log("이미 보유중인 Unit임 : " +unit.name);
                return;
            }
            else
            {
                _canAttackMonsterList.Add(unit);
                Debug.Log("MonsterList에 활성화 됨 : " +unit.name);
                Debug.Log("현재 개수 : " +_canAttackMonsterList.Count);
            }
        }

        public void RemoveMonsterUnit(MonsterUnit unit)
        {
            if (_canAttackMonsterList.Contains(unit)) // 이미 unit 보유중
            {
                return;
            }
            else
            {
                Debug.Log("MonsterList Remove: " +unit.name);
                _canAttackMonsterList.Remove(unit);
            }
        }

        public void MonsterActive()
        {
            
        }

        public void MonsterDisActive()
        {
            
        }
        
        private void UnitActiveCheck()
        {
            
        }
    }
}