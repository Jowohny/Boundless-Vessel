using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;

    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name); // Debug output

        // Invoke the corresponding event based on the interaction type
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
        Debug.Log("Outline Disabled for: " + gameObject.name); // Debug output
    }

    public void EnableOutline()
    {
        outline.enabled = true;
        Debug.Log("Outline Enabled for: " + gameObject.name); // Debug output
    }
}
