using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTile : MonoBehaviour {

	public GameObject tilePrefab;
    public GameObject fishPrefab;

    // Use this for initialization
    void Start () {
		
	}
	

	public GameObject SpawnTile()
	{
		GameObject go;
		go = Instantiate (tilePrefab) as GameObject;
		return go;
	}

    public GameObject SpawnFish()
    {
        GameObject go;
        go = Instantiate(fishPrefab) as GameObject;
        return go;
    }

}
