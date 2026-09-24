using UnityEngine;

public class YMovingPlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public float distance = 2f;

    private Vector3 startPosition; void Start()
    {
        startPosition = transform.position;

    }

    void Update()
    {
        transform.position = startPosition + Vector3.up * Mathf.Sin(Time.time * speed) * distance;
    }
}
