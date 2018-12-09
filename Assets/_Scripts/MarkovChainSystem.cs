using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkovChainDLL;

public class MarkovChainSystem : MonoBehaviour
{
    MarkovChain markovChain;

    private const int states = 4;
    private int currentState = 0;
    private float startTime = 12;
    private float repeatRateTIme = 12;
    private int runs = 0;

    private int[][] transitions = new int[states][];
    private double[][] probabilities = new double[states][];

    private int[] sunnyTrans = new int[states] { 0, 1, 2, 3 };
    private int[] cloudyTrans = new int[states] { 1, 0, 2, 3 };
    private int[] rainingTrans = new int[states] { 2, 0, 1, 3 };
    private int[] freezingTrans = new int[states] { 3, 0, 1, 2 };

    private double[] sunnyProbs = new double[states] { 0.5, 0.5, 0, 0 };
    private double[] cloudyProbs = new double[states] { 0.4, 0.2, 0.2, 0.2 };
    private double[] rainingProbs = new double[states] { 0.5, 0.1, 0.1, 0.3 };
    private double[] freezingProbs = new double[states] { 0.4, 0.1, 0.2, 0.3 };

    // Use this for initialization
    void Start()
    {
        transitions[0] = sunnyTrans;
        transitions[1] = cloudyTrans;
        transitions[2] = rainingTrans;
        transitions[3] = freezingTrans;

        probabilities[0] = sunnyProbs;
        probabilities[1] = cloudyProbs;
        probabilities[2] = rainingProbs;
        probabilities[3] = freezingProbs;

        markovChain = new MarkovChain();

        InvokeRepeating("CalculateWeatherState", startTime, repeatRateTIme);
    }

    private void CalculateWeatherState()
    {
        currentState = markovChain.CalculateWeatherState(states, transitions, probabilities);
        runs++;
        Debug.Log("Run: " + runs + " State: " + currentState);
    }

    public int GetCurrentState() { return currentState; }
}
