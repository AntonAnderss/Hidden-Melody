using UnityEngine;

[CreateAssetMenu(
    fileName = "Trap",
    menuName = "Music Objects/Lute Abilities/Trap")]
public class Trap : Ability
{
    public override void Perform()
    {
        if (!Unlocked)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        TrapController trapController = player.GetComponentInChildren<TrapController>(true);

        if (trapController != null)
        {
            trapController.UseTrap();
        }
    }
}