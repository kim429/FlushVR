using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    public float gazeRange;
    public float updateRate;
    public Image fillMeter;

    private Coroutine gazeUpdate;
    private RaycastHit gazeHit;
    public float hitDuration;

    public InteractableObject hitObject;
    public InteractableObject prevObject;

    public void Start()
    {
        gazeUpdate = StartCoroutine(GazeUpdate());
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
            if (hitObject && hitObject == prevObject)
            {
                hitDuration += Time.deltaTime;
                if (hitDuration >= hitObject.activationDuration)
                {
                    hitObject.IsActivated();
                }
            }
            else
            {
                hitDuration -= Time.deltaTime;
            }
        }
        else
        {
            hitDuration = 0;
        }
    }
}