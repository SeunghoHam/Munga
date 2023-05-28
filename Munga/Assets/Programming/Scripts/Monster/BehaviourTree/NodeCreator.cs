using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NodeCreator
{
    public static SelectorNode CreateSelector(params INode[] _nodes) => new SelectorNode(_nodes);
    public static SequenceNode CreateSequence(params INode[] _nodes) => new SequenceNode(_nodes);
    public static ActionNode CreateAction(Action _action) => new ActionNode(_action);
    public static ConditionNode CreateCondition(Func<bool> _condition) => new ConditionNode(_condition);

    //public static SequenceNode CreateIfSequence(Func<bool> _condition, params INode[] _nodes)
    //{
    //    if(_nodes.Length == 1)
    //    {
    //        return new SequenceNode(CreateCondition(_condition), _nodes[0]);
    //    }

    //    List<INode> nodeList = new List<INode>();

    //    nodeList.Add(CreateCondition(_condition));
    //    foreach(var n in _nodes)
    //    {
    //        nodeList.Add(n);
    //    }
        
    //    return new SequenceNode(nodeList.ToArray());
    //}

    
}
