using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {

    [Tooltip("Time needed to activate this component")]
	public float activationDuration = 4f;

	[Tooltip("How long have we looked at the object")]
	public float hitDuration;

	// This will be set to "True" when the "IsActivated" is called
	protected bool active = true;

	// This method will be called from the Gaze Interections script
	public abstract void IsActivated ();

    public float HitDuration
    {
        get
        {
            return hitDuration;
        }

        set
        {
            if (hitDuration + value > activationDuration)
            {
                hitDuration = activationDuration;
            }
            else if (hitDuration + value < 0)
            {
                hitDuration = 0;
            }
            else
            {
                hitDuration += value;
            }
        }
    }
}
