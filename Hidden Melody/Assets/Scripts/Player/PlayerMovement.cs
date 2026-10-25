/* */
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] InputActionReference leftInput;
    void WalkLeft(InputAction.CallbackContext obj)
    {
        movementVector += new Vector2(-CurrentSpeed, 0);
        lastMoveDirection = -1;
    }
    [SerializeField] InputActionReference rightInput;
    void WalkRight(InputAction.CallbackContext obj)
    {
        movementVector += new Vector2(CurrentSpeed, 0);
        lastMoveDirection = 1;
    }
    [SerializeField] InputActionReference jumpInput;
    void Jump(InputAction.CallbackContext obj)
    {
        if (grounded)
        {
            movementVector += Vector2.up * jumpForce;
            grounded = false;
        }
    }
    [SerializeField] InputActionReference crouchInput;
    void Crouch(InputAction.CallbackContext obj) { crouching = !crouching; }

    [Header("")]
    [SerializeField] Vector2 movementVector;

    [Header("Speed")]
    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] bool running = false;
    [SerializeField] float runningMultiplier = 1.2f;
    [SerializeField] bool crouching = false;
    [SerializeField] float crouchMultiplier = 0.8f;
    float CurrentSpeed
    {
        get
        {
            if (crouching)
                return movementSpeed * crouchMultiplier;
            else if(running)
                return movementSpeed * runningMultiplier;
            else 
                return movementSpeed;
        }
    }

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    public float JumpForce => jumpForce;
    [SerializeField] private bool grounded = false;

    [Header("Unorganized")]
    private Rigidbody rb;
    private Collider body;
    private Vector3 normalSize;
    private int lastMoveDirection = 1;
    public int LastMoveDirection => lastMoveDirection;
    private DashController dashController;
    public float Feet { get { return body.bounds.min.y; } }

    private void OnEnable()
    {
        leftInput.action.started += WalkLeft;
        rightInput.action.started += WalkRight;
        jumpInput.action.started += Jump;
        crouchInput.action.started += Crouch;
    }
    private void OnDisable()
    {
        leftInput.action.started -= WalkLeft;
        rightInput.action.started -= WalkRight;
        jumpInput.action.started -= Jump;
        crouchInput.action.started -= Crouch;
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<Collider>();
        normalSize = transform.localScale;
        rb.freezeRotation = true;

        dashController = GetComponent<DashController>();

    }

    /*
    void Update() // Should not be used
    {
        float movement = 0;

        if (Input.GetKey(KeyCode.A)) movement = -movementSpeed;
        if (Input.GetKey(KeyCode.D)) movement = movementSpeed;

        rb.linearVelocity = new Vector3(movement, rb.linearVelocity.y, 0);

        if (!dashController.IsDashing)
        {
            rb.linearVelocity = new Vector3(movement, rb.linearVelocity.y, 0);
        }

        if (Input.GetKey(KeyCode.A)) movement = -movementSpeed;
        if (Input.GetKey(KeyCode.D)) movement = movementSpeed;


        if (Input.GetKey(KeyCode.A))
        {
            movement = -movementSpeed;
            lastMoveDirection = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement = movementSpeed;
            lastMoveDirection = 1;
        }

        /*if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        } 

        if (crouching)
            transform.localScale = new Vector3(normalSize.x, normalSize.y / 2, normalSize.z);
        else
            transform.localScale = normalSize;

        transform.position += Vector3.up * (Feet - body.bounds.min.y);
    }
    */



    /* Ground check */
    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}