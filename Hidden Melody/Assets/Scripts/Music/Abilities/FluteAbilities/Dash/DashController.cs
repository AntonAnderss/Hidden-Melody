using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class DashController : MonoBehaviour
{

    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 2.5f;

    private Rigidbody rb;
    private PlayerMovement playerMovement;

    private bool isDashing = false;
    private float nextDashTime = 0f;
    private int dashDirection;
    public bool IsDashing => isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    public void Dash()
    {
        if(isDashing || Time.time < nextDashTime)
        {
            return;
        }

        nextDashTime = Time.time + dashCooldown;

        dashDirection = playerMovement.LastMoveDirection;

        StartCoroutine(DashActivate());

    }

    private IEnumerator DashActivate()
    {
        isDashing = true;
        float timer = 0f;

        while (timer < dashDuration)
        {
            rb.linearVelocity = new Vector3(dashSpeed * playerMovement.LastMoveDirection, rb.linearVelocity.y, 0);
            timer += Time.fixedDeltaTime;
            yield return new WaitForSeconds(dashDuration);
        }
        isDashing = false;
        
    }


}
