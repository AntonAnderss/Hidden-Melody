using System.Collections.Generic;
using UnityEngine;

public class CameraFocusTarget : MonoBehaviour
{
    private static readonly List<CameraFocusTarget> targets = new List<CameraFocusTarget>();

    public static IReadOnlyList<CameraFocusTarget> All => targets;

    public float InfluenceAt(Vector2 point)
    {
        float distance = Vector2.Distance(point, transform.position);
        return 1f - Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(12f, 24f, distance));
    }

    private void OnEnable()
    {
        targets.Add(this);
    }

    private void OnDisable()
    {
        targets.Remove(this);
    }
}
