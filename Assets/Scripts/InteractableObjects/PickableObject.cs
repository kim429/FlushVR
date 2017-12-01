using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject {

	[Tooltip("When we pick up the object, how far away from the camera should we hold it?")]
	[SerializeField]
	private float distanceFromCamera = 10f;

	// Where will our hands be
	Vector3 handPosition;
	// our rigidbody
	Rigidbody rb;

	private void Awake ()
	{
		rb = GetComponent<Rigidbody> (); // Get the Rigidbody component
	}

	// Every fixed framerate
	private void FixedUpdate ()
	{
		Grabbing (); // Grabbing the object
	}

	// This method is called by the gaze control
	public override void IsActivated ()
	{
		active = true; // We picked it up
	}

	// Grabbing the object
	void Grabbing ()
	{
		// Did we pick it up or not
		switch (active) 
		{
		case true: // We picked it up
			handPosition = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera; // Where are the hands at
			transform.LookAt (handPosition); // Look at the hands
			rb.useGravity = false; // Don't use gravity

			// Are we 2.0F units or further away from the hands
			if (Vector3.Distance(transform.position, handPosition ) >= 0.2f)
			{
				rb.velocity = (transform.forward * Vector3.Distance(transform.position, handPosition) * 4); // Move towards the hands
			} 
			else // Are we closer than 0.2F units away from the hands
			{
				rb.velocity = new Vector3 (0,0,0); // Remove the velocity
				rb.angularVelocity = new Vector3 (0, 0, 0); // Remove the angular velocity
			}
			break;
		case false: // We didn't pick it up
			rb.useGravity = true; // Use gravity
			break;
		}
	}
}
