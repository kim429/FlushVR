using UnityEngine;

public class Keyhole : MonoBehaviour {

	private Vector3 doorRotate = new Vector3 (0, 90, 0);

	// Open the door if the key enters the keyhole
	void OnTriggerEnter (Collider other)
	{
		if (other.transform.name == "key") 
		{
			transform.parent.transform.Rotate(doorRotate);
		}
	}
}
