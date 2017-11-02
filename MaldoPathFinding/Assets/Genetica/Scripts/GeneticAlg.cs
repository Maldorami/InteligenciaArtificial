using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlg : MonoBehaviour {

    [SerializeField]
    int elitePercentage = 10;

    [SerializeField]
    Agent[] roulette;

    public void Crossover()
    {

    }

    public Agent Roulette()
    {
        Agent ag = new Agent();
        ag.InicializarAgent();





        return ag;
    }

    void SetRoulette()
    {
        int tmp = 0;
        for (int i = 0; i < GeneticManager.instance.agents.Length; i++)
        {
            tmp += GeneticManager.instance.agents[i].chromosome._puntaje;
        }

        roulette = new Agent[tmp];


        for (int j = 0; j < GeneticManager.instance.agents.Length; j++)
        {
            
        }

    }

    public void Mutation()
    {

    }

    public Agent[] Elitism()
    {
        Agent[] elite = new Agent[(elitePercentage * GeneticManager.instance.AgentPoblation) / 100];
        for (int x = 0; x < elite.Length; x++)
        {
            elite[x] = GeneticManager.instance.agents[x];
        }

        for (int i = 0; i < GeneticManager.instance.agents.Length; i++)
        {
            for(int j = 0; j < elite.Length; j++)
            {
                if(GeneticManager.instance.agents[i].chromosome._puntaje > elite[j].chromosome._puntaje)
                {
                    elite[j] = GeneticManager.instance.agents[i];
                    break;
                }
            }
        }

        return elite;
    }

    public void Epoch()
    {
        
    }

}
