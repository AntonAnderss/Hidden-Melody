using UnityEngine;

/*
 * Author: Clara Lönnkrans
 *Baseclass for projectiles
*/
public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 0.5f;
    [SerializeField] protected int damageMultiplier = 1;
    private int damage = 2;
    private float totalDamage;




    void Start()
    {
        totalDamage = damage * damageMultiplier;
    }

    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            playerHealth.LooseHealth(totalDamage);
            Destroy(gameObject);
        }
    }

}
