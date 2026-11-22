using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimedGateButton : MonoBehaviour
{
    private Transform player;
    public Transform gate;
    public float openHeight = 4f;
    public float interactDistance = 3f;

    private bool isOpen;


    public InputActionReference interact;

    void OnEnable()
    {
        interact.action.Enable();
    }


    void Update()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            player = null;

            foreach (GameObject candidate in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (candidate.TryGetComponent<PlayerMovement>(out _))
                {
                    player = candidate.transform;
                    break;
                }
            }
        }

        if (player == null || gate == null)
            return;

        if (!isOpen &&
            Vector3.Distance(player.position, transform.position) <= interactDistance &&
            interact.action.WasPressedThisFrame())
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
