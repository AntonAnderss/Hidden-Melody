using UnityEngine;

public class AbilityMiniGameTrigger : MonoBehaviour
{
    [SerializeField] private AbilityUnlockEvent abilityUnlockEvent;
    [SerializeField] private AbilityType abilityToUnlock;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("Trigger activated");
            hasTriggered = true;

            abilityUnlockEvent.StartAbilityMiniGame(abilityToUnlock);
        }
    }
}
