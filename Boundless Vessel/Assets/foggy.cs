using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foggy : MonoBehaviour
{
    public Color fogColor = Color.gray;
    public float fogDensity = 0.03f;

    private void Start()
    {
        RenderSettings.fog = false; // Ensure fog starts off
        Debug.Log("Fog initially off");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered fog area - Turning Fog On");
            RenderSettings.fog = true;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        }
        else
        {
            Debug.Log("Non-player object entered the fog area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            RenderSettings.fog = false;
        }
    }
}
