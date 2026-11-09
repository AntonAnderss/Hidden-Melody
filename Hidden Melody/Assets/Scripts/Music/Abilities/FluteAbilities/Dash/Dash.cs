using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Music Objects/Flute Abilities/Dash", order = 0)]
public class Dash : Ability
{
    public override void Perform()
    {
        if (!Unlocked)
        {
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }


        DashController dashController = player.GetComponentInChildren<DashController>();

        if (dashController == null)
        {
            return;
        }

        dashController.Dash();
    }

}
