using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelmanCamera : MonoBehaviour
{
    [Header("CrowsNet Camera Settings")]
    public float rotationSpeed = 50f;  // Speed of camera rotation
    public float verticalClamp = 45f; // Maximum vertical rotation angle
    public Camera camera;  // The camera at the end of the cannon

    [Header("Camera Controls")]
    public KeyCode moveUpKey = KeyCode.Home;      // Key to move the camera up
    public KeyCode moveDownKey = KeyCode.Delete;  // Key to move the camera down
    public KeyCode moveLeftKey = KeyCode.End;     // Key to move the camera left
    public KeyCode moveRightKey = KeyCode.PageDown; // Key to move the camera right

    private float currentVerticalRotation = 0f;  // Current vertical rotation
    private float currentHorizontalRotation = 0f;  // Current horizontal rotation

    void Start()
    {
        if (camera != null)
        {
            camera.enabled = true;  // Enable camera at the start
        }
        else
        {
            Debug.LogWarning("No camera assigned to the CannonCameraController!");
        }
    }

    void Update()
    {
        if (camera != null)
        {
            HandleCameraMovement();  // Handle key-based movement of the camera
        }
    }

    void HandleCameraMovement()
    {
        // Horizontal movement (left/right) -- rotate around the Y-axis
        if (Input.GetKey(moveLeftKey))
        {
            currentHorizontalRotation -= rotationSpeed * Time.deltaTime;  // Move left
        }
        if (Input.GetKey(moveRightKey))
        {
            currentHorizontalRotation += rotationSpeed * Time.deltaTime;  // Move right
        }

        // Vertical movement (up/down) -- rotate around the X-axis
        if (Input.GetKey(moveDownKey))
        {
            currentVerticalRotation += rotationSpeed * Time.deltaTime;  // Move down
        }
        if (Input.GetKey(moveUpKey))
        {
            currentVerticalRotation -= rotationSpeed * Time.deltaTime;  // Move up
        }

        // Clamp the rotation to the set limits
        currentHorizontalRotation = Mathf.Repeat(currentHorizontalRotation, 360f);  // Keeps horizontal rotation in 360 range
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -verticalClamp, verticalClamp);

        // Create a new rotation quaternion for the desired vertical and horizontal rotations
        Quaternion targetRotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0);

        // Apply the calculated rotation to the camera's transform
        camera.transform.rotation = targetRotation;
    }
}
