using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlg : MonoBehaviour{

    [SerializeField]
    int eliteSurvivors = 5;
    

    public List<Chromosome> Crossover()
    {
        List<Chromosome> epochPopulation = new List<Chromosome>();
        Chromosome a, b;
        for(int i = 0; i < (GeneticManager.instance.Ships.Count - eliteSurvivors) / 2; i++)
        {
            a = Roulette();
            b = Roulette();

            List<Gen> a1 = new List<Gen>();
            List<Gen> a2 = new List<Gen>();
            List<Gen> b1 = new List<Gen>();
            List<Gen> b2 = new List<Gen>();

            for (int j = 0; j < a._chromosome.Count / 2; j++) a1.Add(a._chromosome[j]);
            for (int j = a._chromosome.Count / 2; j < a._chromosome.Count; j++) a2.Add(a._chromosome[j]);
            for (int j = 0; j < b._chromosome.Count / 2; j++) b1.Add(b._chromosome[j]);
            for (int j = b._chromosome.Count / 2; j < b._chromosome.Count; j++) b2.Add(b._chromosome[j]);

            for (int j = 0; j < a._chromosome.Count; j++) a._chromosome[j].IntentarMutar();
            for (int j = 0; j < b._chromosome.Count; j++) b._chromosome[j].IntentarMutar();

            a.CambiarCromosoma(a1, b2);
            b.CambiarCromosoma(b1, a2);

            epochPopulation.Add(a);
            epochPopulation.Add(b);
        }

        return epochPopulation;
    }

    public Chromosome Roulette()
    {
        Chromosome ag = GeneticManager.instance.Ships[0].chromosome;

        float total = 0;
        for (int i = 0; i < GeneticManager.instance.Ships.Count; i++)
            total += GeneticManager.instance.Ships[i].chromosome._puntaje;

        float selected = Random.Range(0f, total);
        float tmp = 0;
        for (int i = 0; i < GeneticManager.instance.Ships.Count; i++)
        {
            tmp += GeneticManager.instance.Ships[i].chromosome._puntaje;

            if (tmp > selected)
            {
                ag = GeneticManager.instance.Ships[i].chromosome;
                break;
            }
        }

        return ag;
    }        

    public List<Chromosome> Elitism()
    {
        List<Chromosome> elite = new List<Chromosome>();

        for(int i= 0; i < eliteSurvivors; i++)
        {
            elite.Add(GeneticManager.instance.Ships[i].chromosome);
        }

        return elite;
    }

    public List <Chromosome> Epoch()
    {
        List<Chromosome> newPopulation = new List<Chromosome>();

        GeneticManager.instance.Ships.Sort(delegate (ShipController a, ShipController b)
        {
            return (b.chromosome._puntaje).CompareTo(a.chromosome._puntaje);
        });

        List<Chromosome> tmp = Elitism();
        for (int i = 0; i < tmp.Count; i++)
            newPopulation.Add(tmp[i]);

        tmp.Clear();
        tmp = Crossover();
        for (int i = 0; i < tmp.Count; i++)
            newPopulation.Add(tmp[i]);

        //newPopulation.Clear();
        //for (int i = 0; i < GeneticManager.instance.Ships.Count; i++)
        //    newPopulation.Add(GeneticManager.instance.Ships[i].chromosome);

        for (int i = 0; i < newPopulation.Count; i++)
            newPopulation[i]._puntaje = 0;

        return newPopulation;
    }

}
