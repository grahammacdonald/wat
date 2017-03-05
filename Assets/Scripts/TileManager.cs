using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;
	private Transform playerTransform;

	//Constants
	private float playerTilePosition	= 0;
	private float spawnZ 				= 0.0f;
	private int tilesConstant			= 7;
	private float tileLength			= 10;


	private void Start () 
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

		//Spawning Initial tile prefabs
		for (int i = 0; i < tilesConstant; i++) 
		{
			SpawnTile ();
		}
	}
	
	private void Update () 
	{

		playerTilePosition = spawnZ - (tilesConstant * tileLength);
		if (playerTransform.position.z > playerTilePosition) 
		{
			SpawnTile ();
		}
		
	}

	private void SpawnTile(int prefabIndex = -1)
	{
		GameObject go;
		go = Instantiate (tilePrefabs [0]) as GameObject;
		go.transform.SetParent (transform);

		/*
		 * In Start function, 7 tiles are immidetially created. 
		 * This makes spawnZ's position 7 tilesConstant ahead
		*/

		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
	}

}