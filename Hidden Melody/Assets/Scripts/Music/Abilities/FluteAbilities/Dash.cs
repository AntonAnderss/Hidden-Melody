using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Music Objects/Flute Abilities/ Dash", order = 0)]
public class Dash : Ability
{
    public override void Perform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Dash");
        if (player == null)
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
