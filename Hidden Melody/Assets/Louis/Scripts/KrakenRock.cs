using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenRock : Projectile
{
    public Vector3 velocity;
    [SerializeField] public PlayerMovement playerMovement;

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
