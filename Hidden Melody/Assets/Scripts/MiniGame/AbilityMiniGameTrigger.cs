using UnityEngine;

public class AbilityMiniGameTrigger : MonoBehaviour
{
    [SerializeField] private AbilityUnlockEvent abilityUnlockEvent;
    [SerializeField] private Ability abilityToUnlock;

    private bool hasTriggered = false;

    public void StartMinigame()
    {
        hasTriggered = true;

        abilityUnlockEvent.StartAbilityMiniGame(abilityToUnlock);
    }
}
