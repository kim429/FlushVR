using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeMeter : MonoBehaviour {
    public Image fillImage;
    public InteractableObject iObject;

    public GazeMeter()
    {
        UI.gazeMeters.Add(this);
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(iObject.transform.position);
    }
}