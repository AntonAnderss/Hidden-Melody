using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TentacleReset : MonoBehaviour
{
    [SerializeField] private InputActionReference interactInput;

    [SerializeField] private Rigidbody[] barrels;
    [SerializeField] private Transform platform;

    private Vector3[] barrelStartPositions;
    private Quaternion[] barrelStartRotations;

    private Vector3 platformStartPosition;
    private Quaternion platformStartRotation;
    private bool isResetting;
    private bool playerInRange;

    private void Awake()
    {
        barrelStartPositions = new Vector3[barrels.Length];
        barrelStartRotations = new Quaternion[barrels.Length];

        for (int i = 0; i < barrels.Length; i++)
        {
            barrelStartPositions[i] = barrels[i].position;
            barrelStartRotations[i] = barrels[i].rotation;
        }

        platformStartPosition = platform.position;
        platformStartRotation = platform.rotation;
    }

    private void OnEnable()
    {
        interactInput.action.started += Interact;
    }

    private void OnDisable()
    {
        interactInput.action.started -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (playerInRange)
        {
            ResetPuzzle();
        }
    }

    private IEnumerator ResetPuzzel()
    {
        isResetting = true;

        float duration = 1.5f;
        float timer = 0f;

        Vector3[] barrelCurrentPositions = new Vector3[barrels.Length];
        Quaternion[] barrelCurrentRotations = new Quaternion[barrels.Length];

        for (int i = 0; i < barrels.Length; i++)
        {
            barrelCurrentPositions[i] = barrels[i].position;
            barrelCurrentRotations[i] = barrels[i].rotation;

            barrels[i].linearVelocity = Vector3.zero;
            barrels[i].angularVelocity = Vector3.zero;
        }

        Vector3 platformCurrentPosition = platform.position;
        Quaternion platformCurrentRotation = platform.rotation;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            for (int i = 0; i < barrels.Length; i++)
            {
                barrels[i].position = Vector3.Lerp(barrelCurrentPositions[i],barrelStartPositions[i],t);

                barrels[i].rotation = Quaternion.Lerp(barrelCurrentRotations[i],barrelStartRotations[i],t);
            }

            platform.position = Vector3.Lerp(platformCurrentPosition,platformStartPosition,t);

            platform.rotation = Quaternion.Lerp(platformCurrentRotation,platformStartRotation,t);

            yield return null;
        }

        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].position = barrelStartPositions[i];
            barrels[i].rotation = barrelStartRotations[i];
        }

        platform.position = platformStartPosition;
        platform.rotation = platformStartRotation;

        isResetting = false;
    }
    private void ResetPuzzle()
    {
        if (!isResetting)
        {
            StartCoroutine(ResetPuzzel());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}