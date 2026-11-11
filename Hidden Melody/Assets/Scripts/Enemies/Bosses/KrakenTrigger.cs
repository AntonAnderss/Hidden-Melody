using UnityEngine;

public class KrakenTrigger : MonoBehaviour
{
    [SerializeField] private GameObject kraken;

    private void Start()
    {
        kraken.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            kraken.SetActive(true);
        }
    }
}