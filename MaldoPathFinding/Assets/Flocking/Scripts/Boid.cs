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

    bool move = false;
    Vector3 target = new Vector2();    

    private void Start()
    {
        neighbors = new List<Boid>();
    }

    void Update()
    {
        if (move)
        {
            CalculateMovement();
        }
    }

    public void GoToPosition(Vector3 destPos)
    {
        move = true;
        target = destPos;
        Debug.Log("<color=yellow>" + destPos + "</color>");
    }

  

    public void CalculateMovement()
    {
        // DIR = (Sep * pesoSep + Coh * pesoCoh + Alin) / 3.0f

        Vector3 cohesion = ComputeCohesion();
        Vector3 separation = ComputeSeparation();
        Quaternion alignment = ComputeAlignment();
        
        //transform.position = cohesion - separation;
        transform.rotation = Quaternion.Lerp(transform.rotation, alignment, steeringSpeed * Time.deltaTime);

        transform.position += transform.right * speed * Time.deltaTime;
        if (Vector2.Distance(transform.position, target) <= 0.5) move = false;
    }


    Quaternion ComputeAlignment()
    {
        Vector3 tmp = target;

        for (int i = 0; i < neighbors.Count; i++)
            tmp += neighbors[i].transform.position;

        tmp /= neighbors.Count;

        Vector3 diff = (target + tmp) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(rot_z, Vector3.forward);

        return rot;
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
