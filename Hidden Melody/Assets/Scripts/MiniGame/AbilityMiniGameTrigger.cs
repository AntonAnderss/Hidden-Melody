using UnityEngine;

public class AbilityMiniGameTrigger : MonoBehaviour
{
    [SerializeField] private AbilityUnlockEvent abilityUnlockEvent;
    [SerializeField] private Ability abilityToUnlock;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && !hasTriggered)
    //    {
    //        StartMinigame();
    //    }
    //}

    public void StartMinigame()
    {
        Debug.Log("AbilityMiniGameTrigger StartMinigame");

        if (!abilityToUnlock.Unlocked)
        {
            abilityUnlockEvent.StartAbilityMiniGame(abilityToUnlock);
        }
    }
}