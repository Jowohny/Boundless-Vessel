using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 10f;         // Speed of the simulated boat movement
    public float turnSpeed = 50f;     // Turning speed
    public float maxTiltAngle = 15f;  // Maximum tilt angle for the boat
    public float stabilizationForce = 10f; // Force to stabilize the boat

    private Rigidbody rb;

    private void Start()
    {
        this.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // No actual movement logic here, just capturing input
        // Input will be used by the PlaneController to simulate movement
    }
}