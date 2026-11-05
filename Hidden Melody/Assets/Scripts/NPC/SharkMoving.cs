using UnityEngine;

public class SharkPlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform shark;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;

    private Transform target;

    private void Start()
    {
        target = pointB;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            if (target == pointB)
            {
                target = pointA;
                FaceRight();
            }
            else
            {
                target = pointB;
                FaceLeft();
            }
        }
    }

    private void FaceRight()
    {
        shark.localRotation = Quaternion.Euler(90, -90, 0);
    }

    private void FaceLeft()
    {
        shark.localRotation = Quaternion.Euler(-90, 90, 0);
    }
}