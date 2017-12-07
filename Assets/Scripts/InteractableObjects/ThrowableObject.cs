using UnityEngine;

public class ThrowableObject : PickableObject {
    // Variables
    [Header("Throw settings")]
    [Tooltip("How fast does the object need to move if we want to throw it")]
    [SerializeField]
    private float maxVelocity = 2;

    private float currentVelocity = 0;

    [Tooltip("How fast do we want to throw our ball")]
    [SerializeField]
    private float throwSpeed = 5;

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate(); // Everything the PickableObject script does in the FixedUpdate

        if (active) // Only when this object is active
        {
            currentVelocity = rb.velocity.magnitude; // Velocity x y and z combined

            if (currentVelocity > maxVelocity) // If the object goes faster than the max velocity
            {
                rb.AddForce(rb.transform.forward * throwSpeed); // Add some force to throw harder
                active = false; // We won't need to control this object anymore
            }
        }

    }
}
