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
	private void CorrectCombination ()
	{
		if (inputCombinationString == rightCombinationString) 
		{
			combination.unlocked = true;
		}

		if (combination.unlocked) 
		{
			print ("We unlocked this");
		}
	}

	// Add a charachter from another script
	public static void AddToCombination (char myInput)
	{
		combination.inputCombinationString += myInput;
		combination.CorrectCombination ();
	}

	// Clears the combination
	public static void ClearCombination ()
	{
		combination.inputCombinationString = "";
	}
}
