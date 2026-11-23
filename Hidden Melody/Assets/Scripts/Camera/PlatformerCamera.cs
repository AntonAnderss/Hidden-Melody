using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class PlatformerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask occluderLayers = ~0;
    [SerializeField] private float occluderRadius = 0.5f;
    [SerializeField] private bool occludeWithTriggers = false;
    [SerializeField, Range(0f, 1f)] private float occluderAlpha = 0.3f;
    [SerializeField] private float occluderFadeTime = 0.2f;

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
    private float shakeTimer = 0f;
    private float shakeStrength = 0f;

    private Vector2 position;
    private Vector2 positionSpeed;
    private float viewHeight;
    private float viewHeightSpeed;

    private readonly RaycastHit[] occluderHits = new RaycastHit[32];
    private readonly HashSet<Renderer> occluding = new HashSet<Renderer>();
    private readonly List<Renderer> released = new List<Renderer>();
    private readonly Dictionary<Renderer, FadedRenderer> faded = new Dictionary<Renderer, FadedRenderer>();
    private readonly Dictionary<Collider, Renderer[]> colliderRenderers = new Dictionary<Collider, Renderer[]>();

    private static readonly int SurfaceId = Shader.PropertyToID("_Surface");
    private static readonly int BlendId = Shader.PropertyToID("_Blend");
    private static readonly int SrcBlendId = Shader.PropertyToID("_SrcBlend");
    private static readonly int DstBlendId = Shader.PropertyToID("_DstBlend");
    private static readonly int SrcBlendAlphaId = Shader.PropertyToID("_SrcBlendAlpha");
    private static readonly int DstBlendAlphaId = Shader.PropertyToID("_DstBlendAlpha");
    private static readonly int ZWriteId = Shader.PropertyToID("_ZWrite");
    private static readonly int AlphaClipId = Shader.PropertyToID("_AlphaClip");
    private static readonly int AlphaToMaskId = Shader.PropertyToID("_AlphaToMask");
    private static readonly int AlphaTransparencyId = Shader.PropertyToID("_Alpha_Transparency");
    private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
    private static readonly int ColorId = Shader.PropertyToID("_Color");

    private class FadedRenderer
    {
        public Material[] originals;
        public Material[] instances;
        public float[] baseAlphas;
        public float alpha = 1f;
    }

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
        UpdateOccluders();
    }

    private void OnDisable()
    {
        foreach (KeyValuePair<Renderer, FadedRenderer> entry in faded)
            Restore(entry.Key, entry.Value);
        faded.Clear();
    }

    private void UpdateOccluders()
    {
        occluding.Clear();

        Vector3 origin = target.position;
        Vector3 toCamera = transform.position - origin;
        float distance = toCamera.magnitude;

        if (distance > 0.01f)
        {
            QueryTriggerInteraction triggers = occludeWithTriggers ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore;
            int count = Physics.SphereCastNonAlloc(origin, occluderRadius, toCamera / distance, occluderHits, distance, occluderLayers, triggers);

            for (int i = 0; i < count; i++)
            {
                RaycastHit hit = occluderHits[i];
                if (hit.distance <= 0f || hit.collider.transform.IsChildOf(target))
                    continue;

                foreach (Renderer occluder in RenderersOf(hit.collider))
                    occluding.Add(occluder);
            }
        }

        foreach (Renderer occluder in occluding)
        {
            if (!faded.ContainsKey(occluder))
                faded.Add(occluder, MakeTransparent(occluder));
        }

        float step = occluderFadeTime > 0f ? Time.deltaTime / occluderFadeTime : 1f;

        released.Clear();
        foreach (KeyValuePair<Renderer, FadedRenderer> entry in faded)
        {
            Renderer occluder = entry.Key;
            FadedRenderer fade = entry.Value;

            if (occluder == null)
            {
                released.Add(occluder);
                continue;
            }

            float goal = occluding.Contains(occluder) ? occluderAlpha : 1f;
            fade.alpha = Mathf.MoveTowards(fade.alpha, goal, step * (1f - occluderAlpha));
            SetAlpha(fade);

            if (fade.alpha >= 1f)
                released.Add(occluder);
        }

        foreach (Renderer occluder in released)
        {
            Restore(occluder, faded[occluder]);
            faded.Remove(occluder);
        }
    }

    private static FadedRenderer MakeTransparent(Renderer occluder)
    {
        Material[] originals = occluder.sharedMaterials;
        Material[] instances = new Material[originals.Length];
        float[] baseAlphas = new float[originals.Length];

        for (int i = 0; i < originals.Length; i++)
        {
            if (originals[i] == null)
                continue;

            Material material = new Material(originals[i]);
            material.name = originals[i].name + " (Faded)";
            material.SetFloat(SurfaceId, 1f);
            material.SetFloat(BlendId, 0f);
            material.SetFloat(SrcBlendId, (float)BlendMode.SrcAlpha);
            material.SetFloat(DstBlendId, (float)BlendMode.OneMinusSrcAlpha);
            material.SetFloat(SrcBlendAlphaId, (float)BlendMode.One);
            material.SetFloat(DstBlendAlphaId, (float)BlendMode.OneMinusSrcAlpha);
            material.SetFloat(ZWriteId, 0f);
            material.SetFloat(AlphaClipId, 0f);
            material.SetFloat(AlphaToMaskId, 0f);
            material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.SetOverrideTag("RenderType", "Transparent");
            material.renderQueue = (int)RenderQueue.Transparent;

            instances[i] = material;
            baseAlphas[i] = BaseAlpha(material);
        }

        occluder.sharedMaterials = instances;
        return new FadedRenderer { originals = originals, instances = instances, baseAlphas = baseAlphas };
    }

    private static float BaseAlpha(Material material)
    {
        if (material.HasProperty(AlphaTransparencyId))
            return material.GetFloat(AlphaTransparencyId);
        if (material.HasProperty(BaseColorId))
            return material.GetColor(BaseColorId).a;
        if (material.HasProperty(ColorId))
            return material.GetColor(ColorId).a;
        return 1f;
    }

    private static void SetAlpha(FadedRenderer fade)
    {
        for (int i = 0; i < fade.instances.Length; i++)
        {
            Material material = fade.instances[i];
            if (material == null)
                continue;

            float alpha = fade.baseAlphas[i] * fade.alpha;

            if (material.HasProperty(AlphaTransparencyId))
            {
                material.SetFloat(AlphaTransparencyId, alpha);
            }
            else if (material.HasProperty(BaseColorId))
            {
                Color color = material.GetColor(BaseColorId);
                color.a = alpha;
                material.SetColor(BaseColorId, color);
            }
            else if (material.HasProperty(ColorId))
            {
                Color color = material.GetColor(ColorId);
                color.a = alpha;
                material.SetColor(ColorId, color);
            }
        }
    }

    private static void Restore(Renderer occluder, FadedRenderer fade)
    {
        if (occluder != null)
            occluder.sharedMaterials = fade.originals;

        foreach (Material material in fade.instances)
        {
            if (material != null)
                Destroy(material);
        }
    }

    private Renderer[] RenderersOf(Collider collider)
    {
        if (colliderRenderers.TryGetValue(collider, out Renderer[] renderers) && renderers != null)
            return renderers;

        renderers = collider.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0 && collider.transform.parent != null)
            renderers = collider.transform.parent.GetComponentsInChildren<Renderer>();

        colliderRenderers[collider] = renderers;
        return renderers;
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
    public void Shake(float duration = 0.15f, float strength = 3f)
    {
        shakeTimer = duration;
        shakeStrength = strength;
    }

    private void ApplyTransform(float planeZ)
    {
        float distance = 
            viewHeight * 0.5f /
            Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);

        float zRotation = 0f;

        if (shakeTimer > 0f)
        {
            zRotation = Random.Range(-shakeStrength, shakeStrength);

            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                shakeTimer = 0f;
                shakeStrength = 0f;
            }
        }

        transform.SetPositionAndRotation(new Vector3(position.x, position.y, planeZ - distance),
            Quaternion.Euler(0f, 0f, zRotation));
    }

    private Vector2 HalfExtents(float height)
    {
        return new Vector2(height * 0.5f * cam.aspect, height * 0.5f);
    }

    private static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
    {
        return Vector2.Max(min, Vector2.Min(value, max));
    }
    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null)
            return;

        target = newTarget;

        body = newTarget.GetComponent<Rigidbody>();
        bodyCollider = newTarget.GetComponent<Collider>();

        lastTargetPosition = target.position;
    }
}
