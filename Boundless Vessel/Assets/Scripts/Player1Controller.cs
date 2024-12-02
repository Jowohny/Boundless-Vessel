using System.Collections;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public GameObject boatObject;
    public Camera mainCamera, boatCamera;
    public ThirdPersonController characterController;
    public BoatController boatController;
    public Transform steeringWheel;

    private bool isOnSteeringWheel = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithSteeringWheel();
        }
    }

    void InteractWithSteeringWheel()
    {
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, 6f))
        {
            if (hit.collider.CompareTag("Steering"))
            {
                // Toggle whether the player is on the steering wheel or not
                isOnSteeringWheel = !isOnSteeringWheel;

                if (isOnSteeringWheel)
                {
                    // Enable boat control and disable character control
                    EnterBoatView();
                }
                else
                {
                    // Resume character control when leaving the steering wheel
                    ExitBoatView();
                }
            }
        }
    }

    void EnterBoatView()
    {
        characterController.enabled = false;
        mainCamera.enabled = false;
        boatCamera.enabled = true;
        boatController.enabled = true;
    }

    void ExitBoatView()
    {
        characterController.enabled = true;
        mainCamera.enabled = true;
        boatCamera.enabled = false;
        boatController.enabled = false;
    }
}
