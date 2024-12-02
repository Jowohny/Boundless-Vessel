using UnityEngine;

public class LevelTransitionTrigger : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public LevelManager levelManager; // Reference to the LevelManager script
    public BoatGUI boatGUI;

    public string triggerName; // The unique name for this specific trigger
    public string targetTag = "Boat"; // The tag to detect (in this case, "boat")

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "boat" tag
        if (other.CompareTag(targetTag))
        {
            Debug.Log($"Object with tag '{targetTag}' entered the trigger '{triggerName}'");

            // Perform the desired action for this specific trigger
            TriggerAction();
        }
    }

    void TriggerAction()
    {
        // Define actions based on the trigger's name
        switch (triggerName)
        {
            case "TriggerLVL2":
                levelManager.ActivateLevel2();
                boatGUI.Assign("TriggerLVL2");
                break;

            case "TriggerLVL3":
                levelManager.ActivateLevel3();
                boatGUI.Assign("TriggerLVL3");
                break;
            case "Obstacle":
                Debug.Log("TriggerAction: Calling GameOverScreen.SetUp()");
                Cursor.visible = true; // Makes the cursor visible
                Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
                GameOverScreen.SetUp();
                break;
        }
    }
}
