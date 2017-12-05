using UnityEngine;

public class PickableObject : InteractableObject {

	#region Variables
	[Tooltip("When we pick up the object, how far away from the camera should we hold it?")]
	[SerializeField]
	private float distanceFromCamera = 10f;

	// Distance between this GameObject and the hands of the player
	private float dist;

	// Where will our hands be
	Vector3 handPosition;
	// our rigidbody
	Rigidbody rb;
	#endregion

	// Use this for getting components
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
			handPosition = Gaze.mainCamera.transform.position + Gaze.mainCamera.transform.forward * distanceFromCamera; // Where are the hands at
			transform.LookAt (handPosition); // Look at the hands
			rb.useGravity = false; // Don't use gravity
		
			dist = Vector3.Distance (transform.position, handPosition); // Distance between this object and the hands of the player

			// Are we 2.0F units or further away from the hands
			if (dist >= 0.2f)
			{
				rb.velocity = (transform.forward * dist * 4); // Move towards the hands
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
