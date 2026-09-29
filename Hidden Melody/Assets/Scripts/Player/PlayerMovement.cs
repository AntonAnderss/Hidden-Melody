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

    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.15f;




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

        rb.linearVelocity = new Vector3(movement, rb.linearVelocity.y, 0);

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
                doubleJumpUsed = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    /* Movement */
    void WalkLeft(InputAction.CallbackContext obj)
    {
        rb.linearVelocity += new Vector3(-speed, rb.linearVelocity.y, 0);
    }

    void WalkRight(InputAction.CallbackContext obj)
    {
        rb.linearVelocity += new Vector3(speed, rb.linearVelocity.y, 0);
    }

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
    public void Dash()
    {
        StartCoroutine(DashActivate());
     
    }
    private IEnumerator DashActivate()
    {
        isDashing = true;

        rb.linearVelocity = new Vector3( dashSpeed * lastMoveDirection, rb.linearVelocity.y, 0);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }

    //public void DoubleJump()
    //{
    //    if(!grounded & !doubleJumpUsed)
    //    {
    //        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

    //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    //        doubleJumpUsed = true;
    //    }

    //}
    public void ActivateDoubleJump()
    {
        doubleJumpReady = true;
    }
}