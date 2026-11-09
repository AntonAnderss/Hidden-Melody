using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileTime = 2;
    private float projectileTimer = 0.5f;
    private List<GameObject> projectileList;

    private HealthSystem healthSystem;

    private bool playerSpotted;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();
        projectileList = new List<GameObject>();
    }
    private void Update()
    {
        if(playerSpotted)
        {
            projectileTime += Time.deltaTime;
            if (projectileTime >= projectileTimer)
            {
                projectileTime = 0;
                SpawnProjectiles();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerSpotted = true;

            healthSystem.TickingDown = true;
            //Debug.Log("player spotted");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerSpotted = false;
            healthSystem.TickingDown = false;
            //Debug.Log("player left");
        }
    }
    private void SpawnProjectiles()
    {
        //Debug.Log("projectile");
        float xPos = Random.Range(-4, 4);
        Vector3 spawnPos = new Vector3(player.transform.position.x + xPos, player.transform.position.y + 4f, 0);
        GameObject spawnedProjectile = Instantiate(projectile, spawnPos, Quaternion.identity);
        LinearProjectile projectileScript = spawnedProjectile.GetComponent<LinearProjectile>();
        projectileScript.direction = -transform.up;
        projectileList.Add(spawnedProjectile);
    }
}
