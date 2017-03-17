using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tiles;
    public GameObject[] fishes;

    private Transform playerTransform;
	public NewTile spawner;

    //Constants
    private float playerFishPosition    = 0;
    private float playerTilePosition	= 0;
	private float spawnZ 				= 0.0f;
	private int   tilesConstant			= 7;
	private float tileLength			= 100;
    private float spawnDistance         = 0.0f;
    private float fishConstant          = 5.0f;



    void Start () 
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		spawner         = gameObject.GetComponent<NewTile> ();
		tiles           = new GameObject[15];
        fishes          = new GameObject[15];

        //Spawning Initial tile prefabs
        for (int i = 0; i < tilesConstant; i++) 
		{
			SpawnTile ();
            SpawnFish ();
		}


	}
	
	void Update () 
	{

		playerTilePosition = spawnZ - (tilesConstant * tileLength);

        if (playerTransform.position.z > playerTilePosition) 
		{
			SpawnTile ();
            SpawnFish();

        }

        //Iterate through the 
        for (int i = 0; i < tiles.Length; i++) {
			if (tiles [i] != null) {
				if (tiles[i].transform.position.z < playerTransform.position.z - tileLength) {
					Destroy (tiles [i]);
				}
			}
		}

        //Left behind fish deletion
        for (int j = 0; j < fishes.Length; j++)
        {
            if (fishes[j] != null)
            {
                if (fishes[j].transform.position.z < playerTransform.position.z - tileLength)
                {
                    Destroy(fishes[j]);
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

    private void SpawnFish(int prefabIndex = -1)
    {
        GameObject go;

        //Calls spawner code to create a new fish
        go = spawner.SpawnFish();
        go.transform.SetParent(transform);


        //Moves new tile into a emtpy spot in the array of fish tiles
        for (int i = 0; i < fishes.Length; i++)
        {
            if (fishes[i] == null)
            {
                fishes[i] = go;
                break;
            }
        }
        //fishes forward position is related to the location of tiles spawning
        #warning We need a random height generator for the fish's y location
        go.transform.position = Vector3.forward * spawnZ;
    }

}