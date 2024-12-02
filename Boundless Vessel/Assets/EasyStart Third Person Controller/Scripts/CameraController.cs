using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Adjust sensitivity
    public Transform playerBody; // Reference to the player's body for horizontal rotation

    private float xRotation = 0f; // Tracks vertical rotation to limit looking up/down

    [Header("Custom Input Settings")]
    public bool isPlayerOne = true; // Toggle for Player 1's default mouse controls
    public KeyCode lookUpKey = KeyCode.None;
    public KeyCode lookDownKey = KeyCode.None;
    public KeyCode lookLeftKey = KeyCode.None;
    public KeyCode lookRightKey = KeyCode.None;

    void Start()
    {
        if (isPlayerOne)
        {
            // Locks the cursor for Player 1
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (isPlayerOne)
        {
            // Mouse-based controls for Player 1
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            playerBody.Rotate(Vector3.up * mouseX); // Horizontal rotation

            xRotation -= mouseY; // Vertical rotation
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            // Keyboard-based camera movement for Player 2 and Player 3
            float horizontal = 0f;
            float vertical = 0f;

            if (Input.GetKey(lookUpKey)) vertical = -1f;
            if (Input.GetKey(lookDownKey)) vertical = 1f;
            if (Input.GetKey(lookLeftKey)) horizontal = -1f;
            if (Input.GetKey(lookRightKey)) horizontal = 1f;

            float rotationSpeed = mouseSensitivity * Time.deltaTime;

            playerBody.Rotate(Vector3.up * horizontal * rotationSpeed); // Horizontal rotation

            xRotation += vertical * rotationSpeed; // Vertical rotation
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
