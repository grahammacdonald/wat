using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eating : MonoBehaviour {
    public Animator ano;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("a"))
        {
            ano.SetBool("Test", true);
        }
        else
        {
            ano.SetBool("Test", false);
        }
		
	}
}
