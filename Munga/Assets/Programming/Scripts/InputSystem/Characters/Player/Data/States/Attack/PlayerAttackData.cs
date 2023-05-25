using UnityEngine;
using System;

namespace GenshinImpactMovementSystem
{
    public class PlayerAttackData
    {
        [field: SerializeField]
        [field: Range(1f, 3f)]
        public float BasicDamage { get; private set; } = 2f;

        // 연속공격 가능의 시간(이 시간 지나면 공격 초기화)
        public float TimeToBeConsiderConsecutive =1f;
        
        // 공격 쿨타임
        public float AttackLimitReachedCoolDown = 0.5f;
    }
}