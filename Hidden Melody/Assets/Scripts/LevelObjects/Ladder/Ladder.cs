using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Rigidbody player;
    private bool onLadder;

    void FixedUpdate()
    {
        if (onLadder)
        {
            float movement = 0f;

            if (Input.GetKey(KeyCode.W)) movement = 3f;
            if (Input.GetKey(KeyCode.S)) movement = -3f;

            player.linearVelocity = new Vector3(player.linearVelocity.x, movement, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == player)
        {
            onLadder = true;
            player.useGravity = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == player)
        {
            onLadder = false;
            player.useGravity = true;
        }
    }
}