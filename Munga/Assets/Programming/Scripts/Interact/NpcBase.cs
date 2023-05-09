using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class NpcBase : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private void Awake()
    {
        _boxCollider = this.GetComponent<BoxCollider>();
    }
}
