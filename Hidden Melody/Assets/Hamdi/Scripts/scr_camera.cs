using UnityEngine;
// Here im using smooth damp function to make things smoother
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1.5f, -10f);

    [Header("Smoothing")]
    public float smoothTimeX = 0.15f;
    public float smoothTimeY = 0.25f; 

    [Header("Bounds")]
    public bool useBounds = false;
    public float minX, maxX, minY, maxY;

    private Vector3 currentVelocity; 

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            target.position.z + offset.z
        );

        float smoothX = Mathf.SmoothDamp(transform.position.x, desiredPosition.x, ref currentVelocity.x, smoothTimeX);
        float smoothY = Mathf.SmoothDamp(transform.position.y, desiredPosition.y, ref currentVelocity.y, smoothTimeY);

        Vector3 newPosition = new Vector3(smoothX, smoothY, desiredPosition.z);

        if (useBounds)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        }

        transform.position = newPosition;
    }
}