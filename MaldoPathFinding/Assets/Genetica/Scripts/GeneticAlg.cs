using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlg : MonoBehaviour {

    [SerializeField]
    int eliteSurvivors = 5;
    

    public List<Agent> Crossover()
    {
        List<Agent> epochPopulation = new List<Agent>();
        Agent a, b;
        for(int i = 0; i < (GeneticManager.instance.population.Count - eliteSurvivors) / 2; i++)
        {
            a = Roulette();
            b = Roulette();

            List<Gen> a1 = new List<Gen>();
            List<Gen> a2 = new List<Gen>();
            List<Gen> b1 = new List<Gen>();
            List<Gen> b2 = new List<Gen>();

            for (int j = 0; j < a.chromosome._chromosome.Count / 2; j++) a1.Add(a.chromosome._chromosome[j]);
            for (int j = a.chromosome._chromosome.Count / 2; j < a.chromosome._chromosome.Count; j++) a2.Add(a.chromosome._chromosome[j]);
            for (int j = 0; j < b.chromosome._chromosome.Count / 2; j++) b1.Add(b.chromosome._chromosome[j]);
            for (int j = b.chromosome._chromosome.Count / 2; j < b.chromosome._chromosome.Count; j++) b2.Add(b.chromosome._chromosome[j]);

            for (int j = 0; j < a.chromosome._chromosome.Count; j++) a.chromosome._chromosome[j].IntentarMutar();
            for (int j = 0; j < b.chromosome._chromosome.Count; j++) b.chromosome._chromosome[j].IntentarMutar();

            a.chromosome.CambiarCromosoma(a1, b2);
            b.chromosome.CambiarCromosoma(b1, a2);

            epochPopulation.Add(a);
            epochPopulation.Add(b);
        }

        return epochPopulation;
    }

    public Agent Roulette()
    {
        Agent ag = new Agent();
        ag.InicializarAgent();
        int total = 0;
        for (int i = 0; i < GeneticManager.instance.population.Count; i++)
            total += GeneticManager.instance.population[i].chromosome._puntaje;

        int selected = Random.Range(0, total);
        int tmp = 0;
        for (int i = 0; i < GeneticManager.instance.population.Count; i++)
        {
            tmp += GeneticManager.instance.population[i].chromosome._puntaje;

            if (tmp > selected)
            {
                ag = GeneticManager.instance.population[i];
                break;
            }
        }

        return ag;
    }        

    public List<Agent> Elitism()
    {
        List<Agent> elite = new List<Agent>();        

        for(int i= 0; i < eliteSurvivors; i++)
        {
            elite.Add(GeneticManager.instance.population[i]);
        }

        return elite;
    }

    public List<Agent> Epoch()
    {
        List<Agent> newPopulation = new List<Agent>();

        GeneticManager.instance.population.Sort(delegate (Agent a, Agent b) {
            return (a.chromosome._puntaje).CompareTo(b.chromosome._puntaje);
        });

        List<Agent> tmp = Elitism();
        for (int i = 0; i < tmp.Count; i++)
            newPopulation.Add(tmp[i]);

        tmp.Clear();
        tmp = Crossover();
        for (int i = 0; i < tmp.Count; i++)
            newPopulation.Add(tmp[i]);


        return newPopulation;
    }

}
