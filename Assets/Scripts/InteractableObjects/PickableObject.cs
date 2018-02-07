using UnityEngine;

public enum PickableObjectType
{
    NULL,
    KEY,
    PLUNGER
}

public class PickableObject : InteractableObject
    {

	#region Variables
	[Tooltip("When we pick up the object, how far away from the camera should we hold it?")]
	[SerializeField]
	private float distanceFromCamera = 10f;

    public PickableObjectType type;

    [SerializeField] protected float range;

    // Distance between this GameObject and the hands of the player
    protected float dist;

    [SerializeField] private float speed = 1f;
	// our rigidbody
	protected Rigidbody rb;
	#endregion

	// Use this for getting components
	private void Awake ()
	{
		rb = GetComponent<Rigidbody> (); // Get the Rigidbody component
	}

    public override void Start()
    {
        base.Start();
    }

    // Every fixed framerate
    protected virtual void FixedUpdate ()
	{
		Grabbing (); // Grabbing the object
	}

	// This method is called by the gaze control
	public override void IsActivated ()
	{
        base.IsActivated();
        OnPickup();
	}

    public void OnPickup()
    {
        active = true; // We picked it up
        Gaze.playerSettings.heldItem = this;
    }

    // Deactivate from other script
    public void Deactivate()
    {
        active = false; // We put it down
    }

	// Grabbing the object
    void Grabbing ()
	{
        // Did we pick it up or not
        if (active)
        {
            if (transform.parent != Gaze.controller.player.transform)
                transform.SetParent(Gaze.controller.player.transform);

            transform.LookAt(Gaze.controller.hand); // Look at the hands
            rb.useGravity = false; // Don't use gravity

            dist = Vector3.Distance(transform.position, Gaze.controller.hand.position); // Distance between this object and the hands of the player

            // Are we 2.0F units or further away from the hands
            if (dist >= 0.3F)
            {
                if (dist <= range)
                {
                    rb.velocity = (transform.forward * dist * speed); // Move towards the hands
                }
                else
                {
                    rb.velocity = (transform.forward * speed);
                }
            }
            else // Are we closer than 0.2F units away from the hands
            {
                rb.velocity = new Vector3(0, 0, 0); // Remove the velocity
                rb.angularVelocity = new Vector3(0, 0, 0); // Remove the angular velocity
                transform.rotation = Gaze.controller.hand.rotation;
                transform.position = Gaze.controller.hand.position;
            }
        }
        else
        {
			rb.useGravity = true; // Use gravity
        }
	}
}

