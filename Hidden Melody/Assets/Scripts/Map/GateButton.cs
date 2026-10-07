using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
    public Rigidbody box;
    public Rigidbody player;
    public Rigidbody gate;

    public float openHeight = 4f;
    public float gateSpeed = 3f;

    private Vector3 closedPosition;
    private readonly HashSet<Collider> pressingColliders =
        new HashSet<Collider>();

    void Start()
    {
        closedPosition = gate.position;
    }

    void FixedUpdate()
    {
        Vector3 target = closedPosition;

        if (pressingColliders.Count > 0)
            target += Vector3.up * openHeight;

        gate.MovePosition(Vector3.MoveTowards(
            gate.position, target,
            gateSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody body = other.attachedRigidbody;

        if (body != null && (body == box || body == player))
            pressingColliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        pressingColliders.Remove(other);
    }
}