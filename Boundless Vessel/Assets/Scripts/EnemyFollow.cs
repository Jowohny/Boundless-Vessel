using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public float minYLevel = 2f; // Minimum Y level for the enemy

    void Update()
    {
        // Move towards the target
        Vector3 targetPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if the new position is below the minimum Y level
        if (targetPosition.y < minYLevel)
        {
            // Set the Y position to the minimum level
            targetPosition.y = minYLevel;
        }

        // Update the enemy's position
        transform.position = targetPosition;
    }
}
