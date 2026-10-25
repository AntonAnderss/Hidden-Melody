using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody rb;
    private float direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            direction -= 1f;

        if (Input.GetKey(KeyCode.RightArrow))
            direction += 1f;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            direction * speed,
            rb.linearVelocity.y,
            0f
        );
    }
}
