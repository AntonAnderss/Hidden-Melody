using UnityEngine;

/*
 * Author: Clara Lönnkrans
 *Baseclass for projectiles
*/
public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 0.5f;
    [SerializeField] protected int damageMultiplier = 1;
    [SerializeField] protected int damage = 2;
    [SerializeField] protected float totalDamage = 2;

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            totalDamage = damage * damageMultiplier;

            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            playerHealth.LooseHealth(totalDamage);
            //Debug.Log(totalDamage);
            Destroy(gameObject);
        }
    }

}
