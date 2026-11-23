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
    private Coroutine controlCoroutine;

    private bool controllingNote = false;

    public bool IsControllingNote => controllingNote;
    public GameObject CurrentNote => currentNote;
    private Vector2 moveInput;
    private void Start()
    {
        stateMachine = GetComponentInParent<StateMachine>();
        playerMovement = GetComponentInParent<PlayerMovement>();
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

        controlCoroutine = StartCoroutine(ControlNote());
    }

    private IEnumerator ControlNote()
    {
        controllingNote = true;

        float direction = playerMovement.LastMoveDirection;

        Vector3 spawnOffset = new Vector3(1.5f * direction, 0f, 0f);

        currentNote = Instantiate(notePrefab,playerMovement.transform.position + spawnOffset, Quaternion.identity);

        noteRb = currentNote.GetComponent<Rigidbody>();

        Collider[] playerColliders = playerMovement.GetComponentsInChildren<Collider>();

        Collider[] noteColliders = currentNote.GetComponentsInChildren<Collider>();

        foreach (Collider playerCollider in playerColliders)
        {
            foreach (Collider noteCollider in noteColliders)
            {
                Physics.IgnoreCollision(playerCollider,noteCollider,true);
            }
        }
        playerMovement.enabled = false;

        stateMachine.ChangeState(stateMachine.idleState);
        stateMachine.LockedInIdle = true;

        platformerCamera.SetTarget(currentNote.transform);

        yield return new WaitForSeconds(controlDuration);

        controlCoroutine = null;

        StopNoteControl(true);
    }
    public void StopNoteControl(bool destroyNote)
    {
        if (!controllingNote)
            return;

        controllingNote = false;

        if (controlCoroutine != null)
        {
            StopCoroutine(controlCoroutine);
            controlCoroutine = null;
        }

        platformerCamera.SetTarget(playerMovement.transform);

        if (noteRb != null)
        {
            noteRb.useGravity = true;
            noteRb.linearVelocity = Vector3.zero;
        }

        
        if (destroyNote && currentNote != null)
        {
            Destroy(currentNote);
        }

        playerMovement.ResetMovementInput();
        playerMovement.enabled = true;


        if (stateMachine != null)
        {
            stateMachine.LockedInIdle = false;
            stateMachine.enabled = true;
        }

        currentNote = null;
        noteRb = null;

        Debug.Log("NoteControl stopped");
    }
}