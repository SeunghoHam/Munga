using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : IComposite
{
    public List<INode> nodeList;

    public CompositeNode(params INode[] _nodes)
    {
        nodeList = new List<INode>(_nodes);
    }

    public abstract bool Run();
}
