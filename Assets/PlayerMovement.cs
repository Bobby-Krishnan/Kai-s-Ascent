using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 10f;

    public float fireballCooldown = 0.5f;
    private float lastFireballTime = -Mathf.Infinity;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    private bool facingRight = true;

    // === Double Jump ===
    public bool canDoubleJump = false;
    private bool hasDoubleJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level3Scene")
        {
            canDoubleJump = true;
            Debug.Log("Double Jump Unlocked!");
        }
        else
        {
            canDoubleJump = false;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
        UpdateAnimator();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            facingRight = moveInput > 0;
            sr.flipX = !facingRight;
        }
    }

    void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            hasDoubleJumped = false; // reset double jump when landing
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                hasDoubleJumped = true;
                Debug.Log("Double Jump Activated!");
            }
        }
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.X) && Time.time >= lastFireballTime + fireballCooldown)
        {
            lastFireballTime = Time.time;

            animator.SetTrigger("attack");

            float direction = facingRight ? 1f : -1f;

            Vector3 spawnPos = firePoint.position + new Vector3(direction * 0.5f, 0f, 0f);
            spawnPos.z = 0f;

            GameObject fireball = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);

            fireball.transform.localScale = new Vector3(
                Mathf.Abs(fireball.transform.localScale.x) * direction,
                fireball.transform.localScale.y,
                fireball.transform.localScale.z
            );

            Fireball fb = fireball.GetComponent<Fireball>();
            if (fb != null)
            {
                fb.SetDirection(direction);
            }
        }
    }

    void UpdateAnimator()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isRunning", moveInput != 0);
        animator.SetBool("isGrounded", isGrounded);
    }
}
