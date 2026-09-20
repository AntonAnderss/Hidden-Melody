using UnityEngine;

public class SirenScript : MonoBehaviour
{

    [SerializeField] private float sirenTimer;
    private float timePassed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if(timePassed > sirenTimer)
        {
            KillSiren();
        }
    }
    void KillSiren()
    {
        Debug.Log("Siren dead");
        Destroy(gameObject);
    }
    
}
