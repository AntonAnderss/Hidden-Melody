using System.Collections;
using UnityEngine;

public class LevitateController : MonoBehaviour
{
    [SerializeField] private float liftHeight = 0.5f;
    [SerializeField] private float liftSpeed = 2f;

    private Rigidbody rb;
    private bool levitating = false;

    [SerializeField] private float levitateDuration = 10f;

    private RigidbodyConstraints originalConstraints;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        originalConstraints = rb.constraints;
    }

    public void Levitate()
    {
        if (levitating)
            return;

        StartCoroutine(LiftObject());
    }

    private IEnumerator LiftObject()
    {
        levitating = true;

        rb.useGravity = false;

        float targetHeight = transform.position.y + liftHeight;

        while (transform.position.y < targetHeight)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x,liftSpeed,rb.linearVelocity.z);

            yield return new WaitForFixedUpdate();
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z);

        rb.constraints =RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        // Float for 10 seconds
        yield return new WaitForSeconds(levitateDuration);

        // Restore normal physics
        rb.constraints = originalConstraints;
        rb.useGravity = true;

        levitating = false;
    }
}