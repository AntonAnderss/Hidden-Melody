using UnityEngine;

public class DashAbility : Ability
{
    
    public override AbilityType Type => AbilityType.Dash;
    protected override void Activate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Dash");
        if(player == null)
        {
            Debug.Log("No player");
            return;
        }

        
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.Log("PlayerMovemnet not found");
            return;
        }

            playerMovement.Dash();

        
    }
}
