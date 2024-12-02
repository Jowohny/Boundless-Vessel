using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Outline outline;
    public string message;

    public UnityEvent onInteraction;

    private Camera currentHighlightingCamera; // The camera currently highlighting this interactable

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false; // Outline starts disabled
    }

    public void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name); // Debug output
        onInteraction.Invoke();
    }

    public void DisableOutline(Camera requestingCamera)
    {
        // Only disable the outline if the requesting camera is the same as the one currently highlighting
        if (currentHighlightingCamera == requestingCamera)
        {
            outline.enabled = false;
            currentHighlightingCamera = null;
            Debug.Log($"Outline Disabled for: {gameObject.name} by Camera: {requestingCamera.name}"); // Debug output
        }
    }

    public void EnableOutline(Camera requestingCamera)
    {
        // Set the outline only if it's not already enabled by another camera
        if (currentHighlightingCamera == null || currentHighlightingCamera == requestingCamera)
        {
            outline.enabled = true;
            currentHighlightingCamera = requestingCamera;
            Debug.Log($"Outline Enabled for: {gameObject.name} by Camera: {requestingCamera.name}"); // Debug output
        }
        else
        {
            Debug.LogWarning($"Attempted to enable outline for {gameObject.name} by {requestingCamera.name}, but it's already highlighted by {currentHighlightingCamera.name}");
        }
    }
}
