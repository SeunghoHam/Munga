using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public SequenceNode(params INode[] _nodes) : base(_nodes) 
    {

    }


    public override bool Run()
    {
        foreach(var n in nodeList)
        {
            if (!n.Run())
            {
                return false;
            }
        }
        return true;
    }
}
