using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ActionIndicatorSettings
{
    public bool isContainerVisible;
    public Sprite indicatorIcon;
}

public class ActionIndicator : MonoBehaviour {
    // private variables visible in the inspector
    [SerializeField] private Vector3 scale;
    [SerializeField] private Image container;
    [SerializeField] private Image action;

    // public variables hidden in the inspector
    [HideInInspector] public Animator anim;
    [HideInInspector] public InteractableObject iObject;
    [HideInInspector] public bool isGazedAt;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Start()
    {
        if (container)
            container.enabled = iObject.indicatorSettings.isContainerVisible;

        if (iObject.indicatorSettings.indicatorIcon)
            action.sprite = iObject.indicatorSettings.indicatorIcon;
    }

    public void Update()
    {
        transform.LookAt(Gaze.mainCamera.transform);
        transform.localScale = scale * Vector3.Distance(transform.position, Gaze.mainCamera.transform.position);

        if (isGazedAt)
        {
            if (!Gaze.controller.IsGazingAt(iObject)) isGazedAt = false;
        }
    }

    public void SetGaze(bool gaze)
    {
        isGazedAt = gaze;
    }
}
