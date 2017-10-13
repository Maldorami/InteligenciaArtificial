using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{

    List<Boid> _boids;

    void Start()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Boid");
        _boids = new List<Boid>();
        for (int i = 0; i < tmp.Length; i++)
        {
            _boids.Add(tmp[i].GetComponent<Boid>());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray rayHit = Camera.main.ScreenPointToRay(Input.mousePosition);

            for (int i = 0; i < _boids.Count; i++)
            {
                _boids[i].GoToPosition(rayHit.origin);
            }

        }
    }
}
