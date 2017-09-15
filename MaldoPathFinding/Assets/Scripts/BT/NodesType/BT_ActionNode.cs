using System;

abstract class BT_ActionNode<T> : BT_Node<T> where T : class{

    protected BT_ActionNode(T blackboard) : base(blackboard) { }

    protected override void Awake() { }
    protected override void Reset() { }
    protected override void Sleep() { }

    protected override State OnUpdate()
    {
        return BT_Node<T>.State.True;
    }
}
