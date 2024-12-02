using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text interactionText; // The text component for interaction
    [SerializeField] Canvas playerCanvas;      // The player's canvas
    [SerializeField] Camera playerCamera;      // The player's camera
    [SerializeField] int playerIndex = 1;      // Index to track which player this HUD belongs to

    void Start()
    {
        // Initially disable interaction text when the game starts
        DisableInteractionText();

        // Ensure the canvas is only enabled for the correct player
        if (playerCamera.targetTexture == null) // Only enable for the active camera
        {
            playerCanvas.gameObject.SetActive(true);
        }
        else
        {
            playerCanvas.gameObject.SetActive(false);
        }
    }

    public void EnableInteractionText(string text)
    {
        string interactButton = GetInteractButtonForPlayer();

        // Set the interaction text with the appropriate button and the provided action text
        interactionText.text = text + " (" + interactButton + ")";
        interactionText.gameObject.SetActive(true); // Enable the interaction text
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false); // Disable the interaction text
    }

    private string GetInteractButtonForPlayer()
    {
        // Return the correct button based on the player index
        switch (playerIndex)
        {
            case 1:
                return "E"; // Player 1 uses 'E'
            case 2:
                return "U"; // Player 2 uses 'U'
            case 3:
                return "PageUp"; // Player 3 uses 'PageUp'
            default:
                return "E"; // Default to 'E' if player index is undefined
        }
    }
}
