using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform barrelPoint;
    [SerializeField] private Transform sharkPoint;

    private void Start()
    {
        lineRenderer.positionCount = 2;

        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, barrelPoint.position);
        lineRenderer.SetPosition(1, sharkPoint.position);
    }
}