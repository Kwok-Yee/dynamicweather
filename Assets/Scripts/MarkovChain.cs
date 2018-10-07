using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class MarkovChain : MonoBehaviour {

    [DllImport("MarkovChain", EntryPoint = "Sum")]
    public static extern double Sum(double a, double b);

    // Use this for initialization
    void Start () {
        Debug.Log(Sum(500, 100));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
