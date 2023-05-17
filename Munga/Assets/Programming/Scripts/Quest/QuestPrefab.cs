using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPrefab : MonoBehaviour
{
    [SerializeField] private QuestSO _quesetData;
    [HideInInspector] public Transform _questStartPos;

    private void Awake()
    {
        _questStartPos = this.transform;
    }
}
