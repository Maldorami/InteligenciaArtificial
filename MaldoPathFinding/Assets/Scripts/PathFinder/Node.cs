using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	public List<Node> AdyNodes;
	public Node parent;
	public int id;
	public bool isWall;
	public bool salida = false;
	public bool llegada = false;
	public bool parteDelCamino = false;

	public float peso = 1;
    public float pesoTotal = 1;

    public float distanciaALaLlegada = 0;
}