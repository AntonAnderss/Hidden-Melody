using System.Collections;
using UnityEngine;

public class SirenScript : MonoBehaviour
{

    [SerializeField] private float sirenTimer;
    private bool isRisingDone;
    private float timePassed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localPosition = new Vector3(0, -4, 4);
        isRisingDone = false;
        StartCoroutine(MoveUp());
    }

    // Update is called once per frame
    void Update()
    {
        if(isRisingDone)
        {
            timePassed += Time.deltaTime;

            if (timePassed > sirenTimer)
            {
                KillSiren();
            }
        }

    }
    void KillSiren()
    {
        EnemyManager.instance.SirenDead();
        Destroy(gameObject);
    }
    private IEnumerator MoveUp()
    {
        while (transform.localPosition.y < 3)
        {
            transform.localPosition += new Vector3(0, 0.5f * Time.deltaTime, 0);
            yield return null;
        }
        isRisingDone = true;
    }
}
