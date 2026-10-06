using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/*
 * Author: Clara Lönnkrans
 * 
*/
public class SirenScript : MonoBehaviour
{
    [Header("Siren")]
    [SerializeField] private float sirenTimer;
    private bool isRisingDone;
    private float timePassed = 0;

    [Header("Projectiles")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileTime = 0;
    private float projectileTimer = 0.3f;
    private List<GameObject> projectileList;

    private HealthSystem playerHealthsystem;

    void Start()
    {
        transform.localPosition = new Vector3(0, -4, 4);
        isRisingDone = false;
        StartCoroutine(MoveUp());
        playerHealthsystem = transform.parent.GetComponent<HealthSystem>();
        projectileList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRisingDone)
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
                playerHealthsystem.RegainAllHealth();
                StartCoroutine(MoveDown());
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
        playerHealthsystem.AlwaysTickingDown = true;
        projectileList.Clear();
        Destroy(gameObject);
    }
    private IEnumerator MoveUp()
    {
        while (transform.localPosition.y < 2)
        {
            transform.localPosition += new Vector3(0, 0.7f * Time.deltaTime, 0);
            yield return null;
        }
        isRisingDone = true;
        BeginMinigame();

        playerHealthsystem.AlwaysTickingDown = true;
    }
    private IEnumerator MoveDown()
    {
        while (transform.localPosition.y > -4)
        {
            transform.localPosition += new Vector3(0, -0.7f * Time.deltaTime, 0);
            yield return null;
        }
        EnemyManager.instance.SirenDead();
    }
    private void SpawnProjectiles()
    {
        float xPos = Random.Range(-9, 9);
        Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y + 1.5f, 0);
        GameObject spawnedProjectile = Instantiate(projectile, spawnPos, Quaternion.identity);
        projectileList.Add(spawnedProjectile);
    }
    private void BeginMinigame()
    {
        Debug.Log("Minigame starts");
    }
}
