using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedFish : MonoBehaviour {

    // Use this for initialization

    private Color fishColor;

	void Start () {
        //Parameters ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax);
        fishColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GetComponent<Renderer>().material.color = fishColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            print("yes");

        }
       

    }
}
