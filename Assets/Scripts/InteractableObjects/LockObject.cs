using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LockObject : InteractableObject {
    // Private variables that are visible in the inspector
    [Header("Lock")]
    [SerializeField] private Animator anim;

    // Private variables hidden in the inspector
    private Rigidbody rb;

    public override void IsActivated()
    {
        base.IsActivated();

        rb.isKinematic = false;
        Gaze.playerSettings.heldItem.Deactivate();

        if (anim)
        {
            anim.SetBool("isOpen", true);
        }
        else
        {
            Debug.LogWarning(name + ": No animator assigned!");
        }
    }

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}