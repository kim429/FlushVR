using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour {
    public static Gaze controller;
    public static Camera mainCamera;

    public float gazeRange;
    public float updateRate;

    [HideInInspector] public RaycastHit gazeHit;

    private Coroutine gazeUpdate;
    private InteractableObject hitObject;
    private InteractableObject prevObject;

    public void Awake()
    {
        controller = this;
    }

    public void Start()
    {
        gazeUpdate = StartCoroutine(GazeUpdate());
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GazeActivate();
    }

    public IEnumerator GazeUpdate()
    {
        while (true)
        {
            GazeRaycast(updateRate);
            yield return new WaitForSeconds(updateRate);
        }
    }

    public void GazeRaycast(float elapsedTime)
    {
        if (Physics.Raycast(transform.position, transform.forward, out gazeHit))
        {
            hitObject = gazeHit.transform.GetComponent<InteractableObject>();
            prevObject = hitObject;
        }
        else
        {
            hitObject = null;
        }
    }

    public void GazeActivate()
    {
        if (prevObject)
        {
            if (IsGazingAt(prevObject))
            {
                hitObject.HitDuration += Time.deltaTime;
                if (hitObject.HitDuration >= hitObject.activationDuration)
                {
                    hitObject.IsActivated();
                }
            }
        }
    }

    public bool IsGazingAt(InteractableObject iObject)
    {
        if (hitObject && hitObject == iObject)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}