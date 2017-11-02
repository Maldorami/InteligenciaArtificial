using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome : MonoBehaviour {

    [SerializeField]
    int ChromosomeLenght = 6;

    public Gen[] _chromosome;
    public int _puntaje = 1;

    public void CrearCromosoma()
    {
        _chromosome = new Gen[ChromosomeLenght];

        for (int i = 0; i < _chromosome.Length; i++)
            _chromosome[i].CrearGen();

        _puntaje = 1;
    }

}
