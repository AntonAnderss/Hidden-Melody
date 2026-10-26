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
        if (grounded && shouldJump)
        {
            movementVector += Vector2.up * jumpForce;
            grounded = false;
        }
    }

    [Header("Speed")]
    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float maxMovementSpeed = 5.0f;
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

    float MaxMovementSpeed
    {
        get
        {
            if (crouching)
                return maxMovementSpeed * crouchMultiplier;
            else if (running)
                return maxMovementSpeed * runningMultiplier;
            else
                return maxMovementSpeed;
        }
    }

    [Header("Crouch")]
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] float normalColliderHeight;
    [SerializeField] Vector3 normalColliderCenter;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 3.5f;
    public float JumpForce => jumpForce;
    [SerializeField] private bool grounded = false;

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

        normalColliderHeight = capsuleCollider.height;
        normalColliderCenter = capsuleCollider.center;
    }

    private void FixedUpdate()
    {
        if(dashController == null)
            dashController = GetComponentInChildren<DashController>();

        if (!dashController.IsDashing)
        {
            DecideMovementVector();
            rb.linearVelocity += new Vector3(movementVector.x, movementVector.y, 0);
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
        if (crouching)
        {
            capsuleCollider.height = normalColliderHeight / 2f;

            capsuleCollider.center = new Vector3(
                normalColliderCenter.x,
                normalColliderCenter.y - normalColliderHeight / 4f,
                normalColliderCenter.z
            );
        }
        else
        {
            capsuleCollider.height = normalColliderHeight;
            capsuleCollider.center = normalColliderCenter;
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