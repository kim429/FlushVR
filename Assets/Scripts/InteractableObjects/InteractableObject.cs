using UnityEngine;

abstract class InteractableObject : MonoBehaviour {

	[Tooltip("Time needed to activate this component")]
	public float activationDuration = 4f;

	// This method will be called from the Gaze Interections script
	public abstract void IsActivated ();
}
