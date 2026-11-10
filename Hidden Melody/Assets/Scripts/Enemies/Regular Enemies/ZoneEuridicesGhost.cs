using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class ZoneEuridicesGhost : MonoBehaviour
{
    [SerializeField] GameObject euridicePrefab;
    private GameObject euridiceSpawned;
    EuridicesGhost euridiceScript;
    private int direction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hath crossed the threshold into the aura");
            direction = (int)GameObject.FindGameObjectWithTag("Player").transform.right.x;
            if (euridiceSpawned == null)
            {
                Vector3 spawnPos = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x * direction, GameObject.FindGameObjectWithTag("Player").transform.position.y - 2, 0);
                euridiceSpawned = Instantiate(euridicePrefab, spawnPos, Quaternion.identity);
                euridiceScript = euridiceSpawned.GetComponent<EuridicesGhost>();
                euridiceScript.Direction = direction;
                //Debug.Log("Direction of player " + direction);
            }
            euridiceScript.StopCoroutine(euridiceScript.Sink());
            euridiceScript.StartCoroutine(euridiceScript.RiseFromBelow());
        }
        euridiceScript.IsAttacking = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hath crossed the threshold out of the aura");
            if (euridiceSpawned != null)
            {
                euridiceScript.IsAttacking = false;
                euridiceScript.StopCoroutine(euridiceScript.RiseFromBelow());
                euridiceScript.StartCoroutine(euridiceScript.Sink());
            }
        }
    }

}
