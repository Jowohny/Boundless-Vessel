using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    public Camera playerCamera;  // Reference to the player's camera
    public List<string> allowedTags; // List of tags this player can interact with
    private Interactable currentInteractable;
    private HUDController hudController;  // Reference to the player's HUDController

    void Start()
    {
        // Find the HUDController attached to the same GameObject or assign it manually
        hudController = GetComponentInChildren<HUDController>();
        if (hudController == null)
        {
            Debug.LogError("HUDController not found on the player!");
        }
    }

    void Update()
    {
        CheckInteraction(); // Check for interactables each frame

        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward); // Use player's camera for the ray
        Debug.DrawRay(ray.origin, ray.direction * playerReach, Color.red); // Visualize the ray

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            Debug.Log("Hit: " + hit.collider.name); // Debug output

            // Check if the object hit has a tag in the allowed list
            if (allowedTags.Contains(hit.collider.tag))
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (currentInteractable != null && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline(playerCamera);
                }

                if (newInteractable != null && newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        if (currentInteractable != null)
        {
            // Pass the player's camera to disable the outline
            currentInteractable.DisableOutline(playerCamera);
        }

        currentInteractable = newInteractable;
        currentInteractable.EnableOutline(playerCamera); // Pass the player's camera to enable the outline

        // Use the HUDController instance of this player to show the interaction text
        if (hudController != null)
        {
            hudController.EnableInteractionText(currentInteractable.message);
        }
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            // Pass the player's camera to disable the outline
            currentInteractable.DisableOutline(playerCamera);
            currentInteractable = null;
        }

        // Disable interaction text for this player
        if (hudController != null)
        {
            hudController.DisableInteractionText();
        }
    }

}
