using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionIndicator : MonoBehaviour {
    [SerializeField] private Vector3 scale;
    [SerializeField] private Image container;
    [SerializeField] private Image action;
    [SerializeField] private float activateDistance;

    public void Start()
    {
        StartCoroutine(IndicatorUpdate());
    }

    public void Update()
    {
        transform.LookAt(Gaze.mainCamera.transform);
        transform.localScale = scale * Vector3.Distance(transform.position, Gaze.mainCamera.transform.position);
    }

    public IEnumerator IndicatorUpdate()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, Gaze.mainCamera.transform.position) < activateDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, Gaze.mainCamera.transform.position, out hit) && hit.transform == Gaze.mainCamera.transform)
                {
                    transform.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    transform.GetComponent<Canvas>().enabled = false;
                }
            }
            else
            {
                transform.GetComponent<Canvas>().enabled = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
