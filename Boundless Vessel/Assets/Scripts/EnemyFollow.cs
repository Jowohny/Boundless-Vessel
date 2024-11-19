using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target; // The player's position or target
    public float speed; // Walking speed of the enemy
    public float minYLevel = 0f; // Minimum Y level for the enemy
    public float intimidationDistance = 5f; // Distance to start intimidation animation
    public float attackDistance = 1.5f; // Distance to start attack animation
    protected static int maxHealth = 100; // Default enemy health
    private int currentHealth;

    [SerializeField] FloatingHealthBar healthbar;

    protected Animator animator; // Reference to the Animator component

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthbar = GetComponentInChildren<FloatingHealthBar>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }

        if (healthbar == null)
        {
            Debug.LogError("HealthBar not found on " + gameObject.name);
        }
    }

    void Update()
    {
        // Calculate the distance to the target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackDistance)
        {
            animator.Play("Attack");
        }
        else if (distanceToTarget <= intimidationDistance)
        {
            animator.Play("Intimidate");
        }
        else
        {
            animator.Play("Walk");

            // Move towards the target position
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (targetPosition.y < minYLevel)
            {
                targetPosition.y = minYLevel;
            }

            transform.position = targetPosition;

            // Rotate towards the target
            Vector3 direction = (target.position - transform.position).normalized;
            if (direction.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animator.Play("Die");
        Destroy(gameObject, 0.1f); // Delay for animation
    }
}
