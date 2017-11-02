using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public Chromosome chromosome;

    public void InicializarAgent()
    {
        chromosome.CrearCromosoma();
    }

    public void RealizarTest()
    {

    }

}
