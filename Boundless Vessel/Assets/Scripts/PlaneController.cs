using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public BoatController boatController;
    public Transform boatTransform;

    private Vector3 pivotPoint;
    private float angularVelocity = 0f;
    private float currentSpeed = 0f;

    public float turnAcceleration = 2f;
    public float forwardAcceleration = 5f;
    public float forwardDeceleration = 3f;
    public float maxTurnSpeed = 30f;
    public float maxForwardSpeed = 10f;

    public float bobbingAmplitude = 0.1f; // Subtle bobbing
    public float bobbingFrequency = 0.5f; // Slower bobbing
    public float turbulenceIntensity = 0.5f; // Reduced turbulence
    public float turbulenceSpeed = 0.2f; // Slower turbulence

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        if (boatController == null || boatTransform == null)
        {
            Debug.LogError("BoatController or Boat Transform reference missing!");
            return;
        }

        pivotPoint = boatTransform.position;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        pivotPoint = boatTransform.position;

        if (Input.GetKey(KeyCode.W))
            currentSpeed = Mathf.Clamp(currentSpeed + forwardAcceleration * Time.deltaTime, 0, maxForwardSpeed);
        else
            currentSpeed = Mathf.Max(currentSpeed - forwardDeceleration * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.A))
            angularVelocity = Mathf.Clamp(angularVelocity + turnAcceleration * Time.deltaTime, -maxTurnSpeed, maxTurnSpeed);
        else if (Input.GetKey(KeyCode.D))
            angularVelocity = Mathf.Clamp(angularVelocity - turnAcceleration * Time.deltaTime, -maxTurnSpeed, maxTurnSpeed);
        else
        {
            if (angularVelocity > 0)
                angularVelocity = Mathf.Max(angularVelocity - turnAcceleration * Time.deltaTime, 0);
            else if (angularVelocity < 0)
                angularVelocity = Mathf.Min(angularVelocity + turnAcceleration * Time.deltaTime, 0);
        }

        float bobbingOffset = Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude;
        Vector3 bobbingPosition = new Vector3(0, bobbingOffset, 0);

        if (currentSpeed > 0.01f)
            transform.Translate(-boatTransform.forward * currentSpeed * Time.deltaTime, Space.World);

        if (Mathf.Abs(angularVelocity) > 0.01f)
            RotatePlaneAroundPivot(angularVelocity);

        float turbulenceX = Mathf.PerlinNoise(Time.time * turbulenceSpeed, 0) - 0.5f;
        float turbulenceZ = Mathf.PerlinNoise(0, Time.time * turbulenceSpeed) - 0.5f;
        Quaternion turbulence = Quaternion.Euler(
            turbulenceX * turbulenceIntensity,
            0,
            turbulenceZ * turbulenceIntensity
        );

        transform.position += bobbingPosition;
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * turbulence, Time.deltaTime);
    }

    private void RotatePlaneAroundPivot(float turnSpeed)
    {
        transform.RotateAround(pivotPoint, Vector3.up, turnSpeed * Time.deltaTime);
    }
}
