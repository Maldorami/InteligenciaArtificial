using System.Collections.Generic;
using UnityEngine;

public abstract class MinerWalkBaseActionNode : BT_ActionNode<Blackboard>
{
    Stack<Vector2> _path;
    Blackboard blackboard;

    protected bool reverse = false;

    public MinerWalkBaseActionNode(Blackboard blackboard) : base(blackboard) {
        _path = ObtenerRutaRocaACarrito(blackboard);
        this.blackboard = blackboard;
    }

    protected override void Awake()
    {
        _path = ObtenerRutaRocaACarrito(blackboard);
    }

    public override State OnUpdate()
    {
        if (_path.Count > 0)
        {
            Vector2 tmp = Vector2.MoveTowards(blackboard.minerMovement.transform.position, _path.Peek(), blackboard.minerMovement.movementSpeed * Time.deltaTime);
            blackboard.minerMovement.transform.position = new Vector3(tmp.x, tmp.y, -1);
            if (Vector2.Distance(blackboard.minerMovement.transform.position, _path.Peek()) == 0)
            {
                blackboard.minerMovement.transform.position = new Vector3(_path.Peek().x, _path.Peek().y, -1);
                _path.Pop();
            }

            if (_path.Count == 0)
            {
                return BT_Node<Blackboard>.State.True;
            }


            return BT_Node<Blackboard>.State.Processing;
        }

        return BT_Node<Blackboard>.State.False;
    }



    protected Stack<Vector2> ObtenerRutaRocaACarrito(Blackboard blackboard)
    {
        Stack<Vector2> path;
        if (reverse)
        {
            int tmp = blackboard.minerMovement.pathFinder.idInicio;
            blackboard.minerMovement.pathFinder.idInicio = blackboard.minerMovement.pathFinder.idLlegada;
            blackboard.minerMovement.pathFinder.idLlegada = tmp;

            blackboard.minerMovement.pathFinder.BuscarRutaEstrella();
            path = blackboard.minerMovement.pathFinder.ObtenerRuta();

            tmp = blackboard.minerMovement.pathFinder.idInicio;
            blackboard.minerMovement.pathFinder.idInicio = blackboard.minerMovement.pathFinder.idLlegada;
            blackboard.minerMovement.pathFinder.idLlegada = tmp;
        }
        else
        {
            blackboard.minerMovement.pathFinder.BuscarRutaEstrella();
            path = blackboard.minerMovement.pathFinder.ObtenerRuta();
        }        

        return path;
    }

}
