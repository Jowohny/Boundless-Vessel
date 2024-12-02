using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCameraController : MonoBehaviour
{
    [Header("Cannon Camera Settings")]
    public float rotationSpeed = 50f;  // Speed of camera rotation
    public float verticalClamp = 45f; // Maximum vertical rotation angle
    public float horizontalClamp = 60f; // Maximum horizontal rotation angle
    public Camera camera;  // The camera at the end of the cannon

    [Header("Player Key Bindings")]
    public KeyCode moveUpKey = KeyCode.O;    // Key to move the camera up
    public KeyCode moveDownKey = KeyCode.L;  // Key to move the camera down
    public KeyCode moveLeftKey = KeyCode.K;  // Key to move the camera left
    public KeyCode moveRightKey = KeyCode.Semicolon; // Key to move the camera right

    private float currentVerticalRotation = 0f;  // Current vertical rotation
    private float currentHorizontalRotation = 0f;  // Current horizontal rotation

    void Start()
    {
        if (camera != null)
        {
            camera.enabled = false;  // Start with the cannon camera disabled
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
        // Horizontal movement (left/right)
        if (Input.GetKey(moveLeftKey))
        {
            currentHorizontalRotation -= rotationSpeed * Time.deltaTime;  // Move left
        }
        if (Input.GetKey(moveRightKey))
        {
            currentHorizontalRotation += rotationSpeed * Time.deltaTime;  // Move right
        }

        // Vertical movement (up/down)
        if (Input.GetKey(moveDownKey))
        {
            currentVerticalRotation += rotationSpeed * Time.deltaTime;  // Move up
        }
        if (Input.GetKey(moveDownKey))
        {
            currentVerticalRotation -= rotationSpeed * Time.deltaTime;  // Move down
        }

        // Clamp the rotation to the set limits
        currentHorizontalRotation = Mathf.Clamp(currentHorizontalRotation, -horizontalClamp, horizontalClamp);
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -verticalClamp, verticalClamp);

        // Apply the rotation to the camera
        camera.transform.localEulerAngles = new Vector3(currentVerticalRotation, currentHorizontalRotation, 0);
    }
}
