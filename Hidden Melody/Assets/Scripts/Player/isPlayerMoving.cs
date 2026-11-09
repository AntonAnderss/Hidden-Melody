using UnityEngine;

public class isPlayerMoving : MonoBehaviour
{

    public AudioSource PlayerWalking;
    Rigidbody playerBody;


    PlayerMovement playermovement;
    public AudioSource PlayerSprinting;
    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        bool isMoving = playermovement.IsWalking
            && playermovement.IsGrounded
            && Mathf.Abs(playerBody.linearVelocity.x) > 0.1f;

        bool isSprinting = playermovement.IsRunning && !playermovement.IsCrouching;

        AudioSource currentSound = isSprinting ? PlayerSprinting : PlayerWalking;
        AudioSource otherSound = isSprinting ? PlayerWalking : PlayerSprinting;

        if (otherSound.isPlaying)
            otherSound.Stop();

        if (isMoving && !currentSound.isPlaying)
            currentSound.Play();
        else if (!isMoving && currentSound.isPlaying)
            currentSound.Stop();


    }
}
