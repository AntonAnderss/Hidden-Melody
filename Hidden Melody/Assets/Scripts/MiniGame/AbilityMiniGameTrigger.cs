using UnityEngine;

public class AbilityMiniGameTrigger : MonoBehaviour
{
    [SerializeField] private AbilityUnlockEvent abilityUnlockEvent;
    [SerializeField] private Ability abilityToUnlock;

    public void StartMinigame()
    {
        Debug.Log("AbilityMiniGameTrigger StartMinigame");

        if (!abilityToUnlock.Unlocked)
        {
            abilityUnlockEvent.StartAbilityMiniGame(abilityToUnlock);
        }
    }
}