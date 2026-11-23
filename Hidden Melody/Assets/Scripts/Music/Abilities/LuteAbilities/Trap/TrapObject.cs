using UnityEngine;

public class TrapObject : MonoBehaviour
{
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private float trapTimer;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();


    }

    public void Activate()
    {
        Debug.Log("Trap activate");

        rb.linearVelocity = Vector3.zero;

        rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);

        Destroy(gameObject, trapTimer);
    }
}