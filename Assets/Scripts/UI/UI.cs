using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public static UI current;
    public static List<GazeMeter> gazeMeters = new List<GazeMeter>();

    public Canvas canvas;
    public GazeMeter gazeMeterPrefab;

    private void Awake()
    {
        current = this;
    }
}