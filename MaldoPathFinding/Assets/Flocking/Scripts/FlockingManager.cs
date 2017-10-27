using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{

    public float rangeNeighbors = 1;
    List<Boid> _boids;

    public GameObject target;

    public float SepModif = 1;
    public float CohModif = 1;
    public float AlignmModif = 1;
    public float DistanceModif = 1;

    public float minDistanceToNeighboor = 5f;

    void Start()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Boid");
        _boids = new List<Boid>();
        for (int i = 0; i < tmp.Length; i++)
        {
            _boids.Add(tmp[i].GetComponent<Boid>());
        }

        for (int x = 0; x < _boids.Count; x++)
        {
            _boids[x].target = target;
        }

    }

    void Update()
    {
        ClearAndAddNeighborsToBoids();

    }

    void ClearAndAddNeighborsToBoids()
    {
        for (int x = 0; x < _boids.Count; x++) {
            _boids[x].ClearNeighbors();
        }

        for (int i = 0; i < _boids.Count; i++)
        {
            _boids[i].SepModif = SepModif;
            _boids[i].CohModif = CohModif;
            _boids[i].AlignmModif = AlignmModif;
            _boids[i].DistanceModif = DistanceModif;

            _boids[i].minDistanceToNeighboor = minDistanceToNeighboor;

            for (int j = i + 1; j < _boids.Count; j++)
            {
                if (Vector2.Distance(_boids[i].gameObject.transform.position, _boids[j].gameObject.transform.position) < rangeNeighbors)
                {
                    _boids[i].AddNeighbor(_boids[j]);
                    _boids[j].AddNeighbor(_boids[i]);
                }
            }
        }
    }
}
