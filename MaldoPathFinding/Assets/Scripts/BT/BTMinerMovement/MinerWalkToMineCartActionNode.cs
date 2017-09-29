public class MinerWalkToMineCartActionNode : MinerWalkBaseActionNode
{
    Blackboard blackboard;

    public MinerWalkToMineCartActionNode(Blackboard blackboard) : base(blackboard) {
        reverse = true;        
        this.blackboard = blackboard;
    }

    public override State OnUpdate()
    {
        return base.OnUpdate();
    }
}
