using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

	List<Node> AllNodes;
	List<Node> NodosAbiertos;
	List<Node> NodosCerrados;

	public int idInicio;
	public int idLlegada;

    public int NodosVisitados = 0;


	void Start(){
		AllNodes = new List<Node> ();
		NodosAbiertos = new List<Node> ();
		NodosCerrados = new List<Node> ();
		GameObject[] tmp = GameObject.FindGameObjectsWithTag ("Node");

		for (int i = 0; i < tmp.Length; i++) {
			AllNodes.Add (tmp [i].GetComponent<Node> ());
            
		}

		for (int i = 0; i < AllNodes.Count; i++) {
			//AllNodes [i].id = i;
			AllNodes [i].AdyNodes = DevolverAdy (AllNodes [i]);
		}

		//BuscarRuta ();
	}


	public void BuscarRutaBreadthFirst(){
		ClearPath ();

		AbrirNodoInicial ();
		while (NodosAbiertos.Count >= 1) {
			Node node = NodosAbiertos [0];

			if (node.id == idLlegada) {
				ObtenerRuta ();
			}

			CerrarNodo (node);
			AbrirNodosAdy (node);
		}

		//return false;
	}

    public void BuscarRutaDijkstra()
    {
        ClearPath();

        AbrirNodoInicial();
        while (NodosAbiertos.Count >= 1)
        {
            Node node = NodosAbiertos[0];
            for (int i = 1; i < NodosAbiertos.Count; i++)
            {
                if (NodosAbiertos[i].pesoTotal < node.pesoTotal)
                    node = NodosAbiertos[i];
            }

            if (node.id == idLlegada)
            {
                ObtenerRuta();
            }

            CerrarNodoDijkstra(node);
            AbrirNodosAdyDijkstra(node);
        }

        //return false;
    }
    public void BuscarRutaEstrella()
    {
        ClearPath();

        int llegada = 0;
        for (int i = 0; i < AllNodes.Count; i++)
        {
            if (AllNodes[i].id == idLlegada)
            {
                llegada = i;
            }
        }

        for (int i = 0; i < AllNodes.Count; i++)
        {
            AllNodes[i].distanciaALaLlegada = Vector2.Distance(AllNodes[i].transform.position, AllNodes[llegada].transform.position);
        }


        AbrirNodoInicial();
        while (NodosAbiertos.Count >= 1)
        {
            Node node = NodosAbiertos[0];

			float distMenor = node.distanciaALaLlegada + node.pesoTotal;

            for (int i = 1; i < NodosAbiertos.Count; i++)
            {
				if (NodosAbiertos [i].distanciaALaLlegada + NodosAbiertos[i].pesoTotal  < distMenor) {
					distMenor = NodosAbiertos [i].distanciaALaLlegada + NodosAbiertos[i].pesoTotal;
					node = NodosAbiertos [i];
				}
            }
				

            if (node.id == idLlegada)
            {
                ObtenerRuta();
            }


            CerrarNodoDijkstra(node);
            AbrirNodosAdyDijkstra(node);
        }

        //return false;
    }

    public void BuscarRutaDepthFirst(){
		ClearPath ();

		AbrirNodoInicial ();
		while (NodosAbiertos.Count >= 1) {
			Node node = NodosAbiertos [NodosAbiertos.Count - 1];

			if (node.id == idLlegada) {
				ObtenerRuta ();
			}

			CerrarNodo (node);
			AbrirNodosAdy (node);
		}

		//return false;
	}

	public void ClearPath(){
		NodosCerrados.Clear ();
		NodosAbiertos.Clear ();
		for (int i = 0; i < AllNodes.Count; i++) {
			AllNodes [i].parteDelCamino = false;
		}

        NodosVisitados = 0;

    }
	public void ClearWalls(){
		NodosCerrados.Clear ();
		NodosAbiertos.Clear ();
		for (int i = 0; i < AllNodes.Count; i++) {
			AllNodes [i].isWall = false;
			AllNodes [i].peso = 1;
		}
	}


	public void cambiarPuntoDeSalida(int salida){
		idInicio = salida;
		for (int i = 0; i < AllNodes.Count; i++) {
			AllNodes [i].salida = false;

			if(AllNodes [i].id == salida)
				AllNodes [i].salida = true;
		}
	}

	public void cambiarPuntoDeLlegada(int llegada){
		idLlegada = llegada;
		for (int i = 0; i < AllNodes.Count; i++) {

			AllNodes [i].llegada = false;

			if(AllNodes [i].id == llegada)
				AllNodes [i].llegada = true;
		}
	}


	void AbrirNodoInicial(){
		for (int i = 0; i < AllNodes.Count; i++) {
			if (AllNodes [i].id == idInicio) {
				NodosAbiertos.Add (AllNodes [i]);
			}
		}
	}

	Stack<int> ObtenerRuta(){
		Node nodoLlegada = null;
		Node nodoInicio = null;

		for (int i = 0; i < AllNodes.Count; i++) {
			if (idLlegada == AllNodes [i].id)
				nodoLlegada = AllNodes [i];
			if (idInicio == AllNodes [i].id)
				nodoInicio = AllNodes [i];
		}

		Stack<int> IDPath = new Stack<int> ();
		if (nodoLlegada != null && nodoInicio != null) {
			Node tmp = nodoLlegada;
			do {
				IDPath.Push (tmp.id);
				tmp.parteDelCamino = true;
				tmp = tmp.parent;
			} while(tmp != nodoInicio);
		}

        NodosVisitados = NodosCerrados.Count;
		return IDPath;
	}

	bool AbrirNodo(Node node){
		if (!NodosAbiertos.Contains (node)) {
			if (!NodosCerrados.Contains (node)) {
				if (!node.isWall) {
					NodosAbiertos.Add (node);
					return true;
				}
			}
		}
		return false;
	}

    void AbrirNodosAdy(Node node)
    {
        for (int i = 0; i < node.AdyNodes.Count; i++)
        {
            if (AbrirNodo(node.AdyNodes[i]))
            {
                node.AdyNodes[i].parent = node;
            }
        }
    }
    void AbrirNodosAdyDijkstra(Node node)
    {
        for (int i = 0; i < node.AdyNodes.Count; i++)
        {
            if (AbrirNodo(node.AdyNodes[i]))
            {
                node.AdyNodes[i].parent = node;
                node.AdyNodes[i].pesoTotal = node.AdyNodes[i].peso + node.pesoTotal;
            }
        }
    }

    void CerrarNodo(Node node)
    {
        if (NodosAbiertos.Contains(node))
        {
            NodosAbiertos.Remove(node);
        }

        if (!NodosCerrados.Contains(node))
        {
            NodosCerrados.Add(node);
        }
    }

    void CerrarNodoDijkstra(Node node)
    {
        
        if (NodosAbiertos.Contains(node))
        {
            NodosAbiertos.Remove(node);
        }

        if (!NodosCerrados.Contains(node))
        {
            NodosCerrados.Add(node);
        }
        else
        {
            for(int i = 0; i < NodosCerrados.Count; i++)
            {
                if(NodosCerrados[i] == node)
                {
                    if((NodosCerrados[i].pesoTotal) > (node.peso + node.pesoTotal))
                    {
                        NodosCerrados[i].pesoTotal = NodosCerrados[i].peso + node.pesoTotal;
                    }
                }
            }


        }
    }

    List<Node> DevolverAdy(Node node){
		List<Node> Ady = new List<Node> ();
		Vector3 nodeTransform = node.gameObject.transform.position;

		for (int i = 0; i < AllNodes.Count; i++) {
			if (Vector3.Distance (AllNodes [i].transform.position, nodeTransform) <= 1.1) {
				Ady.Add (AllNodes [i]);
			}
		}

		return Ady;
	}
}