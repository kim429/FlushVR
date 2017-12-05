using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Variables
	private bool invertCamera = false;

    private float xSensitivity = 1;
    private float ySensitivity = 1;

    private float xRot;
    private float yRot;
	#endregion

	 //Every frame
    private void Update()
    {
        UpdateCameraTransform();
    }

	// Update the rotation of the Camera
    private void UpdateCameraTransform()
    {
        xRot += Input.GetAxis("Mouse X") * xSensitivity;
        yRot += invertCamera ? Input.GetAxis("Mouse Y") * ySensitivity : -Input.GetAxis("Mouse Y") * ySensitivity;

        yRot = Mathf.Clamp(yRot, -90, 90);

        transform.rotation = Quaternion.Euler(yRot, xRot, 0);
    }
}