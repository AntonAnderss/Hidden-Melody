using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class scr_moveable_object : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    public void PushFromNote(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}