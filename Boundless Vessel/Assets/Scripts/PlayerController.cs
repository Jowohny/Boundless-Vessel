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
    public Camera mainCamera;
    public Camera boatCamera;

    private PlayerState currentState = PlayerState.Character;

    void Start()
    {
        boatController.enabled = false;
        boatCamera.enabled = false;

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
            else
            {
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

        switch (newState)
        {
            case PlayerState.Character:
                characterController.enabled = true;
                mainCamera.enabled = true;

                boatController.enabled = false;
                boatCamera.enabled = false;

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
                boatController.enabled = true;
                boatCamera.enabled = true;

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
                // Enable the specific cannon camera
                if (cannonID >= 0 && cannonID < items.Count)
                {
                    Camera cannonCamera = items[cannonID].GetComponentInChildren<Camera>();
                    if (cannonCamera != null)
                    {
                        cannonCamera.enabled = true;
                    }
                }

                characterController.enabled = false;
                boatController.enabled = false;
                mainCamera.enabled = false;
                boatCamera.enabled = false;
                break;
        }
    }
    void ResumeCharacterControl()
    {
        SetPlayerState(PlayerState.Character);
    }
}
