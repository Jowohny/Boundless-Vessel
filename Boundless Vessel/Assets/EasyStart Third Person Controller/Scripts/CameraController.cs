using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // Variables for camera rotation
    public float mouseSensitivity = 100f; // Adjusts how sensitive the mouse input is
    public Transform playerBody; // Reference to the player's body to rotate it with the camera

    private float xRotation = 0f; // Tracks up-and-down rotation to limit vertical movement

    void Start()
    {
        // Locks the cursor to the center of the screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Ensures the cursor is hidden
    }

    void Update()
    {
        // Get mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player's body horizontally based on mouseX (left and right movement)
        playerBody.Rotate(Vector3.up * mouseX);

        // Calculate vertical rotation (looking up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f); // Prevents over-rotating up or down

        // Apply vertical rotation to the camera (up and down movement)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Only apply vertical rotation to the camera
    }
}