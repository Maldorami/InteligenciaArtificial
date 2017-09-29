public class MinerWalkToRockActionNode : MinerWalkBaseActionNode
{
    Blackboard blackboard;

    public MinerWalkToRockActionNode(Blackboard blackboard) : base(blackboard) {
        reverse = false;
        this.blackboard = blackboard;
    }

    public override State OnUpdate()
    {
        return base.OnUpdate();
    }
}