using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    // Static variables
    public static UI current;
    public static List<GazeMeter> gazeMeters = new List<GazeMeter>();

    // Public variables
    public Canvas canvas;
    public ActionIndicator aIndicatorPrefab;

    // Is called when the script instance is being loaded
    private void Awake()
    {
        current = this;
    }

    public void ReticleCompleted()
    {
        Gaze.controller.reticleAnimator.SetBool("isCompleted", false);
    }
}