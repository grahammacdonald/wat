using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Transform	lookAt;
	private Vector3 	startOffset;
	private Vector3 	moveVector;

	//Constants
	private float animationDuration = 4.0f;
	private float transition 		= 0.0f;
	private Vector3 animationOffset	= new Vector3 (0, 3, 2);

	// Max and min height clamp of Camera. Relevant for stair climbing, jumping etc.
	private int heightMax = 5;
	private int heightMin = 3;


	private void Start () 
	{
		/*
		 * The original location of the camera is stored. That way, the height and distance between
		 * the camera and the player is maintained. As moveVector is tracking the player, the camera is
		 * "startOffset" displacement away from the player.
		*/

		lookAt 		= GameObject.FindGameObjectWithTag ("Player").transform;
		startOffset	= transform.position - lookAt.position;
	}

	private void Update () 
	{
		moveVector 		= lookAt.position + startOffset;
		moveVector.x	= 0;


		// Keep the height of the camera restricted between a max and min

		moveVector.y = Mathf.Clamp(moveVector.y, heightMin, heightMax);

		moveVector.x = Mathf.Clamp(moveVector.x, 4, 7);


		// transition value starts at 0 and reaches 1 when start camera animation is complete

		if (transition > 1.0f) 
		{
			transform.position = moveVector;
		} else 
		{
			/*
				Interpolate between animation initial (position and rotation) and final;
			*/
			transform.position = Vector3.Lerp (moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * (1 / animationDuration);
			transform.LookAt (lookAt.position);
		}

	}
}
