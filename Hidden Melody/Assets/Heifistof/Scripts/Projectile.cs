using UnityEngine;

/*
 * Author: Clara Lönnkrans
 * Add Player tag to player
*/
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private int damageMultiplier = 1;
    private int damage = 2;
    private float totalDamage;

    private float timer;
    [SerializeField] private float timeExisting = 5f;


    void Start()
    {
        totalDamage = damage * damageMultiplier;
        timer = 0;
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x + (speed * Time.deltaTime), 
            transform.position.y, 
            transform.position.z);

        timer += Time.deltaTime;

        if (timer > timeExisting)
        {
            Destroy(gameObject);
        }
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
