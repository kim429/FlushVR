using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

    private int fps = 0;
    [SerializeField] private Text fpsUI;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("FpsClear", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        fps++; // Ads 1 to the fps
	}

    // Clears the fps and displays the frames in this second
    void FpsClear()
    {
        fpsUI.text = "" + fps + " fps"; // Displays the frames in this second
        fps = 0; // Resets the fps
    }
}
