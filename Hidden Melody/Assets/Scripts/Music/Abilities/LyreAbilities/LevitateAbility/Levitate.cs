using UnityEngine;

[CreateAssetMenu(
    fileName = "Levitate",
    menuName = "Music Objects/Lyre Abilities/Levitate"
)]
public class Levitate : Ability
{
    public override void Perform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        LevitateAbilityController controller = player.GetComponent<LevitateAbilityController>();

        if (controller == null)
            return;

        controller.ActivateLevitate();
    }
}