using UnityEngine;

/*
 * Author: Clara Lönnkrans
 *A projectile that has a linear travel path
*/
public class LinearProjectile : Projectile
{
    private float timer;
    [SerializeField] private float timeExisting = 7f;
    public Vector3 direction = Vector3.right;
    void Start()
    {
        timer = 0;
    }
    void Update()
    {
        transform.position += direction * +(speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer > timeExisting)
        {
            Destroy(gameObject);
        }
    }
}
