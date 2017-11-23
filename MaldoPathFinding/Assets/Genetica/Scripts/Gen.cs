using UnityEngine;

public enum Action
{
    Propulsor,
    GirarDerecha,
    GirarIzquierda
}

public struct Gen{
    
    public Action _action;
    public float _tiempo;

    [SerializeField]
    int probabilidadDeMutar;
    
	public void CrearGen() {

        switch(Random.Range(0, 2))
        {
            case 0: _action = Action.Propulsor;
                break;
            case 1: _action = Action.GirarDerecha;
                break;
            case 2: _action = Action.GirarIzquierda;
                break;
        }

        _tiempo = Random.Range(0.2f, 1.0f);
        probabilidadDeMutar = 1;
	}
	
    public void IntentarMutar()
    {
        int aux = Random.Range(0, 100);

        if (aux <= probabilidadDeMutar)
        {
            Debug.Log("<color=red>MUTÓ!</color>");

            if (Random.value == 1)
                _tiempo += 0.1f;
            else
                _tiempo -= 0.1f;
        }

        if (_tiempo <= 0f) _tiempo = 0.1f;
    }


}
