using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour {
	// Static Variables
    public static Gaze controller;
    public static Camera mainCamera;

    // Private variables visible in the inspector
	[SerializeField] private LayerMask gazeMask = 8;
    [SerializeField] private float gazeRange = 5F;
    [SerializeField] private float updateRate = 0.1F;


    // Private Variables hidden in the inspector
    private RaycastHit gazeHit;
    private InteractableObject hitObject;
    private InteractableObject prevObject;

    // Is called when the script instance is being loaded
    public void Awake()
    {
        controller = this;
    }

    // Is called on the frame when a script is enabled just before any of the Update methods is called the first time
    public void Start()
    {
        StartCoroutine(GazeUpdate());
        mainCamera = Camera.main;
    }

    // Is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        GazeActivate();
    }

	// Is called at the rate of rateUpdate
    public IEnumerator GazeUpdate()
    {
        while (true)
        {
            GazeRaycast(updateRate);
            yield return new WaitForSeconds(updateRate);
        }
    }

	// Casts a raycast from the camera and sets hitObject
    public void GazeRaycast(float elapsedTime)
    {
        if (Physics.Raycast(transform.position, transform.forward, out gazeHit, gazeRange, gazeMask))
        {
            hitObject = gazeHit.transform.GetComponent<InteractableObject>();
            prevObject = hitObject;
        }
        else
        {
            hitObject = null;
        }
    }

	// Call IsActivated() on the IObject that is being gazed at after the duration
    public void GazeActivate()
    {
		if (prevObject && IsGazingAt(prevObject))
        {
        	hitObject.HitDuration += Time.deltaTime;
            if (hitObject.HitDuration >= hitObject.activationDuration)
            {
                hitObject.IsActivated();
            }
         }
    }

	// Returns true when the IObject is being looked at
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