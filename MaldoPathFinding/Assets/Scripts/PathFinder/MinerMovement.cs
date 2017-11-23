using UnityEngine;

public class MinerMovement : MonoBehaviour
{

    public PathFinder pathFinder;
    public float movementSpeed = 1;
    public int capacidadMochila = 1;
    public int cantPiedrasEnMochila = 0;
    public int objetivoPiedrasAObtener = 0;
    public int piedrasEnElDeposito = 0;
    public int miningSpeed = 1;
    public int depoSpeed = 1;

    public bool pedidoPendiente = false;

    Blackboard blackboard;
    MinerBuilderBT AIBuilder;
    BT_SecuenceNode<Blackboard> RootNode;

    void Start()
    {
        blackboard = new Blackboard();
        blackboard.minerMovement = this;

        AIBuilder = new MinerBuilderBT();
        RootNode = AIBuilder.CreateIA(blackboard);

    }

    private void Update()
    {
        RootNode.OnUpdate();
    }

    public void NuevoPedidoDePiedras(int pedido)
    {
        objetivoPiedrasAObtener = pedido;
        piedrasEnElDeposito = 0;
        cantPiedrasEnMochila = 0;
        pedidoPendiente = true;
    }

}