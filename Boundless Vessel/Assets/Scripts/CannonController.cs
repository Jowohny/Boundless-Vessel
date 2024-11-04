using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCameraController : MonoBehaviour
{
    public float rotationSpeed = 2f; // Speed of camera rotation
    public float verticalClamp = 45f; // Maximum vertical rotation angle
    public float horizontalClamp = 45f; // Maximum horizontal rotation angle
    public Camera camera;
    public int ID;

    private float currentVerticalRotation = 0f; // Current vertical rotation
    private float currentHorizontalRotation = 0f; // Current horizontal rotation

    void Start()
    {
        camera.enabled  = false;
    }

    void Update()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        // Get mouse input (or use WASD for keyboard input)
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Update current horizontal rotation and clamp it
        currentHorizontalRotation += mouseX;
        currentHorizontalRotation = Mathf.Clamp(currentHorizontalRotation, -horizontalClamp, horizontalClamp);

        // Update current vertical rotation and clamp it
        currentVerticalRotation -= mouseY;
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -verticalClamp, verticalClamp);

        // Apply rotation to the camera
        transform.localEulerAngles = new Vector3(currentVerticalRotation, currentHorizontalRotation, 0);
    }
}
