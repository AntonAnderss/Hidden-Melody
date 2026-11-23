using UnityEngine;

public class PlatfromButtonMoveX : MonoBehaviour
{
    public Rigidbody PlatfromX;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private bool isPressed;
    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        startPosition = PlatfromX.position;
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

        float x = Mathf.PingPong(timer, moveDistance);
        PlatfromX.MovePosition(startPosition + Vector3.right * x);
    }
}