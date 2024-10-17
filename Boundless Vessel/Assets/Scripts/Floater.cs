using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    public int floaterCount = 1;

    public float waterDrag = 0.99f;

    public float waterAngularDrag = 0.5f;

    public float buoyancyForce = 10f; // New variable to control the buoyancy force

    private float initialYPosition; // Store the initial Y position of the boat

    private void Start()
    {
        // Store the initial Y position of the boat
        initialYPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        // Calculate the wave height at the boat's position
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);

        // Calculate the buoyancy force based on the wave height and the boat's position
        float buoyancyMultiplier = Mathf.Clamp01(waveHeight - initialYPosition) / depthBeforeSubmerged;
        float buoyancy = buoyancyMultiplier * buoyancyForce;

        // Apply the buoyancy force to the boat
        rigidBody.AddForceAtPosition(new Vector3(0f, buoyancy, 0f), transform.position, ForceMode.Acceleration);

        // Apply the water drag and angular drag
        rigidBody.AddForce(-rigidBody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        rigidBody.AddTorque(-rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}