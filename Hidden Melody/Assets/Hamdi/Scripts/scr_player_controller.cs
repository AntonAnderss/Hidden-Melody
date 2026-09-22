using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class scr_player_controller : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 4f;
    public float gravityMultiplier = 2.5f; // extra fall speed

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.29f;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Flute")]
    public bool hasFlute = false;
    public scr_flute_shooter fluteShooter;

    private Rigidbody rb;
    private float horizontalInput;

    private bool hasDoubleJumped;
    private bool jumpQueued;
    public bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                        | RigidbodyConstraints.FreezeRotationX
                        | RigidbodyConstraints.FreezeRotationY
                        | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isGrounded = groundCheck != null &&
            Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
            hasDoubleJumped = false;

        if (Input.GetButtonDown("Jump") && isGrounded)
            jumpQueued = true;

        if (hasFlute && Input.GetButtonDown("Fire1"))
            FireFlute();

        if (Mathf.Abs(horizontalInput) > 0.01f)
            facingRight = horizontalInput > 0f;

        Vector3 scale = transform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;

        if (jumpQueued)
        {
            velocity.y = jumpForce;
            jumpQueued = false;
        }

        if (velocity.y < 0f)
            velocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1f) * Time.fixedDeltaTime;

        rb.linearVelocity = velocity;
    }
    void FireFlute()
    {
        Vector3 direction = facingRight ? Vector3.right : Vector3.left;

        if (fluteShooter != null)
            fluteShooter.Shoot(direction);

        if (!isGrounded && !hasDoubleJumped)
        {
            Vector3 v = rb.linearVelocity;
            v.y = jumpForce * 1.5f;
            rb.linearVelocity = v;
            hasDoubleJumped = true;
        }
    }
}