using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/*
 * Author: Clara Lönnkrans
 * Manager for enemies
*/
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private bool isSirenSpawned = false;
    [SerializeField] private GameObject sirenPrefab;
    private SirenBossScript spawnedSiren;
    private GameObject player;
    public List<Enemy> enemies;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = new List<Enemy>();
        instance = this;
    }
    public void SpawnSiren()
    {
        if (!isSirenSpawned)
        {
            isSirenSpawned = true;
            GameObject siren = Instantiate(sirenPrefab);
            spawnedSiren = siren.GetComponent<SirenBossScript>();
        }
    }
    public void SirenDead()
    {
        if(spawnedSiren != null)
        {
            spawnedSiren.KillSiren();
        }
        isSirenSpawned = false;
    }
}
