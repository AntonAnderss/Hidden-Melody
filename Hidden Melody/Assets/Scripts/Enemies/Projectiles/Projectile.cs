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
        BadNoteSound();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<ShieldCollision>() != null)
        {
            Debug.Log("Shield");
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            totalDamage = damage * damageMultiplier;

            HealthSystem playerHealth = other.gameObject.GetComponent<HealthSystem>();
            playerHealth.LooseHealth(totalDamage);
            
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void BadNoteSound()
    {
        AudioClip hitSound = SoundBank.Instance.GetEnemySound("BadNoteHit");
        if(hitSound == null)
        {
            Debug.Log("No Sound");
            return;
        }
        audioSource.PlayOneShot(hitSound);
    }

}
