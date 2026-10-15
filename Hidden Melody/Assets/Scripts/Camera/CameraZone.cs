using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    private static readonly List<CameraZone> zones = new List<CameraZone>();

    [SerializeField] private Vector2 size = new Vector2(30f, 16f);
    [SerializeField] private float viewHeight = 18f;

    public float ViewHeight => viewHeight;

    private Rect Area => new Rect((Vector2)transform.position - size * 0.5f, size);

    public static CameraZone Find(Vector2 point)
    {
        foreach (CameraZone zone in zones)
        {
            if (zone.Area.Contains(point))
                return zone;
        }
        return null;
    }

    private void OnEnable()
    {
        zones.Add(this);
    }

    private void OnDisable()
    {
        zones.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
