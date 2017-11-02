using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticManager : MonoBehaviour {

    public static GeneticManager instance;
    public int AgentPoblation = 50;
    public List<Agent> population;

    [SerializeField]
    GeneticAlg geneticAlg;

    public GameObject Ship;
    bool onTest = false;

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
                

        if (!onTest)
        {

        }
    }


    void BeginTest()
    {
        onTest = true;
    }

    public void EpochPoblation()
    {
        population.Clear();
        population = geneticAlg.Epoch();
    }
}
