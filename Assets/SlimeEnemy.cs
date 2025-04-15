using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool movingRight = true;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.1f;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>(); // Animator is on SlimeSprite
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Movement logic
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);
        animator.SetBool("isMoving", true);

        // Check for ledge or wall to flip direction
        bool groundAhead = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        bool hittingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        if (!groundAhead || hittingWall)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        // Flip the slime visually
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1); // Deal 1 damage
            }
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Stop movement
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        
        this.enabled = false;

        
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }

        // Trigger death animation
        animator.SetTrigger("Die");

        
    }
}
