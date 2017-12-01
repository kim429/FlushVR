using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour {
    public float gazeRange;
    public float updateRate;

    private Coroutine gazeUpdate;
    private RaycastHit gazeHit;
    private GameObject hitObject;
    private float hitDuration;

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
            if (gazeHit.transform.gameObject == hitObject)
            {
                hitDuration += elapsedTime;
            }
            else
            {
                hitObject = gazeHit.transform.gameObject;
            }
        }
    }
}
