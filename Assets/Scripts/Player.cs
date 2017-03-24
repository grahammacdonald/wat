using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private CharacterController	controller;
	private Vector3 			moveVector;
	private Vector3 			positionVector;

	//Player Attributes
	public  float 				red;
	public  float 				green;
	public  float 				blue;
	public  ParticleSystem 		smoke;
	private Color 				smokeColour;
    public  Color               oceanColor;
	public int 					difference;

	//FrameCounter
	private int 				framz;
	public int					smokeDelay;

	//Constants
	private float 	animationDuration 	= 4.0f;
	private float 	buoyancy 			= 0.5f;
	private float 	speed 				= 5f;
	private float 	verticalVelocity	= 0.0f;
	public float 	minWidth			= 0f;
	public float 	maxWidth			= 10f;
    private float 	health				= 100f;
	public float 	colourMult			= 5;



    private void Start () 
	{
		controller = GetComponent<CharacterController> ();
		positionVector = transform.position;

		//set colour start values
		red 	= 100f;
		green 	= 100f;
		blue 	= 100f;

		//set Player start attributes Colour is between 0 and 1
		smokeColour = new Color(1f, 1f, 1f, 1f);
		framz = 0;
        oceanColor = new Color(0, 0, 1, 1);

	}
	
	private void Update () 
	{

		//if all colours reach the bottom, and in balance, push them all back up
		if (red < colourMult + 10 && green < colourMult + 10 && blue < colourMult + 10) {
			red += Mathf.Max (colourMult, 50f);
			green += Mathf.Max (colourMult, 50f);
			blue += Mathf.Max (colourMult, 50f);

		}

		//Keep colour values between 0 and 100 to represent valid colours
		red	= Mathf.Clamp (red, 0, 100);
		green = Mathf.Clamp (green, 0, 100);
		blue = Mathf.Clamp (blue, 0, 100);

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
		smokeColour = new Color(red/100f, green/100f, blue/100f, 1f);

		//If health will effect the alpha, comment out above line and use the one below
		//smokeColour = new Color(red/100f, green/100f, blue/100f, health/100f);

		if (framz > smokeDelay) {
			var emitParams = new ParticleSystem.EmitParams ();
			//Debug.Log (emitParams.position);
			emitParams.startColor = smokeColour;
			smoke.Emit (emitParams, 1);
			framz = 0;
		}
		framz++;
		
	}

    public void EatFishColor(Color color)
    {
		//Debug.Log (color.r + " " + color.g + " " + color.b + " ");

		oceanColor += color;

		//hitting fish changes the colour attributes of the player representing colour which changes the smoke colour
		red -= colourMult * color.r;
		green -= colourMult * color.g;
		blue -= colourMult * color.b;


		//if we want to only remove the largest colour aspect of the fish use the below code instead of above.
		/*{
			float r = color.r, g = color.g, b = color.b;
			if (r > g && r > b)
				red -= colourMult * color.r;
			else if (g > r && g > b)
				green -= colourMult * color.g;
			else
				blue -= colourMult * color.b;
					
		}*/

    }

    public void AffectHealth(float healthImpact)
    {
        health += healthImpact;
    }

    //Remove Health if fish is alive at a rate of 1 unit per second
    IEnumerator removeHealth()
    {
        while (true)
        {
            if (health > 0f)
            { // if health > 0
                health -= 1f; // reduce health and wait 1 second
                yield return new WaitForSeconds(1);
            }
            else
            { // if health < 0
                yield return null;
            }
        }
    }


	//Method here will find the difference between colours and if unbalance is great enough, will take action
	private void isBalanced () {
		int colourOut;
		//is there a colour imbalance?
		if (Mathf.Max (red, green, blue) > Mathf.Min (red, green, blue) + difference) {

			if (red > green && red > blue)
				colourOut = 0;
			else if (green > red && green > blue)
				colourOut = 1;
			else
				colourOut = 2;
		} else
			return;

		//Here we can spawn monsters when the colour impalance is great enough. 
		//The colour of monster to spawn is based on the value colourOut: 0 == red, 1 == green, 2 == blue
	}
}
