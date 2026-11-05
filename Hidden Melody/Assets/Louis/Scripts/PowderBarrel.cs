using UnityEngine;

public class PowderBarrel : MonoBehaviour
{
    [SerializeField] private float pushSpeed = 12f;
    [SerializeField] private float lifeTime = 15f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DashController dash = collision.gameObject.GetComponent<DashController>();
        if (dash == null || !dash.IsDashing)
        {
            return;
        }

        float direction = Mathf.Sign(transform.position.x - collision.transform.position.x);
        rb.linearVelocity = new Vector3(direction * pushSpeed, 2f, 0);
    }
}
