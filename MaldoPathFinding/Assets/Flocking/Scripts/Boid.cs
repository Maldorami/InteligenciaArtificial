using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 5;
    public float steeringSpeed = 3;
    public List<Boid> neighbors;

    public float pesoSep = 1;
    public float pesoCoh = 1;

    public float minDistanceToNeighboor = 5f;
    public float SepModif = 1;
    public float CohModif = 1;
    public float AlignmModif = 1;
    public float DistanceModif = 1;

    public GameObject target;    

    private void Start()
    {
        neighbors = new List<Boid>();
    }

    void Update()
    {
            CalculateMovement();
    }  

    public void CalculateMovement()
    {
        Vector3 cohesion = ComputeCohesion();

        pesoSep = Mathf.Clamp01(Vector3.Distance(transform.position, cohesion) / minDistanceToNeighboor);
        pesoCoh = 1 - pesoSep;
        cohesion.Normalize();

        Vector3 separation = -cohesion;
        Vector3 alignment = ComputeAlignment();

        cohesion = (cohesion - transform.position).normalized;
        separation = -cohesion;

        transform.position += -transform.forward * speed * Time.deltaTime;
        Vector3 result = (cohesion * pesoCoh * CohModif + separation * pesoSep * SepModif + alignment * AlignmModif + (transform.position - target.transform.position).normalized * DistanceModif);
        result.Normalize();

        transform.forward = Vector3.Lerp(transform.forward, result, steeringSpeed * Time.deltaTime);
    }


    Vector3 ComputeAlignment()
    {
        Vector3 tmp = transform.forward;

        for (int i = 0; i < neighbors.Count; i++)
            tmp += neighbors[i].transform.right;

        tmp /= neighbors.Count + 1;

        return tmp.normalized;
    }

    Vector3 ComputeCohesion()
    {
        Vector3 center = transform.position;

        for (int i = 0; i < neighbors.Count; i++)
            center += neighbors[i].transform.position;
        center /= neighbors.Count + 1;
        
        return center;
    }
    Vector3 ComputeSeparation()
    {
        Vector3 sep = Vector3.zero;

        for (int i = 0; i < neighbors.Count; i++)
            sep += neighbors[i].transform.position - transform.position;

        sep *= -1;

        return sep;
    }


    public void AddNeighbor(Boid neighbor)
    {
        neighbors.Add(neighbor);
    }

    public void ClearNeighbors()
    {
        neighbors.Clear();
    }

}
