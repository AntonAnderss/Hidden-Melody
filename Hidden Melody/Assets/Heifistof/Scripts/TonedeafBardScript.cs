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
    private Vector3 notePos;

    void Start()
    {
        notePos = new Vector3(transform.position.x + (transform.right.x * 0.7f), transform.position.y, transform.position.z);
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
        int noteType = Random.Range(0, 2);
        if(noteType == 0)
        {
            currentProjectile = Instantiate(projectilePrefab2, notePos, Quaternion.identity);
        }
        else
        {
            currentProjectile = Instantiate(projectilePrefab1, notePos, Quaternion.identity);
        }

    }
}
