public class MinerBuilderBT {
    
    public BT_SecuenceNode<Blackboard> CreateIA(Blackboard blackboard)
    {
        BT_SecuenceNode<Blackboard> RootNode = new BT_SecuenceNode<Blackboard>(blackboard);

        MinerPendingDeliveryConditionalNode pdCondNode = new MinerPendingDeliveryConditionalNode(blackboard);
        RootNode.AddChild(pdCondNode);

        BT_SecuenceNode<Blackboard> MiningSeqNode = new BT_SecuenceNode<Blackboard>(blackboard);
        MinerWalkToRockActionNode walkToRockANode = new MinerWalkToRockActionNode(blackboard);
        MinerMiningActionNode miningANode = new MinerMiningActionNode(blackboard);
        MiningSeqNode.AddChild(walkToRockANode);
        MiningSeqNode.AddChild(miningANode);
        RootNode.AddChild(MiningSeqNode);
        

        MinerWalkToMineCartActionNode walkToMineCartANode = new MinerWalkToMineCartActionNode(blackboard);
        MinerDepositingActionNode depoANode = new MinerDepositingActionNode(blackboard);
        RootNode.AddChild(walkToMineCartANode);
        RootNode.AddChild(depoANode);

        MinerFinishConditionalNode finishCondNode = new MinerFinishConditionalNode(blackboard);
        RootNode.AddChild(finishCondNode);

        return RootNode;
    }
    	
}
