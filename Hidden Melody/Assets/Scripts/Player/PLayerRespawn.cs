using UnityEngine;

public class PLayerRespawn : MonoBehaviour
{
    public float deathHeight = -10f;

    private Vector3 checkpoint;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkpoint = transform.position;
    }

    void Update()
    {
        if (transform.position.y < deathHeight)
            Respawn();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
            checkpoint = rb.position + Vector3.up * 0.2f;
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.position = checkpoint;
    }
}