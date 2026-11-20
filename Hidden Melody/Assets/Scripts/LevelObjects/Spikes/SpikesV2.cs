using UnityEngine;

public class SpikesV2 : MonoBehaviour
{
    [SerializeField] bool killOnImpact = false;
    [SerializeField] float bounceForceY = 4;
    [SerializeField] float bounceForceX = 30;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (killOnImpact)
            {
                healthSystem.LooseHealth(15);
                healthSystem.GoBackToCheckpoint();
            }
            else
            {
                Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 bounceDirection = collision.transform.position - transform.position;
                bounceDirection.Normalize();
                playerRigidbody.AddForce(new Vector3(bounceDirection.x * bounceForceX, bounceDirection.y * bounceForceY, 0), ForceMode.Impulse);
                healthSystem.LooseHealth(10);
            }

        }
    }
}
