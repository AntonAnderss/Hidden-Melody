using UnityEngine;

[CreateAssetMenu(fileName = "Leap", menuName = "Music Objects/Flute Abilities/Leap", order = 1)]
public class Leap : Ability
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

        LeapController leapController = player.GetComponentInChildren<LeapController>();

        if (leapController == null)
        {
            return;
        }

        leapController.ActivateLeap();
    }
}