using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : LeafNode
{
    protected Action action;

    public ActionNode(Action _action)
    {
        action = _action;
    }

    public override bool Run()
    {
        action();
        return true;
    }
}
