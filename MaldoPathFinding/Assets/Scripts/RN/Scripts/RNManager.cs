using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNManager : MonoBehaviour {
    
    public static RNManager instance;
    public RNGeneticAlg geneticAlg;
    public List<RNShipController> Ships;
    bool onTest = false;

    public float timerToEpoch = 10;
    float timer;

    public float distModif = 1;
    public float rotModif = 1;
    public float airTimeModif = 1;
    public float impactToFinishModif = 1;
    public float objectiveReward = 1;

    public int inputs;
    public int outputs;
    public int hiddenLayers;
    public int neuronasPorLayer;
    public float pendiente;

    public int genNro = 0;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        genNro = 0;

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
            RNShipController tmp = Ships[i].gameObject.GetComponent<RNShipController>();

            tmp.airTimeModif = airTimeModif;
            tmp.distModif = distModif;
            tmp.rotModif = rotModif;
            tmp.impactToFinishModif = impactToFinishModif;
            tmp.objectiveReward = objectiveReward;

            tmp.gameObject.SetActive(true);
        }

        onTest = true;
    }

    public void EpochPoblation()
    {
        List<RNCromosoma> newChromosomes = geneticAlg.Epoch();
                
        for (int i = 0; i < Ships.Count; i++)
        {
            Ships[i].ActualizarCromosoma(newChromosomes[i]);
            Ships[i].puntaje = 0;
        }

        genNro++;
    }
}
