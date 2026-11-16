using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DefaultExecutionOrder(100)]
public class PlatformCarry : MonoBehaviour
{
    private Rigidbody platform;
    private Rigidbody player;
    private PlayerMovement playerMovement;
    private DashController dash;
    private float offsetX;

    private readonly HashSet<Collider> supports = new HashSet<Collider>();

    void OnEnable()
    {
        platform = GetComponent<Rigidbody>();
        player = null;
        supports.Clear();
    }

    void FixedUpdate()
    {
        if (player == null || supports.Count == 0)
            return;

        if (!playerMovement.enabled)
            playerMovement.ResetMovementInput();

        Vector3 velocity = player.linearVelocity;
        bool walking = playerMovement.enabled && playerMovement.IsWalking;
        bool dashing = dash != null && dash.IsDashing;

        if (walking || dashing || velocity.y > 0.1f)
        {
            offsetX = player.position.x - platform.position.x;
            return;
        }

        Vector3 position = player.position;
        position.x = platform.position.x + offsetX;
        player.position = position;

        velocity.x = platform.linearVelocity.x;
        player.linearVelocity = velocity;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody == null)
            return;

        PlayerMovement movement =
            collision.rigidbody.GetComponent<PlayerMovement>();

        if (movement == null)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                if (player != collision.rigidbody || supports.Count == 0)
                {
                    player = collision.rigidbody;
                    playerMovement = movement;
                    dash = player.GetComponentInChildren<DashController>();
                    offsetX = player.position.x - platform.position.x;
                }

                supports.Add(collision.collider);
                return;
            }
        }

        supports.Remove(collision.collider);
    }

    void OnCollisionExit(Collision collision)
    {
        supports.Remove(collision.collider);
    }
}