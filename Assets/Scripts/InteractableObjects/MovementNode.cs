using UnityEngine;
using System.Collections;

public class MovementNode : InteractableObject
{
    [Header("Movement Node")]
    // Private variables visible in the inspector
    [SerializeField] private GameObject player = null;
    [SerializeField] private float speed = 0.8f;

    // Private variables hidden in the inspector
    private GameObject previousNode;
    private bool isTraveling;
    private float travelLerp;

    private bool isFading;
    private bool isFaded;
    private float fadeLerp;

    private Vector3 initialPlayerPos;
    private float initialPlayerDistance;

    public override void Start()
    {
        base.Start();
        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend)
            rend.enabled = false;
    }

    // Called from the Gaze script
    public override void IsActivated()
    {
        base.IsActivated();

        if (Gaze.playerSettings.useTeleportMove)
        {
            Gaze.controller.StartCoroutine(Gaze.controller.FadeToBlack(transform.position));
        }
        else
        {
            isTraveling = true;
            initialPlayerPos = player.transform.position;
            initialPlayerDistance = Vector3.Distance(Gaze.mainCamera.transform.position, transform.position);
        }
    }

    // Every 0.1F secondes
    public override void Update()
    {
        base.Update();

        if (!Gaze.playerSettings.useTeleportMove && isTraveling)
        {
            player.transform.position = Vector3.Lerp(initialPlayerPos, transform.position, travelLerp);
            travelLerp += Time.deltaTime / initialPlayerDistance * speed;
            if (player.transform.position == transform.position)
            {
                isTraveling = false;
                travelLerp = 0;
            }
        }
    }
}