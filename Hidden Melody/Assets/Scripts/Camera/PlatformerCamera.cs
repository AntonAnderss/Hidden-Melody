using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlatformerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Camera cam;
    private Rigidbody body;
    private Collider bodyCollider;
    private Vector3 lastTargetPosition;
    private Vector2 velocity;

    private float focusX;
    private float groundY;
    private float lookAhead;
    private float lookAheadGoal;
    private float lookAheadSpeed;
    private float fallOffset;
    private float fallOffsetSpeed;
    private float speedZoom;

    private Vector2 position;
    private Vector2 positionSpeed;
    private float viewHeight;
    private float viewHeightSpeed;

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (target == null)
        {
            enabled = false;
            return;
        }

        body = target.GetComponent<Rigidbody>();
        bodyCollider = target.GetComponent<Collider>();
        Snap();
    }

    private void LateUpdate()
    {
        if (Time.deltaTime <= 0f)
            return;

        Vector3 targetPosition = target.position;
        if (Vector2.Distance(targetPosition, lastTargetPosition) > 15f)
        {
            Snap();
            return;
        }

        velocity = body != null
            ? (Vector2)body.linearVelocity
            : (Vector2)(targetPosition - lastTargetPosition) / Time.deltaTime;
        lastTargetPosition = targetPosition;

        Follow(targetPosition);

        Vector2 desired = DesiredPosition(targetPosition, out float desiredHeight);
        bool falling = velocity.y < -8f;

        viewHeight = Mathf.SmoothDamp(viewHeight, desiredHeight, ref viewHeightSpeed, 0.6f);
        position.x = Mathf.SmoothDamp(position.x, desired.x, ref positionSpeed.x, 0.25f);
        position.y = Mathf.SmoothDamp(position.y, desired.y, ref positionSpeed.y, falling ? 0.12f : 0.35f);

        Vector2 reach = HalfExtents(viewHeight) - Vector2.one * 1.5f;
        position = Clamp(position, (Vector2)targetPosition - reach, (Vector2)targetPosition + reach);

        ApplyTransform(targetPosition.z);
    }

    private void Follow(Vector3 targetPosition)
    {
        focusX = Mathf.Clamp(focusX, targetPosition.x - 0.75f, targetPosition.x + 0.75f);

        if (Mathf.Abs(velocity.x) > 1f)
            lookAheadGoal = Mathf.Sign(velocity.x) * 3f;
        lookAhead = Mathf.SmoothDamp(lookAhead, lookAheadGoal, ref lookAheadSpeed, 0.8f);

        if (IsGrounded() || targetPosition.y < groundY)
            groundY = targetPosition.y;
        else
            groundY = Mathf.Max(groundY, targetPosition.y - 3f);

        float fall = Mathf.InverseLerp(8f, 16f, -velocity.y);
        fallOffset = Mathf.SmoothDamp(fallOffset, -3f * fall, ref fallOffsetSpeed, 0.35f);

        float speed = Mathf.InverseLerp(6f, 14f, velocity.magnitude);
        speedZoom = Mathf.Max(speed, speedZoom - Time.deltaTime / 1.2f);
    }

    private bool IsGrounded()
    {
        if (velocity.y > 0.1f)
            return false;
        if (bodyCollider == null)
            return velocity.y > -0.1f;

        Bounds bounds = bodyCollider.bounds;
        float radius = bounds.extents.x * 0.9f;
        float distance = bounds.extents.y - radius + 0.25f;
        return Physics.SphereCast(bounds.center, radius, Vector3.down, out _, distance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
    }

    private Vector2 DesiredPosition(Vector2 targetPoint, out float height)
    {
        CameraZone zone = CameraZone.Find(targetPoint);
        height = (zone != null ? zone.ViewHeight : 10f) + 3f * speedZoom;

        Vector2 min = targetPoint - Vector2.one * 2f;
        Vector2 max = targetPoint + Vector2.one * 2f;

        foreach (CameraFocusTarget focus in CameraFocusTarget.All)
        {
            float influence = focus.InfluenceAt(targetPoint);
            if (influence <= 0f)
                continue;

            Vector2 point = Vector2.Lerp(targetPoint, focus.transform.position, influence);
            min = Vector2.Min(min, point - Vector2.one * 4f);
            max = Vector2.Max(max, point + Vector2.one * 4f);
        }

        Vector2 size = max - min;
        height = Mathf.Max(height, size.y, size.x / cam.aspect);

        Vector2 half = HalfExtents(height);
        Vector2 center = new Vector2(focusX + lookAhead, groundY + 1.5f + fallOffset);
        return Clamp(center, max - half, min + half);
    }

    private void Snap()
    {
        Vector3 targetPosition = target.position;
        lastTargetPosition = targetPosition;
        velocity = Vector2.zero;

        focusX = targetPosition.x;
        groundY = targetPosition.y;
        lookAhead = lookAheadGoal = lookAheadSpeed = 0f;
        fallOffset = fallOffsetSpeed = 0f;
        speedZoom = 0f;

        position = DesiredPosition(targetPosition, out viewHeight);
        positionSpeed = Vector2.zero;
        viewHeightSpeed = 0f;

        ApplyTransform(targetPosition.z);
    }

    private void ApplyTransform(float planeZ)
    {
        float distance = viewHeight * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        transform.SetPositionAndRotation(new Vector3(position.x, position.y, planeZ - distance), Quaternion.identity);
    }

    private Vector2 HalfExtents(float height)
    {
        return new Vector2(height * 0.5f * cam.aspect, height * 0.5f);
    }

    private static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
    {
        return Vector2.Max(min, Vector2.Min(value, max));
    }
}
