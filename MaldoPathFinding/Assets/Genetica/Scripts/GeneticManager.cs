using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticManager : MonoBehaviour
{

    public static GeneticManager instance;
    public int AgentPoblation = 50;
    public List<Agent> population;

    [SerializeField]
    GeneticAlg geneticAlg;

    public GameObject Ship;
    bool onTest = false;

    public float timerToEpoch = 10;
    float timer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        FirstPopulation();
        BeginTest();
    }

    void FirstPopulation()
    {
        population = new List<Agent>();
        for (int i = 0; i < AgentPoblation; i++)
        {
            Agent ag = new Agent();
            ag.InicializarAgent();
            population.Add(ag);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerToEpoch)
        {
            onTest = false;
            CalcularPuntajes();
        }

        if (!onTest)
        {
            KillCurrentPoblation();
            EpochPoblation();
            BeginTest();
            timer = 0;
        }
    }

    void CalcularPuntajes()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < tmp.Length; i++)
        {
            tmp[i].GetComponent<ShipController>().CalcularPuntaje();
        }
    }

    void KillCurrentPoblation()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < tmp.Length; i++)
        {
            Destroy(tmp[i].gameObject);
        }
    }

    void BeginTest()
    {        
        for (int i = 0; i < population.Count; i++)
        {
            ShipController ship = Instantiate(Ship).GetComponent<ShipController>();
            ship.SetAgent(population[i]);
        }
        onTest = true;
    }

    public void EpochPoblation()
    {
        //population.Clear();
        population = geneticAlg.Epoch();
    }
}
