using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Quieto = 0,
    CaminandoALaPiedra,
    Minando,
    CaminandoALaCarretilla,
    Depositando
};

public enum Event
{
    NuevoPedidoDePiedra = 0,
    Llegue,
    MeLlene,
    Termine,
    MeVacie
}

public class FSM : MonoBehaviour
{
    State _currentState;
    int[,] _FSMMatrix;

    public void Init()
    {
        _FSMMatrix = new int[System.Enum.GetNames(typeof(State)).Length, System.Enum.GetNames(typeof(Event)).Length];
        _currentState = 0;
    }

    public void SetRelation(State origen, Event evento, State destino)
    {
        _FSMMatrix[(int)origen, (int)evento] = (int)destino;
    }

    public void SetEvent(Event evento)
    {
        _currentState = (State)_FSMMatrix[(int)_currentState, (int)evento];
    }

    public State GetState()
    {
        return _currentState;
    }
}
