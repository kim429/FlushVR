using UnityEngine;

public class Keyhole : MonoBehaviour {

	// Open the door if the key enters the keyhole
	void OnTriggerEnter (Collider other)
	{
		if (other.transform.name == "key") 
		{
			print ("we opened the door");
			transform.parent.transform.Rotate(new Vector3 (0,90,0));
		}
	}
}
