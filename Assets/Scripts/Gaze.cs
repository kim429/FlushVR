using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    public float gazeRange;
    public float updateRate;

    private Coroutine gazeUpdate;
    private RaycastHit gazeHit;
    public float hitDuration;

    private InteractableObject hitObject;
    private InteractableObject prevObject;

    public void Start()
    {
        gazeUpdate = StartCoroutine(GazeUpdate());/////
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
            if (hitObject == prevObject)
            {
                hitDuration += elapsedTime;
                if (hitDuration >= hitObject.activationDuration)
                {
                    hitObject.IsActivated();
                    Debug.Log("Activated");
                }
            }
            else
            {
                hitDuration = 0;
            }
        }
    }
}