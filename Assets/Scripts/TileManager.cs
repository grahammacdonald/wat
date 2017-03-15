using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tiles;
	private Transform playerTransform;
	public NewTile spawner;

	//Constants
	private float playerTilePosition	= 0;
	private float spawnZ 				= 0.0f;
	private int tilesConstant			= 7;
	private float tileLength			= 100;


	void Start () 
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		spawner = gameObject.GetComponent<NewTile> ();
		tiles = new GameObject[15];

		//Spawning Initial tile prefabs
		for (int i = 0; i < tilesConstant; i++) 
		{
			SpawnTile ();
		}
	}
	
	void Update () 
	{

		playerTilePosition = spawnZ - (tilesConstant * tileLength);
		if (playerTransform.position.z > playerTilePosition) 
		{
			SpawnTile ();
		}

		//Iterate through the 
		for (int i = 0; i < tiles.Length; i++) {
			if (tiles [i] != null) {
				if (tiles[i].transform.position.z < playerTransform.position.z - tileLength) {
					Destroy (tiles [i]);
				}
			}
		}
		
	}

	private void SpawnTile(int prefabIndex = -1)
	{
		GameObject go;
		//Calls spawner code to create a new tile ahead of object
		go = spawner.SpawnTile ();
		go.transform.SetParent (transform);


		//Moves new tile into a emtpy spot in the array of game tiles
		for (int i = 0; i < tiles.Length; i++) {
			if (tiles [i] == null) {
				tiles [i] = go;
				break;
			}
		}

		/*
		 * In Start function, 7 tiles are immidetially created. 
		 * This makes spawnZ's position 7 tilesConstant ahead
		*/

		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
	}

}