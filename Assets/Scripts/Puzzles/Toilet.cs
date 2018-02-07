using UnityEngine;

public class Toilet : InteractableObject {
    // Variables
    [Header("Plunging")]
    [Tooltip("The key that will be dropped when we plunge the toilet")]
	[SerializeField]
	private GameObject key = null;
    private bool hasDropped;

    [Tooltip("Where do we want to drop the key")]
    [SerializeField]
    private Transform dropPoint = null;

    // When something hits us
    public override void IsActivated()
    {
        base.IsActivated();
        Gaze.playerSettings.heldItem.Deactivate(); // Put down the plunger
        PlungeToilet(); // Dropping the key
    }

    // Dropping the key when we plunge the tiolet
	void PlungeToilet ()
	{
        if (!hasDropped)
        {
            PickableObject pickableObject = Instantiate(key, dropPoint.position, dropPoint.rotation).GetComponent<PickableObject>(); // Spawning the key
            pickableObject.OnPickup();
            hasDropped = true;
        }
	}
}
