using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    public float JumpForce => jumpForce;

    private Rigidbody rb;
    private Collider body;
    private Vector3 normalSize;
    [SerializeField]private bool grounded;
    public bool IsGrounded => grounded;
    [SerializeField] private Transform characterVisual;
    private int lastMoveDirection = 1;
    public int LastMoveDirection => lastMoveDirection;
    private DashController dashController;

    public InputActionReference left, right, jump, crouch;
    public float Feet { get { return body.bounds.min.y; } }
    public bool isCrouching = false;
    private CapsuleCollider capsuleCollider;

    private float normalColliderHeight;
    private Vector3 normalColliderCenter;
    private float movement;

    private void OnEnable()
    {
        //left.action.started += WalkLeft;
        //right.action.started += WalkRight;
        jump.action.started += Jump;
        crouch.action.started += Crouch;
    }
    private void OnDisable()
    {
        jump.action.started -= Jump;
        crouch.action.started -= Crouch;
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<Collider>();
        normalSize = transform.localScale;
        rb.freezeRotation = true;
        capsuleCollider = GetComponent<CapsuleCollider>();

        normalColliderHeight = capsuleCollider.height;
        normalColliderCenter = capsuleCollider.center;
        dashController = GetComponent<DashController>();
    }


    
    void Update()
    {
        movement = 0;

        if (Input.GetKey(KeyCode.A)) movement = -speed;
        if (Input.GetKey(KeyCode.D)) movement = speed;




        if (Input.GetKey(KeyCode.A)) movement = -speed;
        if (Input.GetKey(KeyCode.D)) movement = speed;


        if (Input.GetKey(KeyCode.A))
        {
            movement = -speed;
            lastMoveDirection = -1;
            characterVisual.localRotation = Quaternion.Euler(0f, -90f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement = speed;
            lastMoveDirection = 1;
            characterVisual.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }

        /*if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        } */

        if (isCrouching)
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
    private void FixedUpdate()
    {
        if (!dashController.IsDashing)
        {
            rb.linearVelocity = new Vector3(movement, rb.linearVelocity.y, 0);
        }
    }



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

    /* Movement */
    //void WalkLeft(InputAction.CallbackContext obj)
    //{
    //    rb.linearVelocity += new Vector3(-speed, rb.linearVelocity.y, 0);
    //}

    //void WalkRight(InputAction.CallbackContext obj)
    //{
    //    rb.linearVelocity += new Vector3(speed, rb.linearVelocity.y, 0);
    //}

    void Jump(InputAction.CallbackContext obj)
    {
        if(grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        }
    }

    void Crouch(InputAction.CallbackContext obj)
    {
        isCrouching = !isCrouching;
    
    }

}