using System.Collections.Generic;
using UnityEngine;

public class GeneticManager : MonoBehaviour
{

    public static GeneticManager instance;
    public int AgentPoblation = 50;
    
    public GeneticAlg geneticAlg;

    public List<ShipController> Ships;
    bool onTest = false;

    public float timerToEpoch = 10;
    float timer;

    public float distModif = 1;
    public float rotModif = 1;
    public float airTimeModif = 1;
    public float impactToFinishModif = 1;


    private void Start()
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
        for (int i = 0; i < Ships.Count; i++)
        {
            Ships[i].InicializarAgent();
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
        for (int i = 0; i < Ships.Count; i++)
        {
            Ships[i].CalcularPuntaje();
        }
    }

    void KillCurrentPoblation()
    {
        for (int i = 0; i < Ships.Count; i++)
        {
            Ships[i].gameObject.SetActive(false);
        }
    }

    void BeginTest()
    {        
        for (int i = 0; i < Ships.Count; i++)
        {
            Ships[i].gameObject.GetComponent<ShipController>().airTimeModif = airTimeModif;
            Ships[i].gameObject.GetComponent<ShipController>().distModif = distModif;
            Ships[i].gameObject.GetComponent<ShipController>().rotModif = rotModif;
            Ships[i].gameObject.GetComponent<ShipController>().impactToFinishModif = impactToFinishModif;
            Ships[i].gameObject.SetActive(true);
        }
        onTest = true;
    }

    public void EpochPoblation()
    {
        List<Chromosome> newChromosomes = geneticAlg.Epoch();
        for (int i = 0; i < Ships.Count; i++)
            Ships[i].setChromosome(newChromosomes[i]);
    }
}
