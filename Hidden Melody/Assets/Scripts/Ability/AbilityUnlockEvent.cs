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

        HitCombination combination = CreateCombinations.GetCombination(ability);
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
