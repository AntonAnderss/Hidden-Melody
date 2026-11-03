using UnityEngine;

[DefaultExecutionOrder(100)]
public class BarrelPlatform : MonoBehaviour
{
    private Rigidbody barrelRb;
    private Rigidbody playerRb;

    private Vector3 lastPosition;

    private void Start()
    {
        barrelRb = GetComponent<Rigidbody>();
        lastPosition = barrelRb.position;
    }

    private void FixedUpdate()
    {
        float barrelSpeedX =
            (barrelRb.position.x - lastPosition.x) / Time.fixedDeltaTime;

        lastPosition = barrelRb.position;

        if (playerRb != null)
        {
            Vector3 velocity = playerRb.linearVelocity;

            velocity.x = barrelSpeedX;

            playerRb.linearVelocity = velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = null;
        }
    }
}