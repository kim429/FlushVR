using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerSettings
{
    public bool useTeleportMove;
    public bool useAntiAliasing;
    public GameObject previousNode;
    bool firstNode;

    public PlayerSettings()
    {
        useTeleportMove = true;
        useAntiAliasing = false;
        firstNode = true;
    }

    public void NewNode(GameObject newNode)
    {
        if (previousNode)
        {
            if (!previousNode.activeSelf)
            {
                previousNode.SetActive(true);
            }

            previousNode = newNode;

            if (previousNode.activeSelf)
            {
                previousNode.SetActive(false);
            }
        }
        else if (firstNode)
        {
            previousNode = newNode;

            if (previousNode.activeSelf)
            {
                previousNode.SetActive(false);
            }

            firstNode = false;
        }
    }
}

public class Gaze : MonoBehaviour {

    // Static variables
    public static Gaze controller;
    public static Camera mainCamera;
    public static PlayerSettings playerSettings = new PlayerSettings();

    // Public variables
    public Animator reticleAnimator;

    // Private variables visible in the inspector
    [Header("Gaze Settings")]
    [SerializeField] private LayerMask gazeMask = 8;
    [SerializeField] private float gazeRange = 200f;
    [SerializeField] private float updateRate = 0.1f;
    [SerializeField] private float indicatorRange = 5f;
    [SerializeField] private float activationRange = 5f;

    [Header("Reticle Settings")]
    [SerializeField] private GameObject reticleCanvas;
    [SerializeField] private Image reticleFill;

    [Header("Debug Settings")]
    [SerializeField] private bool mouseControlEnabled;

    // Private variables hidden in the inspector
    private RaycastHit gazeHit;
    private InteractableObject hitObject;
    private InteractableObject lastObject;
    private Vector3 reticleBaseScale;
    private float reticleBaseDistance;
    private float reticleLerp;
    private float prevRetFill;
    private float retLerpTime;
    private bool isReticleLerping;
    public List<ActionIndicator> aIndicators = new List<ActionIndicator>();

    // Public variables
    public Transform Hands;

    // Is called when the script instance is being loaded
    private void Awake()
    {
        controller = this;
        mainCamera = Camera.main;

        reticleBaseScale = reticleCanvas.transform.localScale;
        reticleBaseDistance = Vector3.Distance(mainCamera.transform.position, reticleCanvas.transform.position);
    }

    private void Start()
    {
        StartCoroutine(IndicatorCheck());
    }

    // Is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        GazeUpdate();
        GazeRaycast(3);
        MouseControl();
    }

	// Casts a raycast from the camera and sets hitObject
    public void GazeRaycast(float elapsedTime)
    {
        if (Physics.Raycast(transform.position, transform.forward, out gazeHit, gazeRange, gazeMask))
        {
            hitObject = gazeHit.transform.GetComponent<InteractableObject>();
            UpdateReticle(true);

            if (hitObject)
                lastObject = hitObject;
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
		if (lastObject && IsGazingAt(lastObject) && gazeHit.distance < activationRange)
        {
        	hitObject.HitDuration += Time.deltaTime;
            if (hitObject.HitDuration >= hitObject.activationDuration)
            {
                hitObject.IsActivated();
            }
         }

        if (IsGazing)
        {
            hitObject.indicator.SetGaze(true);
        }
    }

    // Sets the reticle position to the raycast hit point and adjusts the scale
    public void UpdateReticle(bool isRaycasting)
    {
        if (isRaycasting)
        {
            reticleCanvas.transform.position = gazeHit.point;
            reticleCanvas.transform.localScale = gazeHit.distance / reticleBaseDistance * reticleBaseScale;

            if (IsGazing)
            {
                if (reticleAnimator)
                    reticleAnimator.SetBool("isGazing", true);

                if (lastObject && lastObject != hitObject)
                {
                    isReticleLerping = true;
                    reticleLerp = 0;
                    prevRetFill = lastObject.hitDuration / lastObject.activationDuration;
                }

                float currentFill = hitObject.hitDuration / hitObject.activationDuration;
                if (!isReticleLerping)
                {
                    reticleFill.fillAmount = currentFill;
                }
                else
                {
                    reticleFill.fillAmount = Mathf.Lerp(prevRetFill, currentFill, reticleLerp);
                    reticleLerp += Time.deltaTime / retLerpTime;
                }
            }
            else
            {
                if (reticleAnimator)
                    reticleAnimator.SetBool("isGazing", false);

                if (lastObject)
                {
                    reticleFill.fillAmount = lastObject.hitDuration / lastObject.activationDuration;
                }
            }
        }
        else
        {
            reticleCanvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * reticleBaseDistance;
            reticleCanvas.transform.localScale = reticleBaseScale * reticleBaseDistance;
        }
    }

    // Returns true if the camera is looking at an IObject
    public bool IsGazing
    {
        get
        {
            return hitObject ? true : false;
        }
    }

    // Returns true when the IObject parameter is being looked at
    public bool IsGazingAt(InteractableObject iObject)
    {
        return hitObject && hitObject == iObject ? true : false;
    }

    // Mouse controls for use in editor
    public void MouseControl()
    {
        if (!mouseControlEnabled) return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float xRotation = mainCamera.transform.rotation.eulerAngles.x + -Input.GetAxis("Mouse Y");
            float yRotation = mainCamera.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X");
            mainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }

    public IEnumerator IndicatorCheck()
    {
        while (true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, indicatorRange);
            foreach (ActionIndicator indicator in aIndicators)
            {
                if (indicator.anim.isActiveAndEnabled)
                    indicator.anim.SetBool("isVisible", false);
            }
            aIndicators.Clear();

            foreach (Collider col in hitColliders)
            {
                ActionIndicator indicator = col.GetComponentInChildren<ActionIndicator>();
                if (indicator)
                {
                    aIndicators.Add(indicator);
                }
            }

            foreach (ActionIndicator indicator in aIndicators)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, indicator.transform.position, out hit) && hit.transform.gameObject == indicator.iObject.gameObject && !indicator.isGazedAt)
                {
                    if (indicator.anim.isActiveAndEnabled)
                        indicator.anim.SetBool("isVisible", true);
                }
                else
                {
                    if (indicator.anim.isActiveAndEnabled)
                        indicator.anim.SetBool("isVisible", false);
                }
            }

            yield return new WaitForSeconds(updateRate);
        }
    }
}