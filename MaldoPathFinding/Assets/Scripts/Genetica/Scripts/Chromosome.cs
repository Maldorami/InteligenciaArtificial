using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome
{
    [SerializeField]
    int ChromosomeLenght = 8;

    public List<Gen> _chromosome;
    public float _puntaje = 1;

    public void CrearCromosoma()
    {
        _chromosome = new List<Gen>();

        for (int i = 0; i < ChromosomeLenght; i++)
        {
            Gen gen = new Gen();
            gen.CrearGen();
            _chromosome.Add(gen);
        }
        _puntaje = 1;
    }

    public void CambiarCromosoma(List<Gen> x, List<Gen> y)
    {
        _chromosome.Clear();

        for (int i = 0; i < x.Count; i++)
        {
            _chromosome.Add(x[i]);
        }
        for (int i = 0; i < y.Count; i++)
        {
            _chromosome.Add(y[i]);
        }
    }
}
