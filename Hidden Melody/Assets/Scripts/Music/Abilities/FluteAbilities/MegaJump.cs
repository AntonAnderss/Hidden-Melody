using UnityEngine;

[CreateAssetMenu(fileName = "MegaJump", menuName = "Music Objects/Flute Abilities/MegaJump", order = 1)]
public class MegaJump : Ability
{

    public override void Perform()
    {

        Debug.Log("DoubleJump");

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log(" Player not found");
            return;
        }

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.Log("PlayerMovement not found");
            return;
        }

        playerMovement.ActivateDoubleJump();
    }
}