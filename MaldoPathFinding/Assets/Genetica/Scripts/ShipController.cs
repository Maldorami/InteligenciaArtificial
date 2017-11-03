using UnityEngine;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {
    [SerializeField]
    GameObject fire;
    [SerializeField]
    float throtle;
    [SerializeField]
    float steerSpeed;
    [SerializeField]
    Rigidbody _rg;

    [SerializeField]
    Agent agent;

    Queue<Gen> actionsAndTime;
    float timer = 0;
    int puntaje = 0;

    private void OnEnable()
    {
        _rg = GetComponent<Rigidbody>();
        actionsAndTime = new Queue<Gen>();
        actionsAndTime.Clear();
    }

    private void Update()
    {
        fire.SetActive(false);
        
        if(actionsAndTime.Count > 0)
        {
            timer += Time.deltaTime;
            puntaje += (int)Time.deltaTime;

            if (timer < actionsAndTime.Peek()._tiempo)
                switch (actionsAndTime.Peek()._action)
                {
                    case Action.Propulsor:
                        ApplyThrotle();
                        break;
                    case Action.GirarDerecha:
                        SteerLeft();
                        break;
                    case Action.GirarIzquierda:
                        SteerRight();
                        break;
                }
            else
            {
                actionsAndTime.Dequeue();
                timer = 0;
            }
        }
    }

    public void SetAgent(Agent ag)
    {
        agent = ag;
        puntaje = 0;
        actionsAndTime = new Queue<Gen>();
        actionsAndTime.Clear();

        for(int i = 0; i < agent.chromosome._chromosome.Count; i++)
        {
            actionsAndTime.Enqueue(agent.chromosome._chromosome[i]);
        }
    }

    public void ApplyThrotle()
    {
        _rg.AddForce(transform.up * throtle);
        fire.SetActive(true);
    }

    public void SteerLeft()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, steerSpeed * Time.deltaTime));
    }
    public void SteerRight()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -steerSpeed * Time.deltaTime));
    }
    

    public void CalcularPuntaje()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("Finish");
        puntaje += 1000 / ((int)Vector3.Distance(transform.position, tmp.transform.position) + 1);

        agent.chromosome._puntaje = puntaje;
    }
}
