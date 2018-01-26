using UnityEngine;
using System.Collections;

public class MovementNode : InteractableObject
{
    [Tooltip("Movement Node")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private float speed = 0.8f;

    private bool isTraveling;
    private float travelLerp;
    private Vector3 initialPlayerPos;

    // Called from the Gaze script
    public override void IsActivated()
    {
        base.IsActivated();

        if (Gaze.playerSettings.useTeleportMove)
        {
            player.transform.position = transform.position;
        }
        else
        {
            isTraveling = true;
            initialPlayerPos = player.transform.position;
        }
    }

    // Every 0.1F secondes
    public override void Update()
    {
        base.Update();
        if (!isTraveling) return;

        player.transform.position = Vector3.Lerp(initialPlayerPos, transform.position, travelLerp);
        travelLerp += Time.deltaTime;
        if (player.transform.position == transform.position)
        {
            isTraveling = false;
            travelLerp = 0;
        }
    }
}