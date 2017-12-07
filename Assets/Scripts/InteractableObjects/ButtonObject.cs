using UnityEngine;

public class ButtonObject : InteractableObject {
    // Private variabled visible in the inspector
    [SerializeField] private Keypad keypad;
	[SerializeField] private char character = ' ';
	[SerializeField] private bool isClearButton;

	// Before the game starts
	void Awake ()
	{
		activationDuration = 1.5F;
	}

    // Is called from the gaze controls
    public override void IsActivated ()
	{
		if (!isClearButton) {
			keypad.AddToCombination (character);
			hitDuration = 0;
		}
		else // When we want to clear the combination
		{
			keypad.ClearCombination ();
			hitDuration = 0;
		}
	}
}
