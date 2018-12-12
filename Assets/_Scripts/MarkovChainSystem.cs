using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkovChainDLL;

public class MarkovChainSystem : MonoBehaviour
{
    MarkovChain markovChain;

    [SerializeField]
    private const int states = 4;
    [SerializeField]
    private int currentState = 0;
    [SerializeField]
    private float waitTime = 5;
    private int runs = 0;

    private int[][] transitions = new int[states][];
    private double[][] probabilities = new double[states][];
    private double[][] tempProbabilities = new double[states][];

    [SerializeField]
    private int[] sunnyTrans = new int[states] { 0, 1, 2, 3 };
    [SerializeField]
    private int[] cloudyTrans = new int[states] { 1, 0, 2, 3 };
    [SerializeField]
    private int[] rainingTrans = new int[states] { 2, 0, 1, 3 };
    [SerializeField]
    private int[] freezingTrans = new int[states] { 3, 0, 1, 2 };

    [SerializeField]
    private double[] sunnyProbs = new double[states] { 0.5, 0.5, 0, 0 };
    [SerializeField]
    private double[] cloudyProbs = new double[states] { 0.4, 0.2, 0.2, 0.2 };
    [SerializeField]
    private double[] rainingProbs = new double[states] { 0.5, 0.1, 0.1, 0.3 };
    [SerializeField]
    private double[] freezingProbs = new double[states] { 0.4, 0.1, 0.2, 0.3 };

    float time = 0;

    private void Start()
    {
        transitions[0] = sunnyTrans;
        transitions[1] = cloudyTrans;
        transitions[2] = rainingTrans;
        transitions[3] = freezingTrans;

        UpdateProbabilities();

        markovChain = new MarkovChain();
    }

    private void UpdateProbabilities()
    {
        probabilities[0] = sunnyProbs;
        probabilities[1] = cloudyProbs;
        probabilities[2] = rainingProbs;
        probabilities[3] = freezingProbs;
        tempProbabilities = probabilities;
    }

    private void Update()
    {
        if (time < waitTime)
            time += Time.deltaTime;
        if(time > waitTime)
        {
            time = 0;
            CalculateWeatherState();
        }
    }

    private void CalculateWeatherState()
    {
        if (tempProbabilities != probabilities)
        {
            Debug.Log("CHANGED");
            UpdateProbabilities();
        }

        currentState = markovChain.CalculateWeatherState(states, transitions, probabilities);
        runs++;
        Debug.Log("Run: " + runs + " State: " + currentState);
    }

    public int GetCurrentState() { return currentState; }
}