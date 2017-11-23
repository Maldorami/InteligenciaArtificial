using UnityEngine;
using UnityEngine.UI;

public class nodosVisitadosText : MonoBehaviour {

    public PathFinder pathFinder;
    public Text nodosText;

    private void Update()
    {
        nodosText.text = "Nodos visitados: " + pathFinder.NodosVisitados.ToString();
    }
}
