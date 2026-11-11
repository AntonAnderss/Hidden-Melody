using UnityEngine;

public class RoofButton : MonoBehaviour
{
    public Transform roof;
    public float liftHeight = 4f;
    public float speed = 8f;
    public float openTime = 3f;

    private Vector3 closedPosition;
    private float closeTime;

    void Start()
    {
        closedPosition = roof.position;
    }

    public void Press()
    {
        closeTime = Time.time + openTime;
    }

    void Update()
    {
        Vector3 target = closedPosition;

        if (Time.time < closeTime)
            target += Vector3.up * liftHeight;

        roof.position = Vector3.MoveTowards(
            roof.position, target, speed * Time.deltaTime);
    }
}