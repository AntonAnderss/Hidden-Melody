using UnityEngine;

[CreateAssetMenu(
    fileName = "NoteSwap",
    menuName = "Music Objects/Lyre Abilities/NoteSwap")]
public class NoteSwap : Ability
{
    public override void Perform()
    {
        if (!Unlocked)
            return;

        GameObject player =GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        NoteSwapController controller = player.GetComponentInChildren<NoteSwapController>(true);

        if (controller != null)
        {
            controller.Swap();
        }
    }
}