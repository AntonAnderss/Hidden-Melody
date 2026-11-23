using UnityEngine;

public class SlidingBoat : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;

    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float maxSpeed = 8f;

    [SerializeField] private Transform playerRidePoint;

    private int currentPoint = 0;
    private float currentSpeed = 0f;
    private bool isSliding = false;

    private PlayerMovement playerMovement;
    private Rigidbody playerRb;
    private Rigidbody boatRb;
    private Transform playerTransform;
    private bool rideCompleted = false;
    private Transform originalPlayerParent;
    private void Awake()
    {
        boatRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!isSliding)
            return;

        if (currentPoint >= pathPoints.Length)
        {
            FinishRide();
            return;
        }

        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            maxSpeed,
            acceleration * Time.fixedDeltaTime);

        Transform target = pathPoints[currentPoint];

        Vector3 newPosition = Vector3.MoveTowards(
            boatRb.position,
            target.position,
            currentSpeed * Time.fixedDeltaTime);

        boatRb.MovePosition(newPosition);

        if (Vector3.Distance(newPosition, target.position) < 0.05f)
        {
            currentPoint++;

            currentPoint++;

            if (currentPoint >= pathPoints.Length)
            {
                FinishRide();
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSliding || rideCompleted)
            return;

        PlayerMovement movement =
            other.GetComponentInParent<PlayerMovement>();

        if (movement == null)
            return;

        playerMovement = movement;
        playerTransform = movement.transform;
        playerRb = movement.GetComponent<Rigidbody>();

        playerMovement.ResetMovementInput();
        playerMovement.enabled = false;

        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        playerRb.isKinematic = true;

        playerTransform.SetParent(playerRidePoint);
        playerTransform.localPosition = Vector3.zero;

        isSliding = true;

        Debug.Log("Boat ride started");
    }

    private void FinishRide()
    {
        if (rideCompleted)
            return;

        rideCompleted = true;
        isSliding = false;

        Debug.Log("FINISH RIDE");

        if (playerTransform != null)
        {
            playerTransform.SetParent(null);
        }

        if (playerRb != null)
        {
            playerRb.isKinematic = false;
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        if (playerMovement != null)
        {
            playerMovement.ResetMovementInput();
            playerMovement.enabled = true;
        }

        Debug.Log("PLAYER RELEASED");

        playerTransform = null;
        playerRb = null;
        playerMovement = null;
    }

}