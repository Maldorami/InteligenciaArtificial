using UnityEngine;

abstract class BT_SecuenceOrNode<T> : BT_NodeWithChild<T> where T : class{

    int curIndex;

    protected BT_SecuenceOrNode(T blackboard) : base(blackboard){
        curIndex = 0;
    }


    override protected State OnUpdate()
    {
        if (childs.Count > 0)
        {
            for (int i = curIndex; i < childs.Count; i++)
            {
                switch (childs[i].Update())
                {
                    case State.True:
                        {
                            curIndex = 0;
                            return BT_Node<T>.State.True;
                        }
                    case BT_Node<T>.State.False:
                        {
                            curIndex++;
                            break;
                        }
                    case BT_Node<T>.State.Processing:
                        {
                            curIndex = i;
                            return BT_Node<T>.State.Processing;
                        }
                    default:
                        {
                            Debug.LogError("<color=red>BT_SecuenceOrNode fail!: method 'Update' cant return State.None</color>");
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

