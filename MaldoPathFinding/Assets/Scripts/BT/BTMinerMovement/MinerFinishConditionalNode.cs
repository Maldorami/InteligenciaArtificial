public class MinerFinishConditionalNode : BT_ConditionalNode<Blackboard> {

    Blackboard blackboard;

    public MinerFinishConditionalNode(Blackboard blackboard) : base(blackboard)
    {
        this.blackboard = blackboard;
    }

    protected override bool Condition()
    {
        //  Si esta lleno devolvería TRUE
        if(blackboard.minerMovement.piedrasEnElDeposito >= blackboard.minerMovement.objetivoPiedrasAObtener)
        {
            blackboard.minerMovement.pedidoPendiente = false;
            return true;
        }

        return false;
    }
}
