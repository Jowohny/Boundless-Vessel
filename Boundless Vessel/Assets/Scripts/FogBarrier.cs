using UnityEngine;

public class DirectFollow : MonoBehaviour
{
    public Transform target; // The Transform to follow (e.g., the player)
    public Vector3 offset = Vector3.zero; // Offset from the target position

    private Quaternion initialRotation; // Store the object's initial rotation

    void Start()
    {
        // Save the object's initial rotation
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned.");
            return;
        }

        // Update the position to follow the target with the offset
        transform.position = target.position + offset;

        // Maintain the initial rotation
        transform.rotation = initialRotation;
    }
}
