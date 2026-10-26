using UnityEngine;

public class LeapController : MonoBehaviour
{

    [SerializeField] private float leapStrength = 1.75f;
    [SerializeField] private float leapCooldown = 2.5f;

    private Rigidbody rb;
    private PlayerMovement playerMovement;

    private float nextLeapTime = 0f;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }


    public void ActivateLeap()
    {

        Debug.Log("Activating leap ability");
        if(Time.time < nextLeapTime)
        {
            return;
        }

        nextLeapTime = Time.time + leapCooldown;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * playerMovement.JumpForce * leapStrength, ForceMode.Impulse);
    }
}
