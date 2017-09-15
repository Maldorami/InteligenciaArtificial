using System.Collections.Generic;

public abstract class BT_NodeWithChild<T> : BT_Node<T> where T : class {

    protected List<BT_Node<T>> childs;

    public BT_NodeWithChild(T blackboard) : base(blackboard)
    {
        childs = new List<BT_Node<T>>();
    }

    public virtual bool CanAddChild()
    {
        return true;
    }

    public void AddChild(BT_Node<T> child)
    {
        if (CanAddChild())
        {
            childs.Add(child);
        }
    }


}
