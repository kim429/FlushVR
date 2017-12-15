using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionIndicator : MonoBehaviour {
    [SerializeField] private Vector3 scale;
    [SerializeField] private Image container;
    [SerializeField] private Image action;

    public void Update()
    {
        transform.LookAt(Gaze.mainCamera.transform);
        transform.localScale = scale * Vector3.Distance(transform.position, Gaze.mainCamera.transform.position);
    }
}
