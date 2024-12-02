using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    public GameObject cannon1, cannon2, cannon3, cannon4, cannon5, cannon6;
    public ParticleSystem effect1, effect2, effect3, effect4, effect5, effect6;
    public GameObject crosshair; // Crosshair UI element

    private GameObject activeCannon;
    private ParticleSystem activeEffect;

    private int cannonDamage = 40;
    public static float fireCooldown = 3f;
    private float nextFireTime = 0f;
    public float raycastDistance = 200f; // Raycast distance to hit objects

    public Camera playerCamera;
    public MonoBehaviour thirdPersonController; // Reference to the ThirdPersonController script
    private Transform activeCannonCameraTransform; // Cannon camera transform
    private Camera activeCamera; // The active camera currently in use

    private bool isControllingCannon = false; // To track if the player is in cannon control mode

    void Start()
    {
        // Disable all cannons initially
        DisableCannon(cannon1, effect1);
        DisableCannon(cannon2, effect2);
        DisableCannon(cannon3, effect3);
        DisableCannon(cannon4, effect4);
        DisableCannon(cannon5, effect5);
        DisableCannon(cannon6, effect6);

        crosshair.SetActive(false); // Ensure crosshair is hidden at the start
    }

    void Update()
    {
        // Handle interaction with the cannon
        if (Input.GetKeyDown(KeyCode.U)) // Player 2 presses "U" to interact with a cannon
        {
            if (isControllingCannon)
            {
                ExitCannonControl(); // Exit cannon mode
            }
            else
            {
                InteractWithCannon(); // Attempt to enter cannon mode
            }
        }

        // Handle firing logic (mouse click to fire the cannon)
        if (isControllingCannon && Input.GetMouseButtonDown(0)) // Left mouse button
        {
            FireCannon();
        }

        // Update the crosshair position relative to the active camera
        UpdateCrosshairPosition();
    }

    void InteractWithCannon()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, 6f))
        {
            if (hit.collider.CompareTag("Cannon"))
            {
                if (hit.collider.gameObject == cannon1) ActivateCannon(cannon1, effect1);
                else if (hit.collider.gameObject == cannon2) ActivateCannon(cannon2, effect2);
                else if (hit.collider.gameObject == cannon3) ActivateCannon(cannon3, effect3);
                else if (hit.collider.gameObject == cannon4) ActivateCannon(cannon4, effect4);
                else if (hit.collider.gameObject == cannon5) ActivateCannon(cannon5, effect5);
                else if (hit.collider.gameObject == cannon6) ActivateCannon(cannon6, effect6);

                // Enter cannon control mode
                EnterCannonControl();
            }
        }
    }

    void ActivateCannon(GameObject cannon, ParticleSystem effect)
    {
        activeCannon = cannon;
        activeEffect = effect;

        Camera cannonCamera = cannon.GetComponentInChildren<Camera>();
        if (cannonCamera != null)
        {
            cannonCamera.enabled = true;
            activeCannonCameraTransform = cannonCamera.transform;
            activeCamera = cannonCamera; // Set the active camera to the cannon's camera
        }
    }

    void EnterCannonControl()
    {
        isControllingCannon = true;
        crosshair.SetActive(true); // Enable the crosshair UI

        // Disable player movement
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = false;
        }

        // Enable the CannonCameraController
        if (activeCannon != null)
        {
            var cannonController = activeCannon.GetComponent<CannonCameraController>();
            if (cannonController != null)
            {
                cannonController.enabled = true;
            }
        }
    }

    void ExitCannonControl()
    {
        isControllingCannon = false;
        crosshair.SetActive(false); // Hide the crosshair UI

        // Enable player movement
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = true;
        }

        // Disable the CannonCameraController
        if (activeCannon != null)
        {
            var cannonController = activeCannon.GetComponent<CannonCameraController>();
            if (cannonController != null)
            {
                cannonController.enabled = false;
            }

            // Disable the cannon's camera
            Camera cannonCamera = activeCannon.GetComponentInChildren<Camera>();
            if (cannonCamera != null)
            {
                cannonCamera.enabled = false;
            }
        }

        // Revert back to player camera
        activeCamera = playerCamera;
        activeCannon = null;
        activeEffect = null;
    }

    void UpdateCrosshairPosition()
    {
        if (activeCamera != null)
        {
            crosshair.transform.SetParent(activeCamera.transform, false);
            crosshair.transform.localPosition = new Vector3(0f, 0f, 0.1f); // Slightly forward
        }
    }

    void FireCannon()
    {
        if (Time.time < nextFireTime)
        {
            Debug.Log("Cannon is reloading...");
            return;
        }

        RaycastHit hit;
        Vector3 boxCenter = activeCamera.transform.position;
        Vector3 boxDirection = activeCamera.transform.forward;
        Vector3 boxHalfExtents = new Vector3(3f, 2f, 3f);

        if (Physics.BoxCast(boxCenter, boxHalfExtents, boxDirection, out hit, Quaternion.identity, raycastDistance))
        {
            Debug.DrawLine(boxCenter, hit.point, Color.green, 1f);
            Debug.Log("Hit: " + hit.collider.name);

            EnemyFollow enemy = hit.collider.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.TakeDamage(cannonDamage);
            }

            if (activeEffect != null)
            {
                activeEffect.transform.position = hit.point;
                activeEffect.Play();
            }
        }
        else
        {
            Debug.DrawLine(boxCenter, boxCenter + boxDirection * raycastDistance, Color.red, 1f);
            Debug.Log("Missed!");
        }

        if (activeCannonCameraTransform != null)
            StartCoroutine(CameraShake(activeCannonCameraTransform));

        nextFireTime = Time.time + fireCooldown;
    }

    IEnumerator CameraShake(Transform cameraTransform)
    {
        Vector3 originalPosition = cameraTransform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < 0.2f) // Shake duration
        {
            float offsetX = Random.Range(-0.1f, 0.1f);
            float offsetY = Random.Range(-0.1f, 0.1f);

            cameraTransform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y + offsetY,
                originalPosition.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }

    void DisableCannon(GameObject cannon, ParticleSystem effect)
    {
        if (cannon != null)
        {
            Camera cannonCamera = cannon.GetComponentInChildren<Camera>();
            if (cannonCamera != null) cannonCamera.enabled = false;

            var cannonController = cannon.GetComponent<CannonCameraController>();
            if (cannonController != null) cannonController.enabled = false;
        }

        if (effect != null) effect.Stop();
    }
}
