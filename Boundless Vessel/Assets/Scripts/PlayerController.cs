using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Character, Boat }

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public MouseLook mouseLook;
    public BoatController boatController;
    public Camera mainCamera;
    public Camera boatCamera;

    private PlayerState currentState = PlayerState.Character;

    void Start()
    {
        // Start as the character controller
        characterController.enabled = true;
        mouseLook.enabled = true;
        boatController.enabled = false;
        mainCamera.enabled = true;
        boatCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case PlayerState.Character:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                }
                break;
            case PlayerState.Boat:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ResumeCharacterControl();
                }
                break;
        }
    }

    void Interact()
    {
        // Stop character movement
        characterController.enabled = false;
        mouseLook.enabled = false;

        // Change camera
        mainCamera.enabled = false;
        boatCamera.enabled = true;

        // Switch to boat controller script
        boatController.enabled = true;

        currentState = PlayerState.Boat;
    }

    void ResumeCharacterControl()
    {
        // Resume character movement
        characterController.enabled = true;
        mouseLook.enabled = true;

        // Change camera back
        mainCamera.enabled = true;
        boatCamera.enabled = false;

        // Switch back to character controller script
        boatController.enabled = false;

        currentState = PlayerState.Character;
    }
}