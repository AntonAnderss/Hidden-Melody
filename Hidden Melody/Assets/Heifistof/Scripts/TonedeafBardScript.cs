using UnityEngine;

/*
 * Author: Clara Lönnkrans
 * 
*/
public class TonedeafBardScript : Enemy
{
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab1;
    [SerializeField] GameObject projectilePrefab2;
    [SerializeField] GameObject currentProjectile;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentProjectile == null)
        {
            Attack();
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Attack()
    {
        currentProjectile = Instantiate(projectilePrefab1, transform.position, Quaternion.identity);
    }
}
