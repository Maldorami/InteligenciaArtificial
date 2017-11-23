using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverToWall : MonoBehaviour {

	public void ConvertToWall(){
		gameObject.GetComponent<Node> ().isWall = !gameObject.GetComponent<Node> ().isWall;
	}
}
