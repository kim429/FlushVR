using UnityEngine;

public class Toilet : MonoBehaviour {
    // Variables
    [Header("Plunging")]
    [Tooltip("The key that will be dropped when we plunge the toilet")]
	[SerializeField]
	private GameObject key = null;

    [Tooltip("Where do we want to drop the key")]
    [SerializeField]
    private Transform dropPoint = null;

    // When something hits us
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.name == "Toilet Plunger") // Was it the Toilet plunger that hit us
		{
			PlungeToilet (); // Dropping the key
            Destroy (gameObject.GetComponent<Toilet>()); // Disable the toilet script so that we cant spawn 2 keys
            other.gameObject.GetComponent<PickableObject>().Deactivate(); // Put down the plunger
		}
	}

    // Dropping the key when we plunge the tiolet
	void PlungeToilet ()
	{
        PickableObject pickableObject = Instantiate(key, dropPoint.position, dropPoint.rotation).GetComponent<PickableObject>(); // Spawning the key
        pickableObject.OnPickup();
	}
}
