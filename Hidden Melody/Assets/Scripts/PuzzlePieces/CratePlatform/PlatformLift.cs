using UnityEngine;

public class PlatformLift : MonoBehaviour
{
    [SerializeField] private Transform barrelTopPoint;
    [SerializeField] private Transform barrelPoint;

    [SerializeField] private float liftMultiplier = 1f;

    private float startRopeLength;
    private Vector3 startPlatformPosition;

    private void Start()
    {
        startRopeLength = Vector3.Distance(barrelTopPoint.position,barrelPoint.position);

        startPlatformPosition = transform.position;
    }

    private void Update()
    {
        float currentRopeLength = Vector3.Distance(
            barrelTopPoint.position,
            barrelPoint.position
        );

        float ropeDifference = currentRopeLength - startRopeLength;

        Vector3 targetPosition = startPlatformPosition;
        targetPosition.y += ropeDifference * liftMultiplier;

        transform.position = targetPosition;
    }
}