public class MinerPendingDeliveryConditionalNode : BT_ConditionalNode<Blackboard>
{

    Blackboard blackboard;

    public MinerPendingDeliveryConditionalNode(Blackboard blackboard) : base(blackboard)
    {
        this.blackboard = blackboard;
    }

    protected override bool Condition()
    {
        return blackboard.minerMovement.pedidoPendiente;
    }
}
