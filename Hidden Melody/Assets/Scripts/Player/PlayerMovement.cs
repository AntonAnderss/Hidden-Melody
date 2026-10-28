/* Author: Anton Andersson & Rasmus (Crouch)*/
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] InputActionReference leftInput;
    [SerializeField] bool walkingLeft = false;
    void WalkLeft(InputAction.CallbackContext obj)
    {
        if(obj.started)
        {
            walkingLeft = true;
            lastMoveDirection = -1;
        }

        if(obj.canceled)
        {
            walkingLeft = false;
        }
    }
    [SerializeField] InputActionReference rightInput;
    [SerializeField] bool walkingRight = false;
    void WalkRight(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            walkingRight = true;
            lastMoveDirection = 1;
        }

        if (obj.canceled)
        {
            walkingRight = false;
        }
    }
    [SerializeField] InputActionReference jumpInput;
    [SerializeField] bool shouldJump = false;
    void Jump(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            shouldJump = true;
        }

        if (obj.canceled)
        {
            shouldJump = false;
        }
    }
    [SerializeField] InputActionReference crouchInput;
    void Crouch(InputAction.CallbackContext obj) { crouching = !crouching; }

    [Header("")]
    [SerializeField] Vector2 movementVector;
    void DecideMovementVector()
    {
        movementVector = Vector2.zero;
        if(walkingLeft && walkingRight)
        {
             //Nothing happens
        }
        else if(walkingLeft)
        {
            movementVector += Vector2.left * CurrentSpeed;
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(walkingRight)
        {
            movementVector += Vector2.right * CurrentSpeed;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        // Jump
        if (IsGrounded && shouldJump)
        {
            movementVector += Vector2.up * jumpForce;
            grounded = false;
        }
    }
    [SerializeField] float zLevel = 0.0f;

    [Header("Speed")]
    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] float maxMovementSpeed = 5.0f;
    [SerializeField] bool running = false;
    public bool IsRunning { get { return running; } } 
    [SerializeField] float runningMultiplier = 1.2f;
    [SerializeField] bool crouching = false;
    public bool IsCrouching { get { return crouching; } }
    [SerializeField] float crouchMultiplier = 0.8f;
    float CurrentSpeed
    {
        get
        {
            if (IsCrouching)
                return movementSpeed * crouchMultiplier;
            else if(running)
                return movementSpeed * runningMultiplier;
            else 
                return movementSpeed;
        }
    }
    [SerializeField] float drag = 0.8f;

    float MaxMovementSpeed
    {
        get
        {
            if (IsCrouching)
                return maxMovementSpeed * crouchMultiplier;
            else if (running)
                return maxMovementSpeed * runningMultiplier;
            else
                return maxMovementSpeed;
        }
    }

    [Header("Crouch")]
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] float height;
    [SerializeField] Vector3 center;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 3.5f;
    public float JumpForce => jumpForce;
    [SerializeField] bool grounded = false;
    public bool IsGrounded { get { return grounded; } }

    [Header("Dash Dependencies")]
    [SerializeField] private DashController dashController = null;
    private int lastMoveDirection = 1;
    public int LastMoveDirection => lastMoveDirection;

    [Header("Unorganized")]
    [SerializeField]private Rigidbody rb;

    private void OnEnable()
    {
        leftInput.action.started += WalkLeft;
        leftInput.action.canceled += WalkLeft;
        rightInput.action.started += WalkRight;
        rightInput.action.canceled += WalkRight;
        jumpInput.action.started += Jump;
        jumpInput.action.canceled += Jump;
        crouchInput.action.started += Crouch;
    }
    private void OnDisable()
    {
        leftInput.action.started -= WalkLeft;
        leftInput.action.canceled -= WalkLeft;
        rightInput.action.started -= WalkRight;
        rightInput.action.canceled -= WalkRight;
        jumpInput.action.started -= Jump;
        jumpInput.action.canceled -= Jump;
        crouchInput.action.started -= Crouch;
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        height = capsuleCollider.height;
        center = capsuleCollider.center;
    }

    private void FixedUpdate()
    {
        //Make sure player doesn't move on z-axis
        if (transform.position.z != zLevel)
            transform.position = new Vector3(transform.position.x, transform.position.y, zLevel);

        if (dashController == null)
            dashController = GetComponentInChildren<DashController>();

        if (!dashController.IsDashing)
        {
            DecideMovementVector();

            if(movementVector.x == 0) //Isn't moving
                rb.linearVelocity = new Vector3(rb.linearVelocity.x * drag, rb.linearVelocity.y + movementVector.y, 0); //Slow down
            else
                rb.linearVelocity += new Vector3(movementVector.x, movementVector.y, 0l);

            movementVector = Vector2.zero;

            if(rb.linearVelocity.x > MaxMovementSpeed)
            {
                rb.linearVelocity = new Vector3(MaxMovementSpeed, rb.linearVelocity.y, 0);
            }
            else if(rb.linearVelocity.x < -MaxMovementSpeed)
            {
                rb.linearVelocity = new Vector3(-MaxMovementSpeed, rb.linearVelocity.y, 0);
            }
        }
        
        //Crouch
        if (IsCrouching)
        {
            capsuleCollider.height = height / 2f;

            capsuleCollider.center = new Vector3(
                center.x,
                center.y - height / 4f,
                center.z
            );
        }
        else
        {
            capsuleCollider.height = height;
            capsuleCollider.center = center;
        }
    }

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