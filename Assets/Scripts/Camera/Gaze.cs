using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour {
	// Static variables
    public static Gaze controller;
    public static Camera mainCamera;

    // Private variables visible in the inspector
	[SerializeField] private LayerMask gazeMask = 8;
    [SerializeField] private float gazeRange = 5F;
    [SerializeField] private float updateRate = 0.1F;
    [SerializeField] private GameObject reticleCanvas;
    [SerializeField] private Image reticleImage;
    [SerializeField] private float reticleDefaultDistance;

    // Private variables hidden in the inspector
    private RaycastHit gazeHit;
    private InteractableObject hitObject;
    private InteractableObject lastObject;
    private Vector3 reticleScale;
    private Quaternion reticleRotation;

    // Is called when the script instance is being loaded
    public void Awake()
    {
        controller = this;
        mainCamera = Camera.main;
        reticleScale = reticleCanvas.transform.localScale;
        reticleRotation = reticleImage.transform.rotation;
        reticleDefaultDistance = mainCamera.farClipPlane;
    }

    // Is called on the frame when a script is enabled just before any of the Update methods is called the first time
    public void Start()
    {
        //StartCoroutine(GazeRayUpdate());
    }

    // Is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        GazeUpdate();
        GazeRaycast(Time.deltaTime);
    }

	// Is called at the rate of rateUpdate
    public IEnumerator GazeRayUpdate()
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
            lastObject = hitObject;
            UpdateReticle(true);
        }
        else
        {
            hitObject = null;
            UpdateReticle(false);
        }
    }

	// Call IsActivated() on the IObject that is being gazed at after the duration
    public void GazeUpdate()
    {
		if (lastObject && IsGazingAt(lastObject))
        {
        	hitObject.HitDuration += Time.deltaTime;
            if (hitObject.HitDuration >= hitObject.activationDuration)
            {
                hitObject.IsActivated();
            }
         }
    }

    public void UpdateReticle(bool isRaycasting)
    {
        if (isRaycasting)
        {
            reticleCanvas.transform.position = gazeHit.point;
            reticleCanvas.transform.localScale = reticleScale * gazeHit.distance;
        }
        else
        {
            reticleCanvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * reticleDefaultDistance;
            reticleCanvas.transform.localScale = reticleScale * reticleDefaultDistance;
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