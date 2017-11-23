using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text text;

    [Range(1f, 3f)]
    public float velocity = 1;
    
    // Update is called once per frame
    void Update () {
        text.text = "Generation: " + (RNManager.instance.genNro + 1);
        Time.timeScale = velocity;
	}
}
