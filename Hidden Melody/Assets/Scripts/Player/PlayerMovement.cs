using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private Collider body;
    private Vector3 normalSize;
    [SerializeField]private bool grounded;
    private int lastMoveDirection = 1;
    private bool isDashing = false;
    private bool doubleJumpUsed = false;
    private bool doubleJumpReady = false;


    public InputActionReference left, right, jump, crouch;
    public float Feet { get { return body.bounds.min.y; } }
    public bool isCrouching = false;

    private void OnEnable()
    {
        //left.action.started += WalkLeft;
        //right.action.started += WalkRight;
        jump.action.started += Jump;
        crouch.action.started += Crouch;
    }

    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashDuration = 0.15f;


    private YMovingPlatform platform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<Collider>();
        normalSize = transform.localScale;
        rb.freezeRotation = true;
    }



    void Update()
    {
        float movement = 0;

        if (Input.GetKey(KeyCode.A)) movement = -speed;
        if (Input.GetKey(KeyCode.D)) movement = speed;

        //rb.linearVelocity = new Vector3(movement, rb.linearVelocity.y, 0);

        if (!isDashing)
        {
            rb.linearVelocity = new Vector3(movement,rb.linearVelocity.y,0);
        }

        if (Input.GetKey(KeyCode.A)) movement = -speed;
        if (Input.GetKey(KeyCode.D)) movement = speed;



        if (!isDashing)
        {
            rb.linearVelocity = new Vector3(movement,rb.linearVelocity.y,0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement = -speed;
            lastMoveDirection = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement = speed;
            lastMoveDirection = 1;
        }

        /*if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
            platform = null;
        }

        if (isCrouching)
            transform.localScale = new Vector3(normalSize.x, normalSize.y / 2, normalSize.z);
        else
            transform.localScale = normalSize;

        transform.position += Vector3.up * (Feet - body.bounds.min.y);
    }



    void OnCollisionStay(Collision collision)
    {
        YMovingPlatform touched = collision.gameObject.GetComponent<YMovingPlatform>();

        float lift = touched != null ? touched.verticalSpeed : 0f;

        if (rb.linearVelocity.y > lift + 0.5f)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                grounded = true;
                doubleJumpUsed = false;
                platform = touched;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
        platform = null;

    }

    // Code to handle Y platform
    void FixedUpdate()
    {
        if (grounded && platform != null)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                platform.verticalSpeed,
                0
            );
        }
    }
}