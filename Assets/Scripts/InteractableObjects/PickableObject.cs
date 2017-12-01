using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject {

	[Tooltip("When we pick up the object, how far away from the camera should we hold it?")]
	[SerializeField]
	private float distanceFromCamera = 10f;
	Rigidbody rb;

	private void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	private void FixedUpdate ()
	{
		PickableTick ();
	}

	// This method is called by the gaze control
	public override void IsActivated ()
	{
		active = true;
	}

	void PickableTick ()
	{
		switch (active) {
		case true:
			transform.LookAt (Camera.main.GetComponentInChildren<Transform>().position);
			if (Vector3.Distance(transform.position, Camera.main.GetComponentInChildren<Transform>().position ) >= 0.5f)
			{
				rb.AddForce (transform.forward * 5);
			} 
			else 
			{
				rb.velocity = new Vector3 (0,0,0);
			}
			break;
		case false:
			transform.parent = null;
			break;
		}
	}
}
