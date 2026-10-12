using UnityEngine;

public class AbilityUnlockEvent : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;
    private Ability currentAbility;

    private bool waitingForMiniGame = false;
    //private string currentAbility;

    public void StartAbilityMiniGame(Ability ability)
    {
        currentAbility = ability;

        HitCombination combination = CreateCombinations.GetCombination(ability.Name);
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


            //AbilityUnlockState.Unlock(currentAbility);
            currentAbility.Unlock();
            Debug.Log(currentAbility + " Unlocked");

            waitingForMiniGame = false;
        }
    }
}
