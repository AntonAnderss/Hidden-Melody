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
    private SirenScript spawnedSiren;
    [SerializeField] private GameObject player;
    public List<Enemy> enemies;

    void Start()
    {
        enemies = new List<Enemy>();
        instance = this;
    }

    void Update()
    {

    }
    public void SpawnSiren()
    {
        if (!isSirenSpawned)
        {
            isSirenSpawned = true;
            GameObject siren = Instantiate(sirenPrefab, player.transform);
            spawnedSiren = siren.GetComponent<SirenScript>();
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
