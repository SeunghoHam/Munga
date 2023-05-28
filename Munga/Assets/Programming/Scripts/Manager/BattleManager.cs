using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using GenshinImpactMovementSystem;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class BattleManager : UnitySingleton<BattleManager>
    {
        public CharacterUnit _characterUnit;
        
        public List<MonsterUnit> _activeMonsterList = new List<MonsterUnit>();
        public List<MonsterUnit> _canAttackMonsterList = new List<MonsterUnit>();

        public CameraSystem cameraSystem;
        
        public void CharacterAttack()
        {
            if (_canAttackMonsterList.Count == 0)
                return;
            DebugManager.instance.Log("타격 대상 있음", DebugManager.TextColor.Red);
            for (int i = 0; i < _canAttackMonsterList.Count; i++)
            {
                // 데미지를 입힐 수 있는 상태일 때만 타격하도록
                if(_canAttackMonsterList[i].CanTakeDamaged)
                    _canAttackMonsterList[i].TakeDamage();
            }
        }
        
        public void MonsterAttack()
        {
            DebugManager.instance.Log("몬스터가 공격했당");

            foreach(var monster in _activeMonsterList)
            {
                monster.GetComponent<Monster>().Attack();
            }

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
                //Debug.Log("이미 보유중인 Unit임 : " +unit.name);
                return;
            }
            else
            {
                _canAttackMonsterList.Add(unit);
                DebugManager.instance.Log("MonsterList Add : [" +unit.name + "] ListCount : " + _canAttackMonsterList.Count, DebugManager.TextColor.Blue);
            }
        }

        public void RemoveMonsterUnit(MonsterUnit unit)
        {
            if (_canAttackMonsterList.Contains(unit)) // 이미 unit 보유중
            {
                _canAttackMonsterList.Remove(unit);
                DebugManager.instance.Log("MonsterList Remove: [" +unit.name+"] ListCount : " + _canAttackMonsterList.Count, DebugManager.TextColor.Blue);
            }
            else
            {
                return;
            }
        }

        public void MonsterActive(MonsterUnit unit)
        {
            _activeMonsterList.Add(unit);
        }
        public void MonsterDisActive()
        {
            
        }
        
        private void UnitActiveCheck()
        {
            
        }
        
        
        #region ::: TimeSlop :::

        public void TimeSlopStart()
        {
            DebugManager.instance.Log("Slop Start", DebugManager.TextColor.Yellow);
            //Time.timeScale = 1f;
            cameraSystem.CameraAction( CameraActionType.Near);
        }

        public void TimeSlopEnd()
        {
            DebugManager.instance.Log("Slop End", DebugManager.TextColor.Yellow);
            //Time.timeScale = 1f;
            cameraSystem.CameraAction(CameraActionType.Original);
        }

        public void GetCameraSystem(CameraSystem system)
        {
            cameraSystem = system;
        }

        #endregion
    }
}