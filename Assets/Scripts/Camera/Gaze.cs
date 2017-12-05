using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour {
	#region Variables
	// Public variables
    public static Gaze controller;
    public static Camera mainCamera;

    public float gazeRange;
    public float updateRate;

    [HideInInspector] public RaycastHit gazeHit;

	// Private variables
    private Coroutine gazeUpdate;
    private InteractableObject hitObject;
    private InteractableObject prevObject;
	#endregion

	// Before the game starts
    public void Awake()
    {
        controller = this;
    }

	// When the object is enabled
    public void Start()
    {
        gazeUpdate = StartCoroutine(GazeUpdate());
        mainCamera = Camera.main;
    }

	// Called every frame
    private void Update()
    {
        GazeActivate();
    }

	// Also called every frame
    public IEnumerator GazeUpdate()
    {
        while (true) // Keeps on going
        {
            GazeRaycast(updateRate);
            yield return new WaitForSeconds(updateRate);
        }
    }

	// Get a hitObject
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

	// Activate the hitObject when we looked at it long enough
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

	// Checks if the object we were looking at was a interactable object
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