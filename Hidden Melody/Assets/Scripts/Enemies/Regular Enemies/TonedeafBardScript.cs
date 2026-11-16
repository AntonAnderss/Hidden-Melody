using UnityEngine;
using UnityEngine.Audio;

/*
 * Author: Clara Lönnkrans
 * 
*/
public class TonedeafBardScript : Enemy
{
    [Header("Projectile")]
    [SerializeField] GameObject singleNote;
    [SerializeField] GameObject doubleNote;
    [SerializeField] GameObject currentProjectile;
    private Vector3 notePos;
    protected AudioSource audioSource;
    private float projectileTimer = 1;
    private float timeSinceLastProjectile = 0;

    void Start()
    {
        notePos = new Vector3(transform.position.x + (transform.right.x * 0.7f), transform.position.y, transform.position.z);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastProjectile += Time.deltaTime;
        if (currentProjectile == null && timeSinceLastProjectile > projectileTimer)
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
        int noteType = Random.Range(0, 3);
        if(noteType == 0)
        {
            currentProjectile = Instantiate(doubleNote, notePos, Quaternion.identity);
        }
        else
        {
            currentProjectile = Instantiate(singleNote, notePos, Quaternion.identity); 
        }
        LinearProjectile projectileScript = currentProjectile.GetComponent<LinearProjectile>();
        projectileScript.direction = transform.right;
        PlaySound();

    }
    private void PlaySound()
    {
        if (SoundBank.Instance == null)
            return;

        AudioClip hitSound = SoundBank.Instance.GetEnemySound("BadNoteHit");
        if (hitSound == null)
        {
            Debug.Log("No Sound");
            return;
        }
        audioSource.PlayOneShot(hitSound);
    }
}
