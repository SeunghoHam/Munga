using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NodeCreator;

public abstract class BaseBT : MonoBehaviour
{
    protected INode rootNode;


    protected INode allstopNode;
    protected INode standbyNode;
    protected INode moveNode;
    protected INode actionNode;


    protected virtual void Awake()
    {
        NodeInit();
    }

    protected virtual void Update()
    {
        rootNode.Run();
    }

    protected virtual void NodeInit()
    {
        rootNode =
            CreateSelector
            (
                allstopNode,
                CreateSelector
                (
                    CreateSequence
                    (
                        CreateCondition(BehaviorCondition),
                        CreateSelector
                        (
                            CreateSequence
                            (
                                CreateCondition(ActionCondition),
                                actionNode
                            ),
                            CreateSequence
                            (
                                CreateCondition(MoveCondition),
                                moveNode
                            )
                        )
                    ),
                    standbyNode
                )
            );
    }

    protected abstract bool BehaviorCondition();
    protected abstract bool MoveCondition();
    protected abstract bool ActionCondition();

}
