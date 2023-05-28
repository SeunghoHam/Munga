using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public SelectorNode(params INode[] _nodes) : base(_nodes)
    {

    }



    public override bool Run()
    {
        foreach (var n in nodeList)
        {
            if (n.Run())
            {
                return true;
            }
        }
        return false;
    }
}
