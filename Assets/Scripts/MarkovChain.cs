using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class MarkovChain : MonoBehaviour {

    [DllImport("MarkovChain", EntryPoint = "CalculateWeatherState")]
    public static extern int CalculateWeatherState(int index);

    public int currentState = 0;

    void Start () {
        currentState = CalculateWeatherState(currentState);
    }

    void Update()
    {
        switch (currentState)
        {
            case 0:
                Debug.Log("SUNNY");
                break;
            case 1:
                Debug.Log("RAINING");
                break;
            case 2:
                Debug.Log("WINDY");
                break;
        }
    }
}
