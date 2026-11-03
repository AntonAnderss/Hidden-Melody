using UnityEngine;

public class BarrelFollow : MonoBehaviour
{
    [SerializeField] private Transform shark;
    [SerializeField] private float ropeLength = 6f;
    [SerializeField] private float pullSpeed = 3f;

    private Rigidbody rb;
    private float startY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startY = rb.position.y;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = rb.position;

        float distance = Mathf.Abs(shark.position.x - rb.position.x);

        if (distance > ropeLength)
        {
            float direction = Mathf.Sign(shark.position.x - rb.position.x);

            newPosition.x += direction * pullSpeed * Time.fixedDeltaTime;
            newPosition.y = startY;

            rb.MovePosition(newPosition);
        }
    }
}