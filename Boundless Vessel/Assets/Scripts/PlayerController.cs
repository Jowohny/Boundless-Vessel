using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PlayerState { Character, Boat, Cannon }

public class PlayerController : MonoBehaviour
{
    public GameObject cannon1, cannon2, cannon3, cannon4, cannon5, cannon6;
    public GameObject BoatObject;
    public ParticleSystem effect1, effect2, effect3, effect4, effect5, effect6;
    public ThirdPersonController characterController;
    public BoatController boatController;
    public PlaneController planeController;
    public Camera mainCamera, boatCamera;

    public GameObject crosshair; // Single crosshair object in the hierarchy
    private PlayerState currentState = PlayerState.Character;

    private GameObject activeCannon;
    private ParticleSystem activeEffect;

    private int cannonDamage = 40;
    public static float fireCooldown = 3f; // Time between each shot
    private float nextFireTime = 0f; // Tracks next available fire time
    public float shakeDuration = 0.2f, shakeMagnitude = 0.1f; // Camera shake parameters
    public float raycastDistance = 200f; // Raycast max distance

    private Transform activeCannonCameraTransform; // Reference to the cannon's camera transform

    void Start()
    {
        DisableCannon(cannon1, effect1);
        DisableCannon(cannon2, effect2);
        DisableCannon(cannon3, effect3);
        DisableCannon(cannon4, effect4);
        DisableCannon(cannon5, effect5);
        DisableCannon(cannon6, effect6);

        boatController.enabled = false;
        boatCamera.enabled = false;
        planeController.enabled = false;

        crosshair.SetActive(false); // Ensure crosshair is hidden at start
        SetPlayerState(PlayerState.Character);
    }

    void Update()
    {
        if (currentState == PlayerState.Cannon)
        {
            if (activeCannon != null)
            {
                // Get the crosshair and onTarget images
                RawImage mainCrosshair = crosshair.GetComponentsInChildren<RawImage>()[0];
                RawImage onTargetCrosshair = crosshair.GetComponentsInChildren<RawImage>()[1];

                // Ensure the main crosshair is always visible in cannon mode
                if (mainCrosshair != null)
                {
                    mainCrosshair.enabled = true;
                }

                // Perform raycast
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    // Enable "onTarget" crosshair only if the ray hits an enemy
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        if (onTargetCrosshair != null)
                        {
                            onTargetCrosshair.enabled = true;
                        }
                    }
                    else
                    {
                        if (onTargetCrosshair != null)
                        {
                            onTargetCrosshair.enabled = false;
                        }
                    }
                }
                else
                {
                    // Disable "onTarget" if no raycast hit
                    if (onTargetCrosshair != null)
                    {
                        onTargetCrosshair.enabled = false;
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentState == PlayerState.Character)
            {
                Interact();
            }
            else
            {
                ResumeCharacterControl();
            }
        }

        if (Input.GetMouseButtonDown(0) && currentState == PlayerState.Cannon)
        {
            FireCannon();
        }
    }

    void Interact()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 6f)) // Adjust reach as needed
        {
            if (hit.collider.CompareTag("Steering"))
            {
                SetPlayerState(PlayerState.Boat);
            }
            else if (hit.collider.CompareTag("Cannon"))
            {
                if (hit.collider.gameObject == cannon1) ActivateCannon(cannon1, effect1);
                else if (hit.collider.gameObject == cannon2) ActivateCannon(cannon2, effect2);
                else if (hit.collider.gameObject == cannon3) ActivateCannon(cannon3, effect3);
                else if (hit.collider.gameObject == cannon4) ActivateCannon(cannon4, effect4);
                else if (hit.collider.gameObject == cannon5) ActivateCannon(cannon5, effect5);
                else if (hit.collider.gameObject == cannon6) ActivateCannon(cannon6, effect6);
            }
        }
    }

    void ActivateCannon(GameObject cannon, ParticleSystem effect)
    {
        activeCannon = cannon;
        activeEffect = effect;

        SetPlayerState(PlayerState.Cannon);

        Camera cannonCamera = cannon.GetComponentInChildren<Camera>();
        if (cannonCamera != null)
        {
            cannonCamera.enabled = true;
            activeCannonCameraTransform = cannonCamera.transform;
        }

        crosshair.SetActive(true); // Enable single crosshair
    }

    void FireCannon()
    {
        if (Time.time < nextFireTime)
        {
            Debug.Log("Cannon is reloading...");
            return;
        }

        RaycastHit hit;
        Vector3 boxCenter = Camera.main.transform.position;
        Vector3 boxDirection = Camera.main.transform.forward;
        Vector3 boxHalfExtents = new Vector3(3f, 2f, 3f);

        if (Physics.BoxCast(boxCenter, boxHalfExtents, boxDirection, out hit, Quaternion.identity, raycastDistance))
        {
            Debug.DrawLine(boxCenter, hit.point, Color.green, 1f);
            Debug.Log("Hit: " + hit.collider.name);

            EnemyFollow enemy = hit.collider.GetComponent<EnemyFollow>();
            if (enemy != null) enemy.TakeDamage(cannonDamage);

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

        if (activeCannonCameraTransform != null) StartCoroutine(CameraShake(activeCannonCameraTransform));

        nextFireTime = Time.time + fireCooldown;
    }

    IEnumerator CameraShake(Transform cameraTransform)
    {
        Vector3 originalPosition = cameraTransform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

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
        }

        if (effect != null)
        {
            effect.Stop();
            effect.Clear();
        }

        crosshair.SetActive(false); // Disable crosshair when switching away
    }

    void SetPlayerState(PlayerState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case PlayerState.Character:
                characterController.enabled = true;
                mainCamera.enabled = true;

                boatController.enabled = false;
                boatCamera.enabled = false;
                planeController.enabled = false;

                if (activeCannon != null) DisableCannon(activeCannon, activeEffect);
                break;

            case PlayerState.Boat:
                boatController.enabled = true;
                boatCamera.enabled = true;
                planeController.enabled = true;

                characterController.enabled = false;
                mainCamera.enabled = false;

                if (activeCannon != null) DisableCannon(activeCannon, activeEffect);
                break;

            case PlayerState.Cannon:
                characterController.enabled = false;
                boatController.enabled = false;
                mainCamera.enabled = false;
                boatCamera.enabled = false;
                break;
        }
    }

    void ResumeCharacterControl()
    {
        SetPlayerState(PlayerState.Character);
    }
}
