using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool movingRight = true;

    [Header("Detection")]
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.1f;

    [Header("Visual")]
    [SerializeField] private Transform slimeVisual;
    private Animator animator;

    private bool isDead = false;

    [Header("Flip Cooldown")]
    public float flipCooldownTime = 0.25f;
    private float flipCooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = slimeVisual.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Update cooldown timer
        flipCooldownTimer -= Time.fixedDeltaTime;

        // Move
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);
        animator.SetBool("isMoving", true);

        // Check surroundings
        bool groundAhead = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        bool hittingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        // Only flip if cooldown has passed
        if ((!groundAhead || hittingWall) && flipCooldownTimer <= 0f)
        {
            Flip();
            flipCooldownTimer = flipCooldownTime; // Reset cooldown
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = slimeVisual.localScale;
        scale.x *= -1;
        slimeVisual.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        Destroy(gameObject, 1f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        if (wallCheck != null)
            Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }
}
