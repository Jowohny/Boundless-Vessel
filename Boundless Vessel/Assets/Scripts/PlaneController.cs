using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public BoatController boatController; // Reference to the BoatController script
    public Transform boatTransform;       // Reference to the boat's transform

    private Vector3 pivotPoint;           // Pivot point for rotation

    private void Start()
    {
        if (boatController == null || boatTransform == null)
        {
            Debug.LogError("BoatController or Boat Transform reference missing!");
            return;
        }

        // Set initial pivot point based on boat position
        pivotPoint = boatTransform.position;
    }

    private void Update()
    {
        // Update the pivot point based on the boat's position
        pivotPoint = boatTransform.position;

        // Move the plane forward or backward based on input
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(-boatTransform.forward * boatController.speed * Time.deltaTime, Space.World);
        }

        // Rotate the plane around the pivot point based on input
        if (Input.GetKey(KeyCode.D))
        {
            RotatePlaneAroundPivot(-boatController.turnSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotatePlaneAroundPivot(boatController.turnSpeed);
        }
    }

    private void RotatePlaneAroundPivot(float turnSpeed)
    {
        // Calculate rotation around the pivot point
        transform.RotateAround(pivotPoint, Vector3.up, turnSpeed * Time.deltaTime);
    }
}