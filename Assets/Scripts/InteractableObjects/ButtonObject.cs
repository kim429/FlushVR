using UnityEngine;

public class ButtonObject : InteractableObject {
	
	[SerializeField] private char character = ' ';
	[SerializeField] private bool isClear;

	public override void IsActivated ()
	{
		if (!isClear) {
			CombinationPuzzle.AddToCombination (character);
			hitDuration = 0;
		}
		else
		{
			CombinationPuzzle.ClearCombination ();
		}
	}

}
