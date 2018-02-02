using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {
    // Private variables visible in the inspector
    [Header("UI")]
    [Tooltip("The Text on the display")]
    [SerializeField] private Text displayText;
    [Tooltip("The display image")]
    [SerializeField] private Image displayImage;
    [Header("Settings")]
    [Tooltip("The right combination")]
	[SerializeField] private string combination;
    [Header("Flashing")]
    [Tooltip("how many times do we want to flash")]
    [SerializeField] private int flashAmount;
    [Tooltip("How fast doe we want to flash")]
    [SerializeField] private float flashRate;
    [SerializeField] private Animator doorAnim;

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

            if (input.Length >= combination.Length) // When we have a combination of the correct length
            {
                CheckCombination(); // Checks if the combination is correct or not
            }
        }
    }

    // Checks if the combination is correct or not
    public void CheckCombination()
    {
        if (input == combination) // Correct combination
        {
            unlocked = true;
            StartCoroutine(Completed(Color.green));
        }
        else // Wront combination
        {
            unlocked = false;
            StartCoroutine(Completed(Color.red));
        }
    }

    // Flashes the light
    public IEnumerator Completed(Color color)
    {
        doorAnim.SetBool("isUnlocked", true);
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