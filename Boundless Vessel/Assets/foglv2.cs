using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foglv2 : MonoBehaviour
{
  public Color fogColor = Color.gray;
  public float fogDensity = 0.03f;


  private void OnTriggerEnter(Collider other)
  {
      // Check if the object entering the trigger is the player
      if (other.CompareTag("Player"))
      {
          RenderSettings.fog = true;
          RenderSettings.fogColor = fogColor;
          RenderSettings.fogDensity = fogDensity;
      }
  }

}
