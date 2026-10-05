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
    protected AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        HitSound();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HitSound();
            totalDamage = damage * damageMultiplier;

            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            playerHealth.LooseHealth(totalDamage);
            
            Destroy(gameObject);
        }
    }
    private void HitSound()
    {
        
        AudioClip hitSound = SoundBank.Instance.GetEnemySound("BadNoteHit");
        if(hitSound == null)
        {
            Debug.Log("No Sound");
            return;
        }
        //audioSource.clip = hitSound;
        //audioSource.loop = false;
        audioSource.PlayOneShot(hitSound);
    }

}
