using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPuzzle : MonoBehaviour
{
    public Transform player;
    public Transform lockCube;
    public Transform gate;
    public float gateLift = 4f;

    private bool hasKey;


    public InputActionReference interact;

    void OnEnable()
    {
        interact.action.Enable();
    }


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
           && interact.action.WasPressedThisFrame())
        {
            gate.position += Vector3.up * gateLift;
            Destroy(gameObject);
        }
    }
}