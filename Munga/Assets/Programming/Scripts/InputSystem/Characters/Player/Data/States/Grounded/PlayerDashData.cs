using System;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField] [field: Range(1f, 3f)] public float SpeedModifier { get; private set; } = 2f;
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
        
        [field: Header("연속으로 간주되는 시간")]
        [field: SerializeField] [field: Range(0f, 2f)] public float TimeToBeConsideredConsecutive { get; private set; } = 1f;
        
        [field: Header("연속 대시 제한값")]
        [field: SerializeField] [field: Range(1, 10)] public int ConsecutiveDashesLimitAmount { get; private set; } = 2;
        
        
        // 값 변경함 range (0 ~ 5 ) 기본 : 1.75
        [field: Header("대시 제한 쿨다운")]
        [field: SerializeField] [field: Range(0f, 2f)] public float DashLimitReachedCooldown { get; private set; } = 1.5f;
    }
}