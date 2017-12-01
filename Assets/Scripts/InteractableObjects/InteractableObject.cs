using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {

	[Tooltip("Time needed to activate this component")]
	public float activationDuration = 4f;

	// This will be set to "True" when the "IsActivated" is called
	protected bool active;

	// This method will be called from the Gaze Interections script
	public abstract void IsActivated ();
}
