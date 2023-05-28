using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : IDecorator
{
    public Func<bool> condition;

    public ConditionNode(Func<bool> _condition)
    {
        condition = _condition;
    }

    public bool Run()
    {
        return condition();
    }

}
