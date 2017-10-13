using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 5;
    public float steeringSpeed = 3;
    public List<Boid> neighbors;

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
            Vector3 diff = target - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(rot_z, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, steeringSpeed * Time.deltaTime);
            transform.position += transform.right * speed * Time.deltaTime;

            if (Vector2.Distance(transform.position, target) <= 0.5) move = false;
        }
    }

    public void GoToPosition(Vector3 destPos)
    {
        move = true;
        target = destPos;
        Debug.Log("<color=yellow>" + destPos + "</color>");
    }

    public void AddNeighbors(Boid neighbor)
    {
        neighbors.Add(neighbor);
    }

    public void Calculate()
    {

    }

    public void ClearNeighbor()
    {
        neighbors.Clear();
    }

}
