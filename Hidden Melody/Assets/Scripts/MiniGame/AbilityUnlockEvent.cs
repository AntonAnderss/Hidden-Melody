using UnityEngine;

public class AbilityUnlockEvent : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;
    private Ability currentAbility;

    private bool waitingForMiniGame = false;

    public void StartAbilityMiniGame(Ability ability)
    {
        currentAbility = ability;

        HitCombination combination = CreateCombinations.GetCombination(ability.Name);
        if(combination == null)
        {
            return;
        }

        miniGame.StartMiniGame(combination);

        waitingForMiniGame = true;
    }

    private void Update()
    {
        if(waitingForMiniGame && miniGame.minigameCompleted)
        {
            currentAbility.Unlock();
            waitingForMiniGame = false;
        }
    }
}
