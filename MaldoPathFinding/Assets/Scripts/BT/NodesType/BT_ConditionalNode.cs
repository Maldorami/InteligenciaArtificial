using System;

public abstract class BT_ConditionalNode<T> : BT_Node<T> where T : class {

    protected BT_ConditionalNode(T blackboard) : base(blackboard){}

    override protected State OnUpdate()
    {
        if (Condition())
        {
            return State.True;
        }
        else
        {
            return State.False;
        }        
    }

    abstract protected bool Condition();

    protected override void Awake(){}
    protected override void Reset(){}
    protected override void Sleep(){}
}
