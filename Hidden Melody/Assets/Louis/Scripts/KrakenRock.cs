using UnityEngine;

public class KrakenRock : Projectile
{
    public Vector3 velocity;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        velocity += Physics.gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}
