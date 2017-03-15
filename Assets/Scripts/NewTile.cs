using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTile : MonoBehaviour {

	public GameObject tilePrefab;

	// Use this for initialization
	void Start () {
		
	}
	

	public GameObject SpawnTile()
	{
		GameObject go;
		go = Instantiate (tilePrefab) as GameObject;
		return go;
	}

}
