using UnityEngine;

public class CaveLighting : MonoBehaviour
{
    [SerializeField] private float caveIntensity = 0.3f;

    private float normalIntensity;

    private void Start()
    {
        normalIntensity = RenderSettings.ambientIntensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.ambientIntensity = caveIntensity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.ambientIntensity = normalIntensity;
        }
    }
}