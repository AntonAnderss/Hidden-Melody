using UnityEngine;

[CreateAssetMenu(
    fileName = "Shield",
    menuName = "Music Objects/Flute Abilities/Shield",
    order = 2
)]
public class Shield : Ability
{
    public override void Perform()
    {
        if (!Unlocked)
        {
            return;
        }
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No player");
            return;
        }

        ShieldController shieldController = player.GetComponentInChildren<ShieldController>();

        if (shieldController == null)
        {
            Debug.Log("ShieldController not found");
            return;
        }

        shieldController.ActivateShield();
    }
}
