using UnityEngine;
public class FSM : MonoBehaviour
{
    int _currentState;
    int[,] _FSMMatrix;

	public void Init(int states, int events)
    {
		_FSMMatrix = new int[states, events];

		for (int i = 0; i < states; i++)
			for (int j = 0; j < events; j++) {
				_FSMMatrix [i, j] = -1;
			}

        _currentState = 0;
    }

    public void SetRelation(int origen, int evento, int destino)
    {
        _FSMMatrix[origen, evento] = destino;
    }

    public void SetEvent(int evento)
    {
		if(_FSMMatrix[_currentState, evento] != -1)
        _currentState = _FSMMatrix[_currentState, evento];
    }

    public int GetState()
    {
        return _currentState;
    }
}
