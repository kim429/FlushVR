using UnityEngine;

public class MovementNode : InteractableObject {
	#region Variables
	[Tooltip("Our Player")]
    [SerializeField]
	private GameObject player = null;
	[SerializeField]
	private float speed = 0.8f;
	#endregion

	// Called from the Gaze script
   public override void IsActivated()
    {
		active = true;
    }

	// Every 0.1F secondes
	public override void Update ()
	{
		player.transform.Translate(transform.position * speed * Time.deltaTime, Space.World);
	}
}