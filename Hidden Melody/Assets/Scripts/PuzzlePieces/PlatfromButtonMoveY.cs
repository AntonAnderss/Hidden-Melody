using UnityEngine;

public class PlatfromButtonMoveY : MonoBehaviour
{
    public Rigidbody PlatfromX;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private bool isPressed;
    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        PlatfromX.isKinematic = true;

        Vector3 position = PlatfromX.position;
        position.z = 0f;
        PlatfromX.position = position;

        startPosition = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPressed = true;
    }

    void FixedUpdate()
    {
        if (!isPressed)
            return;


        timer += Time.fixedDeltaTime * moveSpeed;
        float y = Mathf.PingPong(timer, moveDistance);
        PlatfromX.MovePosition(startPosition + Vector3.up * y);
    }
}