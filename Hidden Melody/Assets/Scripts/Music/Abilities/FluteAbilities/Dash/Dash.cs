using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Music Objects/Flute Abilities/Dash", order = 0)]
public class Dash : Ability
{
    public override void Perform()
    {
        if(!Unlocked)
        {
            Debug.Log("Dash is locked");
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Dash");
        if (player == null)
        {
            Debug.Log("No player");
            return;
        }


        DashController dashController = player.GetComponent<DashController>();

        if (dashController == null)
        {
            Debug.Log("DashController not found");
            return;
        }

        dashController.Dash();
    }

}
