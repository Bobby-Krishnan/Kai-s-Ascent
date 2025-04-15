using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    private float direction = 1f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * speed, 0f);
        Destroy(gameObject, lifetime); // Safety auto-destroy
    }

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit an enemy
        if (other.CompareTag("Enemy"))
        {
            // Try to damage the enemy
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Deal 1 damage
            }

            Destroy(gameObject); // Destroy fireball after hitting
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // Also destroy on walls/ground
        }
    }
}
