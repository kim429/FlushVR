using UnityEngine;

public class PickableObject : InteractableObject
    {

	#region Variables
	[Tooltip("When we pick up the object, how far away from the camera should we hold it?")]
	[SerializeField]
	private float distanceFromCamera = 10f;

    [SerializeField] protected float range;

    // Distance between this GameObject and the hands of the player
    protected float dist;

    [SerializeField] private float speed = 1f;

    [SerializeField] protected Transform hands;
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
        print(Gaze.controller);

        if (Gaze.controller.Hands != null)
        {
            hands = Gaze.controller.Hands;
        }
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
        active = true; // We picked it up
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
            transform.LookAt(hands.position); // Look at the hands
            rb.useGravity = false; // Don't use gravity

            dist = Vector3.Distance(transform.position, hands.position); // Distance between this object and the hands of the player

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
                transform.rotation = hands.rotation;
                transform.position = hands.position;
            }
        }
        else
        {
			rb.useGravity = true; // Use gravity
        }
	}
}

