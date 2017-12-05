using UnityEngine;

public class ButtonObject : InteractableObject {
	// Variables
	[SerializeField] private char character = ' ';
	[SerializeField] private bool isClear = false;

	// Before the game starts
	void Awake ()
	{
		activationDuration = 1.5F;
	}

	// Is called from the gaze controls
	public override void IsActivated ()
	{
		if (!isClear) // When we dont want to clear the combination
		{
			CombinationPuzzle.AddToCombination (character); // Add a character
			hitDuration = 0;
		}
		else // When we want to clear the combination
		{
			CombinationPuzzle.ClearCombination ();
		}
	}

}
