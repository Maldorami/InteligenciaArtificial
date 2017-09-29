using UnityEngine;
public class BT_DecoratorNotNode<T> : BT_NodeWithChild<T> where T : class{

    protected BT_DecoratorNotNode(T blackboard) : base(blackboard){}

    override public State OnUpdate()
    {
        if(childs[0] != null)
        {
            switch (childs[0].Update())
            {
                case BT_Node<T>.State.False:
                    return BT_Node<T>.State.True;
                case BT_Node<T>.State.True:
                    return BT_Node<T>.State.False;
                case BT_Node<T>.State.Processing:
                    return BT_Node<T>.State.Processing;
                default:
                    return BT_Node<T>.State.None;
            }
        }
        else
        {
            Debug.LogError("<color=red>BT_DecoratorNotNode fail!: not found childs</color>");
            return State.False;
        }
    }

    protected override void Awake() { }
    protected override void Reset() { }
    protected override void Sleep() { }

    public override bool CanAddChild()
    {
        return !(childs.Count >= 1);
    }
}
