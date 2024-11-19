using UnityEngine;

public class LevelTransitionTrigger : MonoBehaviour
{
    public LevelManager levelManager; // Reference to the LevelManager script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (or another specific object)
        if (other.CompareTag("Boat")) // Make sure your player has the "Player" tag
        {
            // Handle level transitions based on which trigger zone is activated
            if (gameObject.name == "TriggerLVL2") // Level 1 -> Level 2
            {
                levelManager.ActivateLevel2();
            }
            else if (gameObject.name == "TriggerLVL3") // Level 2 -> Level 3
            {
                levelManager.ActivateLevel3();
            }
        }
    }
}
