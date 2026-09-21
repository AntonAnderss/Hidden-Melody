using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private float direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        direction = 0;

        if (Input.GetKey(KeyCode.A))
            direction -= 1;

        if (Input.GetKey(KeyCode.D))
            direction += 1;

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            direction * speed,
            rb.linearVelocity.y,
            0
        );
    }
}