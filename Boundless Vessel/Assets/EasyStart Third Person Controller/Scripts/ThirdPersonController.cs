using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Tooltip("Speed at which the character moves. It is not affected by gravity or jumping.")]
    public float velocity = 5f;
    [Tooltip("This value is added to the speed value while the character is sprinting.")]
    public float sprintAddition = 3.5f;
    [Tooltip("The higher the value, the higher the character will jump.")]
    public float jumpForce = 18f;
    [Tooltip("Stay in the air. The higher the value, the longer the character floats before falling.")]
    public float jumpTime = 0.85f;
    [Space]
    [Tooltip("Force that pulls the player down. Changing this value causes all movement, jumping and falling to be changed as well.")]
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;

    // Player states
    bool isJumping = false;
    bool isSprinting = false;
 
    // Custom key bindings for each player
    [Header("Player Key Bindings")]
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    Animator animator;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogWarning("Animator component is missing. Animations will not work.");
    }

    void Update()
    {

        // Update animations
        UpdateAnimations();

        // Handle jump input
        if (Input.GetKey(jumpKey) && cc.isGrounded)
        {
            isJumping = true;
        }

        // Movement
        HandleMovement();
        HeadHittingDetect();
    }

    void UpdateAnimations()
    {
        if (cc.isGrounded && animator != null)
        {

            float minimumSpeed = 0.1f; // Minimum speed to trigger run animation
            animator.SetBool("run", cc.velocity.magnitude > minimumSpeed);

            isSprinting = cc.velocity.magnitude > minimumSpeed && Input.GetKey(sprintKey);
            animator.SetBool("sprint", isSprinting);
        }

        if (animator != null)
            animator.SetBool("air", !cc.isGrounded);
    }

    void HandleMovement()
    {
        float velocityAddition = 0;
        if (isSprinting)
            velocityAddition = sprintAddition;

        Vector3 direction = Vector3.zero;

        // Input-based movement
        if (Input.GetKey(moveForward)) direction += transform.forward;
        if (Input.GetKey(moveBackward)) direction -= transform.forward;
        if (Input.GetKey(moveLeft)) direction -= transform.right;
        if (Input.GetKey(moveRight)) direction += transform.right;

        direction.Normalize(); // Ensure consistent movement speed when moving diagonally
        direction *= (velocity + velocityAddition) * Time.deltaTime;

        if (isJumping)
        {
            direction.y = Mathf.SmoothStep(jumpForce, jumpForce * 0.3f, jumpElapsedTime / jumpTime) * Time.deltaTime;
            jumpElapsedTime += Time.deltaTime;

            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        direction.y -= gravity * Time.deltaTime; // Apply gravity
        cc.Move(direction);
    }

    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float hitCalc = cc.height / 2f * headHitDistance;

        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }
}
