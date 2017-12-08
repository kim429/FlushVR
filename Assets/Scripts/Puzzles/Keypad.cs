using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {
    // Private variables visible in the inspector
    [SerializeField] private Text displayText;
    [SerializeField] private Image displayImage;
	[SerializeField] private string combination;
    [SerializeField] private int flashAmount;
    [SerializeField] private float flashRate;

    // Private variables hidden in the inspector
    private string input = "";
    private bool unlocked;

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
            StartCoroutine(Completed(Color.green));
        }
        else
        {
            unlocked = false;
            StartCoroutine(Completed(Color.red));
        }
    }

    public IEnumerator Completed(Color color)
    {
        Input = "";
        for (int i = 0; i < flashAmount; i++)
        {
            displayImage.color = color;
            yield return new WaitForSeconds(flashRate);
            displayImage.color = Color.white;
            yield return new WaitForSeconds(flashRate);
        }
    }
}