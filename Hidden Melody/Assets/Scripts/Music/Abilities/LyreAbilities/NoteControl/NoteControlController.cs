using System.Collections;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class NoteControlController : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float controlDuration = 10f;
    [SerializeField] private PlatformerCamera platformerCamera;
    private StateMachine stateMachine;

    private PlayerMovement playerMovement;

    private GameObject currentNote;
    private Rigidbody noteRb;

    private bool controllingNote = false;
    private Vector2 moveInput;
    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        float horizontal = 0f;
        float vertical = 0f;
        if (!controllingNote)
            return;

        if (Input.GetKey(KeyCode.A))
            horizontal = -1f;

        if (Input.GetKey(KeyCode.D))
            horizontal = 1f;

        if (Input.GetKey(KeyCode.W))
            vertical = 1f;

        if (Input.GetKey(KeyCode.S))
            vertical = -1f;

        moveInput = new Vector2(horizontal, vertical);
    }


        
    private void FixedUpdate()
    {
        if(!controllingNote || noteRb== null)
        {
            return;
        }
        noteRb.linearVelocity = new Vector3(moveInput.x * moveSpeed, moveInput.y * moveSpeed, 0f);
    }

    public void ActivateControl()
    {
        if (controllingNote)
            return;

        StartCoroutine(ControlNote());
    }

    private IEnumerator ControlNote()
    {
        controllingNote = true;
        float direction = playerMovement.LastMoveDirection;
        Vector3 spawnOffset = new Vector3(1.5f * direction, 0f, 0f);
        // Create the note at the player's position
        currentNote = Instantiate(notePrefab,transform.position + spawnOffset,Quaternion.identity);

        noteRb = currentNote.GetComponent<Rigidbody>();

        // Stop controlling the player
        playerMovement.enabled = false;
        stateMachine.ChangeState(stateMachine.idleState);
        stateMachine.LockedInIdle = true;


        platformerCamera.SetTarget(currentNote.transform);

        yield return new WaitForSeconds(controlDuration);

        platformerCamera.SetTarget(transform);

        // Remove the note
        Destroy(currentNote);

        currentNote = null;
        noteRb = null;

        // Give control back to the player
        playerMovement.enabled = true;
        stateMachine.LockedInIdle = false;

        controllingNote = false;
    }
}