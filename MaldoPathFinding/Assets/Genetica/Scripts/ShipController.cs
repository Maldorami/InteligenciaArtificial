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
   
    public Stack<Gen> actionsAndTime;

    float timer = 0;

    [SerializeField]
    Transform resetPoint;

    public Transform target;

    public Chromosome chromosome;

    public void InicializarAgent()
    {
        chromosome = new Chromosome();
        chromosome.CrearCromosoma();
    }

    private void OnDisable()
    {
        _rg.isKinematic = true;
        _rg.isKinematic = false;

        _rg.velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        _rg = GetComponent<Rigidbody>();
        actionsAndTime = new Stack<Gen>();
        actionsAndTime.Clear();       
        transform .position= resetPoint.position;
        transform.rotation = resetPoint.rotation;
        SetActions();
    }

    private void FixedUpdate()
    {
        fire.SetActive(false);
        
        if(actionsAndTime.Count > 0)
        {
            timer += Time.fixedDeltaTime;

            if (timer <= actionsAndTime.Peek()._tiempo)
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
                actionsAndTime.Pop();
                timer = 0;
            }
        }
    }

    public void setChromosome(Chromosome chr)
    {
        chromosome = chr;
    }

    public void SetActions()
    {
        actionsAndTime = new Stack<Gen>();
        actionsAndTime.Clear();

        for(int i = 0; i < chromosome._chromosome.Count; i++)
        {
            actionsAndTime.Push(chromosome._chromosome[i]);
        }
                
    }

    public void ApplyThrotle()
    {
        _rg.AddForce(transform.up * throtle, ForceMode.Acceleration);
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
        chromosome._puntaje = 1000 / (Vector3.Distance(transform.position, target.position) + 0.1f);
        Debug.Log(gameObject.name + "  " + chromosome._puntaje);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Finish") chromosome._puntaje += 100;
    }
}
