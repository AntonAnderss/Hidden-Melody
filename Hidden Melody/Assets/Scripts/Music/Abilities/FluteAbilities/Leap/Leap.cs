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
            Debug.Log(" Player not found");
            return;
        }

        LeapController leapController = player.GetComponent<LeapController>();

        if (leapController == null)
        {
            Debug.Log("PlayerMovement not found");
            return;
        }

        leapController.ActivateLeap();
    }
}