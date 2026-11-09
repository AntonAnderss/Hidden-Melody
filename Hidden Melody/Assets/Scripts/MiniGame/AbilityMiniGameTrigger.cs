using UnityEngine;

public class AbilityMiniGameTrigger : MonoBehaviour
{
    [SerializeField] private AbilityUnlockEvent abilityUnlockEvent;
    [SerializeField] private Ability abilityToUnlock;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            StartMinigame();
        }
    }

    public void StartMinigame()
    {
        Debug.Log("AbilityMiniGameTrigger StartMinigame");

        hasTriggered = true;

        abilityUnlockEvent.StartAbilityMiniGame(abilityToUnlock);
    }
}