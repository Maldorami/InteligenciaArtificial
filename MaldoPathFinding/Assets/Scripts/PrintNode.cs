using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintNode : MonoBehaviour {

	Node node;
	SpriteRenderer rendi;
	public TextMesh idText;

	public Color Wall;
	public Color Walkable;
	public Color Path;
	public Color Starts;
	public Color Finish;

	void Start(){
		node = GetComponent<Node> ();
		rendi = GetComponent<SpriteRenderer> ();

		if(idText != null)
		idText.text = node.peso.ToString();
		
		/*
		Wall = Color.red;
		Walkable = Color.grey;
		Path = Color.green;
		Starts = Color.blue;
		Finish = Color.black;
		*/
	}


	void Update ()
	{		
		if (node.isWall) {
			rendi.color = Wall;
		} else {
			rendi.color = Walkable;
		}

		if (node.parteDelCamino) {
			rendi.color = Path;
		}

		if (node.salida) {
			rendi.color = Starts;
		}
		if (node.llegada) {
			rendi.color = Finish;
		}

		if(idText != null)
		idText.text = node.peso.ToString();
	}
}
