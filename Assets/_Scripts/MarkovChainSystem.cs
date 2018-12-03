using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkovChainDLL;

public class MarkovChainSystem : MonoBehaviour
{
    MarkovChain markovChain;

    private static int states = 3;
    private int currentState = 0;
    private int startTime = 5;
    private int repeatRateTIme = 5;

    private int[][] transitions = new int[states][];
    private double[][] probabilities = new double[states][];

    private int[] sunnyTrans = new int[] { 0, 1, 2 };
    private int[] cloudyTrans = new int[] { 1, 0, 2 };
    private int[] freezingTrans = new int[] { 2, 0, 1 };
    //private int[] sunnyTrans = new int[] { 0, 1, 2, 3, 4 };
    //private int[] rainingTrans = new int[] { 1, 0, 2, 3, 4 };
    //private int[] windyTrans = new int[] { 2, 0, 1, 3, 4 };
    //private int[] stormingTrans = new int[] { 3, 0, 1, 2, 4 };
    //private int[] snowingTrans = new int[] { 4, 0, 1, 2, 3 };

    private double[] sunnyProbs = new double[] { 0.5, 0.3, 0.2 };
    private double[] cloudyProbs = new double[] { 0.4, 0.3, 0.3 };
    private double[] freezingProbs = new double[] { 0.5, 0.2, 0.3 };
    //private double[] sunnyProbs = new double[] { 0.5, 0.2, 0.2, 0.1, 0 };
    //private double[] rainingProbs = new double[] { 0.3, 0.3, 0.1, 0.1, 0.2 };
    //private double[] windyProbs = new double[] { 0.3, 0.3, 0.1, 0.1, 0.2 };
    //private double[] stormingProbs = new double[] { 0.4, 0.1, 0.3, 0.2, 0 };
    //private double[] snowingProbs = new double[] { 0.5, 0, 0.2, 0.2, 0.1 };

    // Use this for initialization
    void Start()
    {
        transitions[0] = sunnyTrans;
        transitions[1] = cloudyTrans;
        transitions[2] = freezingTrans;
        //transitions[0] = sunnyTrans;
        //transitions[1] = rainingTrans;
        //transitions[2] = windyTrans;
        //transitions[3] = stormingTrans;
        //transitions[4] = snowingTrans;

        probabilities[0] = sunnyProbs;
        probabilities[1] = cloudyProbs;
        probabilities[2] = freezingProbs;
        //probabilities[0] = sunnyProbs;
        //probabilities[1] = rainingProbs;
        //probabilities[2] = windyProbs;
        //probabilities[3] = stormingProbs;
        //probabilities[4] = snowingProbs;

        markovChain = new MarkovChain();

        InvokeRepeating("CalculateWeatherState", startTime, repeatRateTIme);
    }

    private void CalculateWeatherState()
    {
        currentState = markovChain.CalculateWeatherState(states, transitions, probabilities);
    }

    public int GetCurrentState() { return currentState; }
}
