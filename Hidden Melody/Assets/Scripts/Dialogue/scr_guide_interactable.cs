using UnityEngine;
using UnityEngine.InputSystem;

public class GuideInteractable : MonoBehaviour
{
    [SerializeField] private GuideData guideData;
    [SerializeField] private GameObject interactPrompt;

    private InputAction interactAction;
    public bool playerInRange;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Player/Interact");

    }
    void Update()
    {
        if (!playerInRange || interactAction == null) return;

        if (interactAction.WasPerformedThisFrame() && !DialogueManager.Instance.IsOpen)
            DialogueManager.Instance.Open(guideData);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        if (interactPrompt) interactPrompt.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        if (interactPrompt) interactPrompt.SetActive(false);
        DialogueManager.Instance.Close();
    }
}