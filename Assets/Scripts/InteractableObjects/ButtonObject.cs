using UnityEngine;

public class ButtonObject : InteractableObject {
    // Private variabled visible in the inspector
    [SerializeField] private Keypad keypad;
	[SerializeField] private char character = ' ';
	[SerializeField] private bool isClearButton;

	public override void IsActivated ()
	{
		if (!isClearButton) {
			keypad.AddToCombination (character);
			hitDuration = 0;
		}
		else
		{
			keypad.ClearCombination ();
		}
	}
}
