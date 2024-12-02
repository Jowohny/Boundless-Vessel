using UnityEngine;

public class Rendering : MonoBehaviour
{
    public Camera cameraToModify;  // The camera you want to modify
    public string layerName = "FogBarrier";  // Name of the layer to unrender

    void Start()
    {
        // Get the layer index by name
        int layer = LayerMask.NameToLayer(layerName);

        if (cameraToModify != null && layer != -1)
        {
            // Unrender the layer for this camera by excluding it from the Culling Mask
            cameraToModify.cullingMask &= ~(1 << layer);  // Remove the layer from the culling mask
        }
        else
        {
            Debug.LogWarning("Camera or Layer not found.");
        }
    }
}
