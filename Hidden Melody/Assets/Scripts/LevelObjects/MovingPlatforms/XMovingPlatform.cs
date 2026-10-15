using UnityEngine;

public class XMovingPlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public float distance = 2f;

    private Vector3 startPosition;
    private Rigidbody player;

    void Start()
    {
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 oldPosition = transform.position;

        transform.position = startPosition + Vector3.right * Mathf.Sin(Time.time * speed) * distance;

        if (player != null && player.linearVelocity.y <= 0.1f)
            player.position += transform.position - oldPosition;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null &&
            collision.GetContact(0).normal.y < -0.5f)
            player = collision.rigidbody;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody == player)
            player = null;
    }
}