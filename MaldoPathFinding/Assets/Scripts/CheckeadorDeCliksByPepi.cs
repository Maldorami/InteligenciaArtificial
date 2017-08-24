using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckeadorDeCliksByPepi : MonoBehaviour {

	public PathFinder pathFinder;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (2)) {
			Camera cam = Camera.main;
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null)
			if (rayHit.collider.gameObject.tag == "Node") {
				rayHit.collider.gameObject.GetComponent<Node> ().isWall = !rayHit.collider.gameObject.GetComponent<Node> ().isWall;
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			Camera cam = Camera.main;
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null)
			if (rayHit.collider.gameObject.tag == "Node") {
				pathFinder.cambiarPuntoDeSalida (rayHit.collider.gameObject.GetComponent<Node> ().id);
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			Camera cam = Camera.main;
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null)
			if (rayHit.collider.gameObject.tag == "Node") {
				pathFinder.cambiarPuntoDeLlegada (rayHit.collider.gameObject.GetComponent<Node> ().id);
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pathFinder.ClearWalls ();
		}



		if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
		{
			Camera cam = Camera.main;
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null)
			if (rayHit.collider.gameObject.tag == "Node") {
				rayHit.collider.gameObject.GetComponent<Node> ().peso++;
			}
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
		{
			Camera cam = Camera.main;
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null)
			if (rayHit.collider.gameObject.tag == "Node") {
				rayHit.collider.gameObject.GetComponent<Node> ().peso--;
			}
		}



	}
}
