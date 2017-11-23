using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNRedNeuronal {

    int inputs;
    int outputs;
    int hiddenLayers;
    int neuronasPorLayer;
    float pendiente;
    
    List<RNLayer> layers;
    
    public RNRedNeuronal(int _inputs, int _outputs, int _hiddenLayers, int _neuronasPorLayer, float _pendiente)
    {
        inputs = _inputs;
        outputs = _outputs;
        hiddenLayers = _hiddenLayers;
        neuronasPorLayer = _neuronasPorLayer;
        pendiente = _pendiente;
        layers = new List<RNLayer>();
    }

    public void CrearRedNeuronal()
    {
        if(hiddenLayers > 0)
        {
            layers.Add(new RNLayer(inputs, neuronasPorLayer));
            for (int i = 0; i < hiddenLayers; i++)
            {
                layers.Add(new RNLayer(neuronasPorLayer, neuronasPorLayer));
            }
        }

        layers.Add(new RNLayer(inputs, outputs));
    }


    public List<float> UpdateRN(List<float> _inputs)
    {
        //stores the resultant outputs from each layer
        List<float> outputs = new List<float>();
        int cWeight = 0;

        //first check that we have the correct amount of inputs
        if (_inputs.Count != inputs)
        {
            //just return an empty vector if incorrect.
            return outputs;
        }
        //For each layer...
        for (int i = 0; i < hiddenLayers + 1; ++i)
        {
            if (i > 0)
            {
                _inputs = new List<float>(outputs);
            }
            outputs.Clear();
            cWeight = 0;

            //for each neuron sum the inputs * corresponding weights. Throw
            //the total at the sigmoid function to get the output.
            for (int j = 0; j < layers[i].neuronasCount; ++j)
            {
                float activation = 0;
                int NumInputs = layers[i].neuronas[j].inputs;
                //for each weight
                for (int k = 0; k < NumInputs - 1; ++k)
                {
                    //sum the weights x inputs
                    activation += layers[i].neuronas[j].pesos[k] * _inputs[cWeight++];
                }

                activation += layers[i].neuronas[j].pesos[NumInputs - 1] * -1;
                //we can store the outputs from each layer as we generate them.
                //The combined activation is first filtered through the sigmoid
                //function
                outputs.Add(Sigmoide(activation));

                cWeight = 0;
            }
        }

        return outputs;
    }

    float Sigmoide(float act)
    {
        return (1 / (1 + Mathf.Exp(-act / pendiente)));
    }

    public RNCromosoma ObtenerPesos()
    {
        RNCromosoma _pesos = new RNCromosoma();

        foreach(RNLayer layer in layers)
        {
            foreach(RNNeurona neurona in layer.neuronas)
            {
                foreach(float peso in neurona.pesos)
                {
                    _pesos.cromosoma.Add(new RNGen(peso));
                }
            }
        }

        return _pesos;
    }

    public void SetearPesos(RNCromosoma nuevosPesos)
    {
        int tmp = 0;
        for (int i = 0; i < layers.Count; i++)
        {
            for (int j = 0; j < layers[i].neuronasCount; j++)
            {
                for (int k = 0; k < layers[i].neuronas[j].pesos.Count; k++)
                {
                    layers[i].neuronas[j].pesos[k] = nuevosPesos.cromosoma[tmp].peso;
                    tmp++;
                }
            }
        }
    }

}
