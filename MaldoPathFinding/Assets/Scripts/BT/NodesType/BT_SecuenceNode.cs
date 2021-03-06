﻿using UnityEngine;
public class BT_SecuenceNode<T> : BT_NodeWithChild<T> where T : class{

    int curIndex;

    public BT_SecuenceNode(T blackboard) : base(blackboard){
        curIndex = 0;
    }


    override public State OnUpdate()
    {
        if (childs.Count > 0)
        {
            for (int i = curIndex; i < childs.Count; i++)
            {
                switch (childs[i].Update())
                {
                    case State.True:
                        {
                            curIndex++;
                            break;
                        }
                    case BT_Node<T>.State.False:
                        {
                            curIndex = 0;
                            return BT_Node<T>.State.False;
                        }
                    case BT_Node<T>.State.Processing:
                        {
                            curIndex = i;
                            return BT_Node<T>.State.Processing;
                        }
                    default:
                        {
                            Debug.LogError("<color=red>BT_SecuenceNode fail!: method 'Update' cant return State.None</color>");
                            return BT_Node<T>.State.False;
                        }
                }               
            }

            curIndex = 0;
            return BT_Node<T>.State.True;
        }
        else
        {
            Debug.LogError("<color=red>BT_SecuenceNode fail!: not found childs</color>");
            return State.False;
        }
    }

    protected override void Awake() { }
    protected override void Reset() { }
    protected override void Sleep() { }
}
