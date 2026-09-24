using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class scr_flute_note : MonoBehaviour
{
    public float pushForce = 12f;
    public float lifeTime = 2f;

    private Vector3 direction;
    private float speed;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Destroy(gameObject, lifeTime);

        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void Init(Vector3 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        scr_moveable_object movable = other.GetComponent<scr_moveable_object>();
        if (movable != null)
            movable.PushFromNote(direction, pushForce);

        Destroy(gameObject);
    }
}