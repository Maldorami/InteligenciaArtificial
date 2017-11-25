using System.Collections.Generic;
using UnityEngine;

public struct RNGen
{
    public float peso;
    public RNGen(float _peso)
    {
        peso = _peso;
    }
    public void IntentarMutar()
    {
        if(Random.Range(0, 100) < 10)
        {
            peso += Random.Range(-0.01f, 0.01f);
        }
    }
};

public struct RNNeurona
{
    public List<float> pesos;
    public int inputs;

    public RNNeurona(int _inputs)
    {
        inputs = _inputs + 1;
        pesos = new List<float>();
        for (int i = 0; i < inputs; i++)
        {
            pesos.Add(Random.Range(-1f, 1f));
        }
    }
};

public struct RNLayer
{
    public List<RNNeurona> neuronas;
    public int neuronasCount;

    public RNLayer(int _inputs, int _neuCount)
    {
        neuronas = new List<RNNeurona>();
        neuronasCount = _neuCount;

        for (int i = 0; i < neuronasCount; i++)
        {
            neuronas.Add(new RNNeurona(_inputs));
        }
    }
};