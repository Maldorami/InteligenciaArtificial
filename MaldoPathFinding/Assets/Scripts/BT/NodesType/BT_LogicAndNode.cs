using UnityEngine;
abstract class BT_LogicAndNode<T> : BT_NodeWithChild<T> where T : class {

    protected BT_LogicAndNode(T blackboard) : base(blackboard){ }

    override protected State OnUpdate()
    {
        if (childs.Count > 0)
        {
            for(int i = 0; i < childs.Count; i++)
            {
                if(childs[i].Update() == BT_Node<T>.State.False)
                {
                    return BT_Node<T>.State.False;
                }
            }
            return BT_Node<T>.State.True;
        }
        else
        {
            Debug.LogError("<color=red>BT_LogicAndNode fail!: not found childs</color>");
            return State.False;
        }
    }

    protected override void Awake() { }
    protected override void Reset() { }
    protected override void Sleep() { }

    public override bool CanAddChild()
    {
        return !(childs.Count >= 2);
    }


}
