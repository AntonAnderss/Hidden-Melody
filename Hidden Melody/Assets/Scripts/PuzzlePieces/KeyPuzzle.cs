using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPuzzle : MonoBehaviour
{
    public Transform player;
    public Transform lockCube;
    public Transform gate;
    public float gateLift = 4f;

    private bool hasKey;

    void Update()
    {
        if (!hasKey)
        {
            if (Vector3.Distance(transform.position, player.position) > 1.5f)
                return;

            hasKey = true;
        }

        // Makes the key hover aobe the player
        transform.position = player.position + Vector3.up * 2f;

        // Get into the cubes range and Hold E to open the gate
        if (Vector3.Distance(player.position, lockCube.position) < 2f
            && Keyboard.current != null
            && Keyboard.current.eKey.wasPressedThisFrame)
        {
            gate.position += Vector3.up * gateLift;
            Destroy(gameObject);
        }
    }
}