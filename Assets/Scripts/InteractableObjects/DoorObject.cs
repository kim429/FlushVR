using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : InteractableObject {
    // Serialized private variables
    [SerializeField] private Animator animator;
    [SerializeField] private MovementNode advanceNode;

    // Non-serialized private variables
    private bool isOpen;

    public override void IsActivated()
    {
        base.IsActivated();
        Open();
    }

    public override void Start()
    {
        base.Start();
        if (!advanceNode) return;

        advanceNode.gameObject.SetActive(isOpen);
    }

    // Sets the 'isOpen' bool in the animator to the appropriate value
    public void Open()
    {
        if (!animator) { Debug.LogWarning("No animator found on : " + transform.name); return; }

        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);

        if (advanceNode)
        {
            advanceNode.gameObject.SetActive(isOpen);
        }
    }
}
