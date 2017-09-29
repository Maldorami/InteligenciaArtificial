using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerMiningActionNode : BT_ActionNode<Blackboard>
{
    Blackboard blackboard;
    float timer = 0;

    public MinerMiningActionNode(Blackboard blackboard) : base(blackboard) {
        this.blackboard = blackboard;
    }

    protected override void Awake()
    {
        timer = 0;
    }

    public override State OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > blackboard.minerMovement.miningSpeed)
        {
            blackboard.minerMovement.cantPiedrasEnMochila++;

            if (blackboard.minerMovement.cantPiedrasEnMochila + blackboard.minerMovement.piedrasEnElDeposito >= blackboard.minerMovement.objetivoPiedrasAObtener
                || blackboard.minerMovement.cantPiedrasEnMochila >= blackboard.minerMovement.capacidadMochila)
            {
                return BT_Node<Blackboard>.State.True;
            }

            timer = 0;
        }

        return BT_Node<Blackboard>.State.Processing;
    }
}
