using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Author: Clara Lönnkrans
 * 
 * Add projectile object to projectile
*/
public class SirenBossScript : MonoBehaviour
{
    [Header("Siren")]
    [SerializeField] private float sirenTimer;
    private bool isAttacking;
    private float timePassed = 0;

    [Header("Projectiles")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileTime = 0;
    private float projectileTimer = 0.3f;
    private List<GameObject> projectileList;

    [SerializeField] GameObject player;
    private HealthSystem healthSystem;


    void Start()
    {
        isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();
        transform.position = new Vector3(player.transform.position.x, -4, 4);
        StartCoroutine(MoveUp());
        projectileList = new List<GameObject>();
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, transform.position.z);
        if (isAttacking)
        {

            projectileTime += Time.deltaTime;
            if (projectileTime >= projectileTimer)
            {
                projectileTime = 0;
                SpawnProjectiles();
            }

            timePassed += Time.deltaTime;

            if (timePassed > sirenTimer)
            {
                healthSystem.TickingDown = false;
                healthSystem.RegainAllHealth();
                StartCoroutine(MoveDown());
                isAttacking = false;
            }
        }
    }
    public void KillSiren()
    {
        foreach(GameObject projectile in projectileList)
        {
            if(projectile != null)
            {
                Destroy(projectile.gameObject);
            }
        }
        projectileList.Clear();
        Destroy(gameObject);
    }
    private IEnumerator MoveUp()
    {
        while (transform.position.y < player.transform.position.y + 2)
        {
            transform.position += new Vector3(0, 0.8f * Time.deltaTime, 0);
            yield return null;
        }
        isAttacking = true;
        healthSystem.TickingDown = true;
    }
    private IEnumerator MoveDown()
    {
        while (transform.position.y > player.transform.position.y - 4)
        {
            transform.position += new Vector3(0, -4f * Time.deltaTime, 0);
            yield return null;
        }
        EnemyManager.instance.SirenDead();
    }
    private void SpawnProjectiles()
    {
        float xPos = Random.Range(-9, 9);
        Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y + 1.5f, 0);
        GameObject spawnedProjectile = Instantiate(projectile, spawnPos, Quaternion.identity);
        LinearProjectile projectileScript = spawnedProjectile.GetComponent<LinearProjectile>();
        projectileScript.direction = -transform.up;
        projectileList.Add(spawnedProjectile);
    }
    private void BeginMinigame()
    {
        Debug.Log("Minigame starts");
    }
}
