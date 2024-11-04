using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform groundCheck;
    public Camera playerCamera;

    [Header("Movement Settings")]
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float smooth = 0.1f;

    [Header("Ground Check Settings")]
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Camera Settings")]
    public float lookSensitivity = 2f;

    private Vector3 velocity;
    private bool isGrounded;
    private float smoothValX, smoothValZ;
    private float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void Update()
    {
        HandleCamera();
        HandleMovement();
    }

    void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Stick to the ground
        }

        // Basic movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Horizontal rotation
        transform.Rotate(Vector3.up * mouseX);

        // Vertical rotation
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -60f, 60f); // Clamp to 60 degrees
        playerCamera.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }
}