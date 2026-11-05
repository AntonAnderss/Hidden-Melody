using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent onInteract;
    [SerializeField] InputActionReference interact;

    bool PlayerInTrigger = false;

    private void OnEnable() { interact.action.started += Interact; }
    private void OnDisabled() { interact.action.started -= Interact; }

    private void OnTriggerEnter(Collider other)
    {
        // Enables Interaction
        if (other.tag == "Player")
            PlayerInTrigger = true;
            
    }

    private void OnTriggerExit(Collider other)
    {
        // Disables Interaction
        if (other.tag == "Player")
            PlayerInTrigger = false;
    }

    void Interact(InputAction.CallbackContext obj)
    {
        if (!PlayerInTrigger)
            return;

        onInteract.Invoke();
    }
}
