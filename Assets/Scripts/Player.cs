using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private CharacterController	controller;
	private Vector3 			moveVector;
	private Vector3 			positionVector;

	//Player Attributes
	public float 				red;
	public float 				green;
	public float 				blue;
	public ParticleSystem 		smoke;
	private Color 				smokeColour;

	//FrameCounter
	private int 				framz;
	public int					smokeDelay;

	//Constants
	private float animationDuration = 4.0f;
	private float buoyancy 			= 0.5f;
	private float speed 			= 5;
	private float verticalVelocity	= 0.0f;
	public float minWidth			= 0f;
	public float maxWidth			= 10f;


	private void Start () 
	{
		controller = GetComponent<CharacterController> ();
		positionVector = transform.position;

		//set Player start attributes Colour is between 0 and 1
		red = 1;
		green = 1;
		blue = 1;
		smokeColour = new Color(red, green, blue, 1f);
		framz = 0;

	}
	
	private void Update () 
	{



		//testing colour change --> it works
		//smokeColour = Random.ColorHSV();

		/*
		 * Restricts player from moving left and right until the camera animation is complete
		*/

		if (Time.time < animationDuration) 
		{
			controller.Move (Vector3.forward * speed * Time.deltaTime);
			return;
		}

		moveVector = Vector3.zero;

		/*
		 * By using axis, we can easily port to differnt input devices. 
		 * It also requires less code. Inside unity, go to edit -> project settings -> Input
		 * Here you can see all the features of using the Horizontal axis.
		 * 
		 * X is for Horizontal Movement
		 * Left and Right
		*/

		//Removing horizontal movement as per Benham
		//moveVector.x = Input.GetAxis("Horizontal") * speed;



		/* 
		 * Y is for vertical movement, such as buoyancy
		 * Here we make a simple buoyancy function. When the object is not touching the ground, 
		 * every second vertical velocity is increased. Otherwise, it is constant
		*/
		moveVector.y = Input.GetAxis("Vertical") * speed;

		if (controller.isGrounded) 
		{
			verticalVelocity = 0f;
			//verticalVelocity = -0.01f;
		} else 
		{
			verticalVelocity -= buoyancy * Time.deltaTime;
		}
		moveVector.y += verticalVelocity;



		// Z is for forward movement
		moveVector.z = speed;

		controller.Move(moveVector * Time.deltaTime);

		//Clamping
		positionVector 		= transform.position;
		//Not Needed now
		//positionVector.x 	= Mathf.Clamp (positionVector.x, minWidth, maxWidth);
		positionVector.y 	= Mathf.Clamp (positionVector.y, minWidth, 2*maxWidth);
		transform.position	= positionVector;


		//Update smoke colour. The if statement allows for fewer particles to be generated, editable from unity interface.
		smokeColour = new Color(red, green, blue, 1f);

		if (framz > smokeDelay) {
			var emitParams = new ParticleSystem.EmitParams ();
			//emitParams.position = transform.position;
			Debug.Log (emitParams.position);
			emitParams.startColor = smokeColour;
			smoke.Emit (emitParams, 1);
			framz = 0;
		}
		framz++;
		
	}
}
