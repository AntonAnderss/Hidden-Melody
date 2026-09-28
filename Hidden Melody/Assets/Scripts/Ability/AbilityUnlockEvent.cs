using UnityEngine;

public class AbilityUnlockEvent : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;
    [SerializeField] private AbilityType abilityToUnlock;

    private bool waitingForMiniGame = false;
    private AbilityType currentAbility;

    public void StartAbilityMiniGame(AbilityType ability)
    {
        currentAbility = ability;
        Debug.Log("Starting minigame for: " + abilityToUnlock);

        HitCombination combination = CreateCombinations.GetCombination(abilityToUnlock);
        if(combination == null)
        {
            Debug.Log("No combination found");
            return;
        }
        miniGame.StartMiniGame(combination);

        waitingForMiniGame = true;
    }

    private void Update()
    {
        if(waitingForMiniGame && miniGame.minigameCompleted)
        {
            AbilityUnlockState.Unlock(currentAbility);
            Debug.Log(currentAbility + " Unlocked");

            waitingForMiniGame = false;
        }
    }
}
