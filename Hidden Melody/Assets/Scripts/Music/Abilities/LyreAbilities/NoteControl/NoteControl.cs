using UnityEngine;

[CreateAssetMenu(
    fileName = "NoteControl",
    menuName = "Music Objects/Lyre Abilities/Note Control"
)]
public class NoteControl : Ability
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

        NoteControlController controller = player.GetComponent<NoteControlController>();

        if (controller == null)
        {
            Debug.Log("NoteControlController not found");
            return;
        }

        controller.ActivateControl();
    }
}