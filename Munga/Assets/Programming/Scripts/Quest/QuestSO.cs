using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Create QuestData")]
public class QuestSO : ScriptableObject
{
    [field: SerializeField] public QuestData QuestData { get; private set; }
}
