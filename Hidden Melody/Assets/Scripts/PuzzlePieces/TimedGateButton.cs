using System.Collections;
using UnityEngine;

public class TimedGateButton : MonoBehaviour
{
    public Transform player;
    public Transform gate;
    public float openHeight = 4f;
    public float interactDistance = 3f;

    private bool isOpen;

    void Update()
    {
        if (player == null || gate == null) return;

        if (!isOpen &&
            Vector3.Distance(player.position, transform.position) <= interactDistance &&
            Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OpenGate());
        }
    }

    IEnumerator OpenGate()
    {
        isOpen = true;

        Vector3 closedPosition = gate.position;
        gate.position = closedPosition + Vector3.up * openHeight;

        yield return new WaitForSeconds(5f);

        gate.position = closedPosition;
        isOpen = false;

    }


}
