using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class EuridicesGhost : MonoBehaviour
{
    private float spawningTimer = 0;
    private float spawningTime = 2;
    private float speed = 3f;
    [SerializeField] GameObject[] spawnableObjects;
    private float direction;
    private bool isAttacking = false;
    private GameObject player;
    private HealthSystem healtSystem;
    private int damage = 7;
    private List<GameObject> spawnedObjects;
    public bool IsAttacking
    {
        set { isAttacking = value; }
    }
    public int Direction
    {
        set { direction = value; }
    }
    void Start()
    {
        spawnedObjects = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        healtSystem = player.GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (isAttacking)
        {
            if(direction > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            }
            transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
            CheckIsLooking();

            spawningTimer += Time.deltaTime;
            if (spawningTimer > spawningTime)
            {
                spawningTimer = 0;
                SpawnObject();
            }
        }
    }
    void CheckIsLooking()
    {
        Debug.Log("Direction of euridice " + direction);
        Debug.Log("Direction of player " + player.transform.right.x);
        if (player.transform.right.x != direction)
        {
            healtSystem.LooseHealth(1);
        }
    }
    void SpawnObject()
    {
        int i = Random.Range(0, spawnableObjects.Length);
        GameObject spawnObject = spawnableObjects[i];
        spawnedObjects.Add(Instantiate(spawnObject, new Vector3(player.transform.position.x + (direction * 2f), transform.position.y - 1, transform.position.z), Quaternion.identity));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isAttacking)
        {
            healtSystem.LooseHealth(damage);
        }
    }
    public IEnumerator RiseFromBelow()
    {
        while (transform.position.y < 1.3f)
        {
            transform.position += new Vector3(0, 4f * Time.deltaTime, 0);
            yield return null;
        }
        //Scary scream
    }
    public IEnumerator Sink()
    {
        //Scary scream
        foreach(GameObject spawn in spawnedObjects)
        {
            Destroy(spawn);
        }
        while (transform.position.y > -4)
        {
            transform.position += new Vector3(0, -4f * Time.deltaTime, 0);
            yield return null;
        }
        Destroy(gameObject);
    }
}
