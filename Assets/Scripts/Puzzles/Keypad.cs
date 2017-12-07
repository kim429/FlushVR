using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {
    // Private variables visible in the inspector
    [SerializeField] private Text displayText;
    [SerializeField] private Image displayImage;
	[SerializeField] private string combination;
	[SerializeField] private bool unlocked;

    // Private variables hidden in the inspector
    private string input = "";

	// Add a charachter from another script
	public void AddToCombination (char character)
	{
		Input += character;
        displayText.text = Input;
	}

	// Clears the combination
	public void ClearCombination ()
	{
        Input = "";
        displayText.text = Input;
    }

    // get or sets input and checks the combination when the input length matches the combination
    public string Input
    {
        get
        {
            return input;
        }

        set
        {
            input = value;
            if (input.Length >= combination.Length)
            {
                CheckCombination();
            }
        }
    }

    public void CheckCombination()
    {
        if (input == combination)
        {
            unlocked = true;
            displayImage.color = Color.green;
            Input = "";
        }
        else
        {
            unlocked = false;
            displayImage.color = Color.red;
            Input = "";
        }
    }
}