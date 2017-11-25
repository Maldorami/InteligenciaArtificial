using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGeneticAlg : MonoBehaviour
{

    public int eliteSurvivors = 5;

    public List<RNCromosoma> Crossover()
    {
        List<RNCromosoma> epochPopulation = new List<RNCromosoma>();
        RNCromosoma a, b;

        for (int i = 0; i < (RNManager.instance.Ships.Count - eliteSurvivors) / 2; i++)
        {
            a = Roulette();
            b = Roulette();

            RNCromosoma child1 = new RNCromosoma();
            RNCromosoma child2 = new RNCromosoma();

            List<RNGen> a1 = new List<RNGen>();
            List<RNGen> a2 = new List<RNGen>();
            List<RNGen> b1 = new List<RNGen>();
            List<RNGen> b2 = new List<RNGen>();

            int pivot = Random.Range(0, a.cromosoma.Count);

            for (int j = 0; j < pivot; j++) a1.Add(a.cromosoma[j]);
            for (int j = pivot; j < a.cromosoma.Count; j++) a2.Add(a.cromosoma[j]);
            for (int j = 0; j < pivot; j++) b1.Add(b.cromosoma[j]);
            for (int j = pivot; j < b.cromosoma.Count; j++) b2.Add(b.cromosoma[j]);

            for (int j = 0; j < a.cromosoma.Count; j++) a.cromosoma[j].IntentarMutar();
            for (int j = 0; j < b.cromosoma.Count; j++) b.cromosoma[j].IntentarMutar();

            child1.CambiarCromosoma(a1, b2);
            child2.CambiarCromosoma(b1, a2);

            epochPopulation.Add(child1);
            epochPopulation.Add(child2);
        }

        return epochPopulation;
    }

    public RNCromosoma Roulette()
    {
        RNCromosoma ag = RNManager.instance.Ships[0].cromosoma;

        float total = 0;
        for (int i = 0; i < RNManager.instance.Ships.Count; i++)
            total += RNManager.instance.Ships[i].puntaje;

        float selected = Random.Range(0f, total);
        float tmp = 0;
        for (int i = 0; i < RNManager.instance.Ships.Count; i++)
        {
            tmp += RNManager.instance.Ships[i].puntaje;

            if (tmp > selected)
            {
                ag = RNManager.instance.Ships[i].cromosoma;
                break;
            }
        }

        return ag;
    }

    public List<RNCromosoma> Elitism()
    {
        List<RNCromosoma> elite = new List<RNCromosoma>();

        for (int i = 0; i < eliteSurvivors; i++)
        {
            elite.Add(RNManager.instance.Ships[i].cromosoma);
        }

        return elite;
    }

    public List<RNCromosoma> Epoch()
    {
        List<RNCromosoma> newPopulation = new List<RNCromosoma>();

        RNManager.instance.Ships.Sort(delegate (RNShipController a, RNShipController b)
        {
            return (b.puntaje).CompareTo(a.puntaje);
        });


        List<RNCromosoma> tmp = Elitism();
        for (int i = 0; i < tmp.Count; i++)
            newPopulation.Add(tmp[i]);

        tmp.Clear();
        tmp = Crossover();
        for (int i = 0; i < tmp.Count; i++)
            newPopulation.Add(tmp[i]);

        return newPopulation;
    }
}
