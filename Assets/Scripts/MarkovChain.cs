using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class MarkovChain : MonoBehaviour {

    [DllImport("MarkovChain", EntryPoint = "calculateWeatherState")]
    public static extern int CalculateWeatherState();

    [DllImport("MarkovChain", EntryPoint = "getRandom")]
    public static extern double GetRandom(double min, double max);

    [DllImport("MarkovChain", EntryPoint = "calculateTransition")]
    public static extern int CalculateTransition(int index);

    public int currentState = 0;

    void Update()
    {
        currentState = CalculateWeatherState();
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

    [ContextMenu("CALCULATE")]
    void Calculate()
    {
        Debug.Log(CalculateTransition(1));
    }
}
