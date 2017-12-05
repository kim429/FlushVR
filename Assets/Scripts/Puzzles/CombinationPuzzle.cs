using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationPuzzle : MonoBehaviour {

	static CombinationPuzzle combination;
	[SerializeField] private string rightCombinationString = null;
	public string inputCombinationString = null;
	[SerializeField] private bool unlocked = false;

	private void Start() {
		combination = this;
	}

	// Returns true if the input combination is the same as the correct combination
	private bool CorrectCombination ()
	{
		if (inputCombinationString == rightCombinationString) 
		{
			return true;
		}

		return false;
	}

	// Add a charachter from another script
	public static void AddToCombination (char myInput)
	{
		combination.inputCombinationString += myInput;
		combination.unlocked = combination.CorrectCombination ();
	}

	// Clears the combination
	public static void ClearCombination ()
	{
		combination.inputCombinationString = "";
	}
}
