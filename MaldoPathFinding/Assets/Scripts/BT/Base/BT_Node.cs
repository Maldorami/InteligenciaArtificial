public abstract class BT_Node<T> where T : class {
    
    public enum State
    {
        None,
        Processing,
        True,
        False
    }

    public T Blackboard;

    protected State currentState = State.None;
    protected State lastState = State.None;

    public BT_Node (T _blackboard)
    {
        Blackboard = _blackboard;
    }
    
    protected abstract void Reset();
    protected abstract void Awake();
    protected abstract void Sleep();
    protected abstract State OnUpdate();

    public State Update()
    {
        if (currentState == State.None)
        {
            Awake();
        }

        currentState = OnUpdate();
        lastState = currentState;

        if (currentState != State.Processing)
        {
            Sleep();
            Reset();
            currentState = State.None;
        }

        return lastState;
    }
}
