using UnityEngine;

public class SequenceButton : MonoBehaviour
{
    public GateSequence gate;
    [Range(1, 3)] public int buttonNumber = 1;

    private bool pressedThisLanding;

    void OnCollisionEnter(Collision collision)
    {
        CheckLanding(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        CheckLanding(collision);
    }

    void CheckLanding(Collision collision)
    {
        if (pressedThisLanding || gate == null ||
            gate.player == null || collision.rigidbody != gate.player)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            // On the button, a downward normal means the player is on top.
            if (contact.normal.y < -0.5f)
            {
                pressedThisLanding = true;
                gate.PressButton(buttonNumber);
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (gate != null && collision.rigidbody == gate.player)
            pressedThisLanding = false;
    }
}