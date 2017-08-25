using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerMovement : MonoBehaviour {

	Stack<Vector2> _path;
	public PathFinder pathFinder;

	public float speed = 1;

	// Use this for initialization
	void Start () {
		_path = new Stack<Vector2> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_path.Count > 0) {
			transform.position = Vector2.MoveTowards (transform.position, _path.Peek (), speed * Time.deltaTime);
			if (Vector2.Distance (transform.position, _path.Peek ()) < 0.05) {
				transform.position = _path.Peek ();
				_path.Pop ();
			}
		}
	}

	public void SetPath(){
		_path = pathFinder.ObtenerRuta();
	}
}
