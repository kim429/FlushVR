using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {
    // Private variables visible in the inspector
    [SerializeField] private Text displayText;
	[SerializeField] private string combination;
	[SerializeField] private bool unlocked;

    // Private variables hidden in the inspector
    private string input;

	// Returns true if the input combination is the same as the correct combination
	private bool CorrectCombination ()
	{
		if (input == combination) 
		{
			return true;
		}
		return false;
	}

	// Add a charachter from another script
	public void AddToCombination (char character)
	{
		input += character;
		unlocked = CorrectCombination ();
        displayText.text = input;
	}

	// Clears the combination
	public void ClearCombination ()
	{
		input = "";
	}
}