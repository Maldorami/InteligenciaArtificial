using System.Collections;
using System.Collections.Generic;
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

    float timer = 0;
    public FSM StateMachine;
    Stack<Vector2> _path;


    void Start()
    {
        _path = new Stack<Vector2>();

        StateMachine = gameObject.GetComponent<FSM>();
        StateMachine.Init();
        StateMachine.SetRelation(State.CaminandoALaPiedra, Event.Llegue, State.Minando);
        StateMachine.SetRelation(State.Minando, Event.MeLlene, State.CaminandoALaCarretilla);
        StateMachine.SetRelation(State.Minando, Event.Termine, State.CaminandoALaCarretilla);
        StateMachine.SetRelation(State.Depositando, Event.MeVacie, State.CaminandoALaPiedra);
        StateMachine.SetRelation(State.Quieto, Event.NuevoPedidoDePiedra, State.CaminandoALaPiedra);
        StateMachine.SetRelation(State.CaminandoALaCarretilla, Event.Llegue, State.Depositando);
        StateMachine.SetRelation(State.Depositando, Event.Termine, State.Quieto);
    }

    void Update()
    {
        switch (StateMachine.GetState())
        {
            case State.Quieto:
                //Nothing To Do
                break;

            case State.CaminandoALaPiedra:
                Caminando();
                break;

            case State.Minando:
                Minando();
                break;

            case State.CaminandoALaCarretilla:
                Caminando();
                break;

            case State.Depositando:
                Depositando();
                break;

            default:
                //Nothing To Do
                break;
        }
    }

    public void GetPath()
    {
        pathFinder.BuscarRutaEstrella();
        _path = pathFinder.ObtenerRuta();
    }

    void SetReturnPath()
    {
        int tmp = pathFinder.idLlegada;
        pathFinder.idLlegada = pathFinder.idInicio;
        pathFinder.idInicio = tmp;
    }

    void Caminando()
    {
        if (_path.Count > 0)
        {
            Vector2 tmp = Vector2.MoveTowards(transform.position, _path.Peek(), movementSpeed * Time.deltaTime);
            transform.position = new Vector3(tmp.x, tmp.y, -1);
            if (Vector2.Distance(transform.position, _path.Peek()) == 0)
            {
                transform.position = new Vector3(_path.Peek().x, _path.Peek().y, -1);
                _path.Pop();
            }

            if (_path.Count == 0)
            {
                SetReturnPath();
                StateMachine.SetEvent(Event.Llegue);
                GetPath();
            }
        }
    }

    void Minando()
    {
        timer += Time.deltaTime;

        if (timer > miningSpeed)
        {
            cantPiedrasEnMochila++;

            if (cantPiedrasEnMochila + piedrasEnElDeposito >= objetivoPiedrasAObtener)
            {
                StateMachine.SetEvent(Event.Termine);
            }else
            if (cantPiedrasEnMochila >= capacidadMochila)
            {
                cantPiedrasEnMochila = capacidadMochila;
                StateMachine.SetEvent(Event.MeLlene);
            }

           

            timer = 0;
        }
    }

    void Depositando()
    {
        timer += Time.deltaTime;

        if (timer > depoSpeed)
        {
            cantPiedrasEnMochila--;
            piedrasEnElDeposito++;

            if (cantPiedrasEnMochila <= 0)
            {
                cantPiedrasEnMochila = 0;
                StateMachine.SetEvent(Event.MeVacie);
            }

            if (piedrasEnElDeposito >= objetivoPiedrasAObtener)
            {
                piedrasEnElDeposito = objetivoPiedrasAObtener;
                StateMachine.SetEvent(Event.Termine); 
            }

            timer = 0;
        }

    }

    public void NuevoPedidoDePiedras(int pedido)
    {
        StateMachine.SetEvent(Event.NuevoPedidoDePiedra);

        objetivoPiedrasAObtener = pedido;
        piedrasEnElDeposito = 0;
        cantPiedrasEnMochila = 0;

        GetPath();
    }

}