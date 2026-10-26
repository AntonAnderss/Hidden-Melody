using UnityEngine;

[CreateAssetMenu(
    fileName = "Levitate",
    menuName = "Music Objects/Lyre Abilities/Levitate"
)]
public class Levitate : Ability
{
    public override void Perform()
    {
        if (!Unlocked)
        {
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        LevitateAbilityController controller = player.GetComponentInChildren<LevitateAbilityController>();

        if (controller == null)
            return;

        controller.ActivateLevitate();
    }
}