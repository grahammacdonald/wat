using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTile : MonoBehaviour {

	public GameObject tilePrefab;
    public GameObject fishPrefab;

    // Use this for initialization
    void Start () {
		
	}
	

	public GameObject SpawnTile(bool fish, int timeAlive)
	{
		GameObject go;
		go = Instantiate (tilePrefab) as GameObject;
		if (fish) {
			int fishz = Random.Range (3, 7);
			for (int i = 0; i < fishz; i++)
				SpawnFish (go.transform);
				
		}

		return go;
	}

    private GameObject SpawnFish(Transform tile)
    {
        GameObject go;
        go = Instantiate(fishPrefab) as GameObject;

		//Parent of fish is the tile
		go.transform.parent = tile;

		go.transform.position = new Vector3(0, Random.Range(0, 6.0f), Random.Range(-25f, 25f));

        return go;
    }

}
