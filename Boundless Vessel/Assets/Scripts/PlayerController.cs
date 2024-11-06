using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerState { Character, Boat, Cannon }

public class PlayerController : MonoBehaviour
{
    public List<GameObject> items;


    public CharacterController characterController;
    public BoatController boatController;
    public PlaneController planeController;
    public Camera mainCamera;
    public Camera boatCamera;

    private PlayerState currentState = PlayerState.Character;

    void Start()
    {
        boatController.enabled = false;
        boatCamera.enabled = false;
        planeController.enabled = false;

        foreach (GameObject item in items)
        {
            Camera cannonCamera = item.GetComponentInChildren<Camera>();
            if (cannonCamera != null)
            {
                cannonCamera.enabled = false;
            }
        }

        // Start as the character controller
        SetPlayerState(PlayerState.Character);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentState == PlayerState.Character)
            {
                Interact();
            }
            else if (currentState == PlayerState.Boat)
            {
                // If we are in the boat, pressing E should bring us back to character
                ResumeCharacterControl();
            }
            else if (currentState == PlayerState.Cannon)
            {
                // If we are in cannon, resume character as well
                ResumeCharacterControl();
            }
        }
    }

    void Interact()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 6f)) // Adjust reach as needed
        {
            if (hit.collider.CompareTag("Steering"))
            {
                SetPlayerState(PlayerState.Boat);
            }
            else if (hit.collider.CompareTag("Cannon"))
            {
                // Find the cannon in the items list
                foreach (GameObject item in items)
                {
                    if (hit.collider.gameObject == item)
                    {
                        Camera cannonCamera = item.GetComponentInChildren<Camera>();
                        if (cannonCamera != null)
                        {
                            int cannonID = cannonCamera.GetComponent<CannonCameraController>().ID;
                            Debug.Log("Cannon ID: " + cannonID);
                            SetPlayerState(PlayerState.Cannon, cannonID);
                            break;
                        }
                    }
                }
            }
        }
    }

    void SetPlayerState(PlayerState newState, int cannonID = -1)
    {

        currentState = newState;
        // We don't need the cannon ID logic here for the boat or character state
        switch (newState)
        {
            case PlayerState.Character:
                // Enable character controller and main camera
                characterController.enabled = true;
                mainCamera.enabled = true;

                boatController.enabled = false;
                boatCamera.enabled = false;
                planeController.enabled = false;

                // Disable all cannon cameras
                foreach (GameObject item in items)
                {
                    Camera cannonCamera = item.GetComponentInChildren<Camera>();
                    if (cannonCamera != null)
                    {
                        cannonCamera.enabled = false;
                    }
                }
                break;

            case PlayerState.Boat:
                // Enable boat control and camera
                boatController.enabled = true;
                boatCamera.enabled = true;
                planeController.enabled = true;

                characterController.enabled = false;
                mainCamera.enabled = false;

                // Disable all cannon cameras
                foreach (GameObject item in items)
                {
                    Camera cannonCamera = item.GetComponentInChildren<Camera>();
                    if (cannonCamera != null)
                    {
                        cannonCamera.enabled = false;
                    }
                }
                break;

            case PlayerState.Cannon:
                // Disable character, boat, and plane controllers
                characterController.enabled = false;
                boatController.enabled = false;
                mainCamera.enabled = false;
                boatCamera.enabled = false;

                // Enable specific cannon camera based on the ID
                if (cannonID >= 0 && cannonID < items.Count)
                {
                    Camera cannonCamera = items[cannonID].GetComponentInChildren<Camera>();
                    if (cannonCamera != null)
                    {
                        cannonCamera.enabled = true;
                    }
                }
                break;
        }
    }


    void ResumeCharacterControl()
    {
        // Switch back to the character control
        SetPlayerState(PlayerState.Character);
    }
}
