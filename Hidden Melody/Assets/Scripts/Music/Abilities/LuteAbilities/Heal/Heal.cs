using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Music Objects/Lute Abilities/Heal", order = 0)]
public class Heal : Ability
{
    public override void Perform()
    {
        if (!Unlocked)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        HealController healController =
            player.GetComponentInChildren<HealController>(true);

        if (healController != null)
        {
            Debug.Log("Heal");
            healController.TryHeal();
        }
    }

}
