using UnityEngine;

public class Player3Controller : MonoBehaviour
{
    public Camera mainCamera, crowsNestCamera;
    public ThirdPersonController characterController;

    private bool isInCrowsNest = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            InteractWithCrowsNest();
        }

        if (isInCrowsNest)
        {
            SetCamera(crowsNestCamera);
        }
        else
        {
            SetCamera(mainCamera);
        }
    }

    void InteractWithCrowsNest()
    {
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, 6f))
        {
            if (hit.collider.CompareTag("Watch"))
            {
                if (!isInCrowsNest)
                {
                    characterController.enabled = false;
                    mainCamera.enabled = false;
                    isInCrowsNest = true;
                }
                else
                {
                    isInCrowsNest = false;
                    characterController.enabled = true;
                    mainCamera.enabled = true;
                }
            }
        }
    }

    void SetCamera(Camera cameraToSet)
    {
        mainCamera.enabled = cameraToSet == mainCamera;
        crowsNestCamera.enabled = cameraToSet == crowsNestCamera;
    }
}
