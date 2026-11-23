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
            !collision.gameObject.CompareTag("Player"))
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
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
        if (collision.gameObject.CompareTag("Player"))
            pressedThisLanding = false;
    }
}