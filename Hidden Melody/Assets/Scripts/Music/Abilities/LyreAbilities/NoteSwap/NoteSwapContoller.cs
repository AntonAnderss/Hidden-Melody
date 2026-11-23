using UnityEngine;

public class NoteSwapController : MonoBehaviour
{
    [SerializeField] private NoteControlController noteControl;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void Swap()
    {
        if (!noteControl.IsControllingNote)
            return;

        GameObject note = noteControl.CurrentNote;

        if (note == null)
            return;

        Transform player = playerMovement.transform;

        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        Rigidbody noteRb = note.GetComponent<Rigidbody>();

        Vector3 notePosition = note.transform.position;

        // Stop player movement before teleport
        playerMovement.enabled = false;

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            // Move player to note position
            playerRb.position = notePosition;
        }
        else
        {
            player.position = notePosition;
        }

        // Stop note movement
        if (noteRb != null)
        {
            noteRb.linearVelocity = Vector3.zero;
        }

        // End NoteControl AND destroy note
        noteControl.StopNoteControl(true);

        Debug.Log("NoteSwap complete");
    }

}