using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DefaultExecutionOrder(-100)]

public class YMovingPlatform : MonoBehaviour
{


    public float speed = 0.5f;
    public float distance = 2f;


    private Rigidbody rb;
    public float verticalSpeed;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        startPosition = rb.position;
    }



    void FixedUpdate()
    {
        Vector3 next = startPosition + Vector3.up * Mathf.Sin(Time.fixedTime * speed) * distance;

        verticalSpeed = (next.y - rb.position.y) / Time.fixedDeltaTime;
        rb.MovePosition(next);
    }
}
