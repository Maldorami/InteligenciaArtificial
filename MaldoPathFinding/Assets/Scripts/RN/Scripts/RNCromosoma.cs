using System.Collections.Generic;

public class RNCromosoma {

    public List<RNGen> cromosoma;

    public RNCromosoma()
    {
        cromosoma = new List<RNGen>();
    }

    public RNCromosoma(int maxActions, List<float> pesos)
    {
        cromosoma = new List<RNGen>();

        for (int i = 0; i < maxActions; i++)
        {
            cromosoma.Add(new RNGen(pesos[i]));
        }
    }

    public void CambiarCromosoma(List<RNGen> x, List<RNGen> y)
    {
        cromosoma.Clear();

        for (int i = 0; i < x.Count; i++)
        {
            cromosoma.Add(x[i]);
        }
        for (int i = 0; i < y.Count; i++)
        {
            cromosoma.Add(y[i]);
        }
    }
}
