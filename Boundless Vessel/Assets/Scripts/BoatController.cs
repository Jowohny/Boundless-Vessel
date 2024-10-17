using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 10f;         // Speed of the boat
    public float turnSpeed = 50f;     // Turning speed
    public float stabilizationForce = 10f; // Force to stabilize the boat
    public float maxTiltAngle = 15f;  // Maximum tilt angle for the boat

    private Rigidbody rb;             // Reference to the boat's Rigidbody

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Forward movement (only when W is pressed)
        if (Input.GetKey(KeyCode.W)) // Move forward with W key
        {
            rb.AddForce(transform.forward * speed);
        }

        // Turning (only using A and D keys)
        if (Input.GetKey(KeyCode.A)) // Turn left with A key
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) // Turn right with D key
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        // Stabilization to prevent tipping
        Vector3 localRotation = transform.localEulerAngles;

        // Limit the tilt on the X-axis
        if (localRotation.x > 180) localRotation.x -= 360; // Normalize the angle
        localRotation.x = Mathf.Clamp(localRotation.x, -maxTiltAngle, maxTiltAngle);

        // Apply the stabilization force
        Quaternion targetRotation = Quaternion.Euler(localRotation.x, transform.eulerAngles.y, transform.eulerAngles.z);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, stabilizationForce * Time.fixedDeltaTime));
    }
}
