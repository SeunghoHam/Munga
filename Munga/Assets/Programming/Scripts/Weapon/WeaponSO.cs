using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField] public string WeaponName;
    public string WeaponInfo;
    //[field: SerializeField] public string WeaponName;

}
