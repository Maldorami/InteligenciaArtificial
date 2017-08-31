using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	Quieto = 0,
	CaminandoALaPiedra = 1,
	Minando = 2,
	CaminandoALaCarretilla = 3,
	Depositando = 4
};

public enum Event
{
	NuevoPedidoDePiedra = 0,
	Llegue = 1,
	MeLlene = 2,
	Termine = 3,
	MeVacie = 4
}

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
		StateMachine.Init(System.Enum.GetNames(typeof(State)).Length, System.Enum.GetNames(typeof(Event)).Length);

		StateMachine.SetRelation((int)State.CaminandoALaPiedra, (int)Event.Llegue, (int)State.Minando);
		StateMachine.SetRelation((int)State.Minando, (int)Event.MeLlene, (int)State.CaminandoALaCarretilla);
		StateMachine.SetRelation((int)State.Minando, (int)Event.Termine, (int)State.CaminandoALaCarretilla);
		StateMachine.SetRelation((int)State.Depositando, (int)Event.MeVacie, (int)State.CaminandoALaPiedra);
		StateMachine.SetRelation((int)State.Quieto, (int)Event.NuevoPedidoDePiedra, (int)State.CaminandoALaPiedra);
		StateMachine.SetRelation((int)State.CaminandoALaCarretilla, (int)Event.Llegue, (int)State.Depositando);
		StateMachine.SetRelation((int)State.Depositando, (int)Event.Termine, (int)State.Quieto);
    }

    void Update()
    {
		switch (StateMachine.GetState())
        {
		case (int)State.Quieto:
                //Nothing To Do
                break;

		case (int)State.CaminandoALaPiedra:
                Caminando();
                break;

		case (int)State.Minando:
                Minando();
                break;

		case (int)State.CaminandoALaCarretilla:
                Caminando();
                break;

		case (int)State.Depositando:
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
				StateMachine.SetEvent((int)Event.Llegue);
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
				StateMachine.SetEvent((int)Event.Termine);
            }else
            if (cantPiedrasEnMochila >= capacidadMochila)
            {
                cantPiedrasEnMochila = capacidadMochila;
					StateMachine.SetEvent((int)Event.MeLlene);
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

			if (piedrasEnElDeposito >= objetivoPiedrasAObtener)
			{
				piedrasEnElDeposito = objetivoPiedrasAObtener;
				StateMachine.SetEvent((int)Event.Termine); 
			}else
            if (cantPiedrasEnMochila <= 0)
            {
                cantPiedrasEnMochila = 0;
				StateMachine.SetEvent((int)Event.MeVacie);
            }

            

            timer = 0;
        }

    }

    public void NuevoPedidoDePiedras(int pedido)
    {
		StateMachine.SetEvent((int)Event.NuevoPedidoDePiedra);

        objetivoPiedrasAObtener = pedido;
        piedrasEnElDeposito = 0;
        cantPiedrasEnMochila = 0;

        GetPath();
    }

}