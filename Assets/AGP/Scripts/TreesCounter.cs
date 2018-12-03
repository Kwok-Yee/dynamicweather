using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesCounter : MonoBehaviour {

    public TerrainData data;
    private int totalTrees;

	// Use this for initialization
	void Start () {
        totalTrees = data.treeInstances.Length;
        Debug.Log(this + " - Total Trees: " + totalTrees);
    }
}
