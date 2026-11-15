using UnityEngine;

public class NoteGateButton : MonoBehaviour
{
    public Transform gate;
    public float openHeight = 4f;

    private bool opened;

    void OnTriggerEnter(Collider other)
    {
        if (opened || other.gameObject.name != "MovingNoteSkillObject(Clone)")
            return;

        gate.position += Vector3.up * openHeight;
        opened = true;
    }
}