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
    private int lastMoveDirection = 1;
    public int LastMoveDirection => lastMoveDirection;
    private DashController dashController;

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



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<Collider>();
        normalSize = transform.localScale;
        rb.freezeRotation = true;

        dashController = GetComponent<DashController>();
    }



    void Update()
    {
        float movement = 0;

        if (Input.GetKey(KeyCode.A)) movement = -speed;
        if (Input.GetKey(KeyCode.D)) movement = speed;

        rb.linearVelocity = new Vector3(movement, rb.linearVelocity.y, 0);

        if (!dashController.IsDashing)
        {
            rb.linearVelocity = new Vector3(movement, rb.linearVelocity.y, 0);
        }

        if (Input.GetKey(KeyCode.A)) movement = -speed;
        if (Input.GetKey(KeyCode.D)) movement = speed;


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
        } */

        if (isCrouching)
            transform.localScale = new Vector3(normalSize.x, normalSize.y / 2, normalSize.z);
        else
            transform.localScale = normalSize;

        transform.position += Vector3.up * (Feet - body.bounds.min.y);
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