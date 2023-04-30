using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [CreateAssetMenu(fileName = "Player", menuName = "Create Player")]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
        [field: SerializeField] public PlayerAirborneData AirborneData { get; private set; }
    }
}