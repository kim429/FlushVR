using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool invertCamera;

    public float xSensitivity = 1;
    public float ySensitivity = 1;

    private float xRot;
    private float yRot;

    private void Update()
    {
        UpdateCameraTransform();
    }

    private void UpdateCameraTransform()
    {
        xRot += Input.GetAxis("Mouse X") * xSensitivity;
        yRot += invertCamera ? Input.GetAxis("Mouse Y") * ySensitivity : -Input.GetAxis("Mouse Y") * ySensitivity;

        yRot = Mathf.Clamp(yRot, -90, 90);

        transform.rotation = Quaternion.Euler(yRot, xRot, 0);
    }
}