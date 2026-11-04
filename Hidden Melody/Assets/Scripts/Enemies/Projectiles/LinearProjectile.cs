using UnityEngine;

/*
 * Author: Clara Lönnkrans
 *A projectile that has a linear travel path
 *dissapear after a certain travel lenght
*/
public class LinearProjectile : Projectile
{
    private Vector3 startPosition;
    [SerializeField] private float travelDistance = 2.5f;
    public Vector3 direction = Vector3.right;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        transform.position += direction * +(speed * Time.deltaTime);

        if (transform.position.x > startPosition.x + direction.x * travelDistance)
        {
            Destroy(gameObject);
        }
    }
}
