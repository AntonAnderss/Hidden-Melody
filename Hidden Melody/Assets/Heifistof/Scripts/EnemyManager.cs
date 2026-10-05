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
    [SerializeField] private GameObject player;
    public List<Enemy> enemies;

    void Start()
    {
        //sirenPrefab.transform.SetParent(player.transform);
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
            Instantiate(sirenPrefab, player.transform);

        }
    }
    public void SirenDead()
    {

        isSirenSpawned = false;
    }
}
