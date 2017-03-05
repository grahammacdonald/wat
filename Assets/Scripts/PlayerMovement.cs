using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private CharacterController	controller;
	private Vector3 			moveVector;
	private Vector3 			positionVector;

	//Constants
	private float animationDuration = 4.0f;
	private float gravity 			= 12.0f;
	private float speed 			= 5;
	private float verticalVelocity	= 0.0f;
	private float minWidth			= -2.5f;
	private float maxWidth			= 2.5f;


	private void Start () 
	{
		controller = GetComponent<CharacterController> ();
		positionVector = transform.position;
	}
	
	private void Update () 
	{

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
		moveVector.x = Input.GetAxis("Horizontal") * speed;



		/* 
		 * Y is for vertical movement, such as gravity and jumping, ducking
		 * Here we make a simple gravity function. When the object is not touching the ground, 
		 * every second vertical velocity is increased. Otherwise, it is constant
		*/

		if (controller.isGrounded) 
		{
			verticalVelocity = -0.5f;
		} else 
		{
			verticalVelocity -= gravity * Time.deltaTime;
		}
		moveVector.y = verticalVelocity;



		// Z is for forward movement
		moveVector.z = speed;

		controller.Move(moveVector * Time.deltaTime);

		//Clamping
		positionVector 		= transform.position;
		positionVector.x 	= Mathf.Clamp (positionVector.x, minWidth, maxWidth);
		transform.position	= positionVector;

		
	}
}
