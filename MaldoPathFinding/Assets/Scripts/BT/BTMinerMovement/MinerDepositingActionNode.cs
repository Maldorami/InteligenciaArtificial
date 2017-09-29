using UnityEngine;
public class MinerDepositingActionNode : BT_ActionNode<Blackboard>
{
    Blackboard blackboard;
    float timer = 0;

    public MinerDepositingActionNode(Blackboard blackboard) : base(blackboard) {
        this.blackboard = blackboard;
    }

    protected override void Awake()
    {
        timer = 0;
    }

    public override State OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > blackboard.minerMovement.depoSpeed)
        {
            blackboard.minerMovement.cantPiedrasEnMochila--;
            blackboard.minerMovement.piedrasEnElDeposito++;

            if (blackboard.minerMovement.piedrasEnElDeposito >= blackboard.minerMovement.objetivoPiedrasAObtener
                || blackboard.minerMovement.cantPiedrasEnMochila <= 0)
            {
                return BT_Node<Blackboard>.State.True;
            }

            timer = 0;
        }

        return BT_Node<Blackboard>.State.Processing;
    }
}
