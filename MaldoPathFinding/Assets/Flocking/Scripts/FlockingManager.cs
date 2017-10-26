using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{

    public float rangeNeighbors = 1;
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

        ClearAndAddNeighborsToBoids();
    }

    void ClearAndAddNeighborsToBoids()
    {
        for (int x = 0; x < _boids.Count; x++) _boids[x].ClearNeighbors();

        for(int i = 0; i < _boids.Count; i++)
        {
            for(int j = i + 1; j < _boids.Count; j++)
            {
                if (Vector2.Distance(_boids[i].gameObject.transform.position,_boids[j].gameObject.transform.position) < rangeNeighbors)
                {
                    _boids[i].AddNeighbor(_boids[j]);
                    _boids[j].AddNeighbor(_boids[i]);
                }
            }
        }
    }
}
