using UnityEngine;

public class BossProjectile : Projectile
{
    public Vector3 direction;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
