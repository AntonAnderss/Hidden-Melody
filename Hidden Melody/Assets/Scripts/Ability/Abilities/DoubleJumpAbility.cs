using UnityEngine;

public class DoubleJumpAbility : OldAbility
{
    public override AbilityType Type => AbilityType.DoubleJump;

    protected override void Activate()
    {

        Debug.Log("DoubleJump");

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null )
        {
            Debug.Log(" Player not found");
            return;
        }

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if(playerMovement == null )
        {
            Debug.Log("PlayerMovement not found");
            return;
        }

        playerMovement.ActivateDoubleJump();
    }
}
