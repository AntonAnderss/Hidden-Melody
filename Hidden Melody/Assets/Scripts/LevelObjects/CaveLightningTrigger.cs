using UnityEngine;

public class CaveLighting : MonoBehaviour
{
    [SerializeField] private float caveIntensity = 0.1f;
    [SerializeField] private float transitionSpeed = 2f;

    private float normalIntensity;
    private float targetIntensity;

    private void Start()
    {
        normalIntensity = RenderSettings.ambientIntensity;
        targetIntensity = normalIntensity;
    }
    private void Update()
    {
        RenderSettings.ambientIntensity = Mathf.Lerp(
            RenderSettings.ambientIntensity,
            targetIntensity,
            transitionSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetIntensity = caveIntensity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetIntensity = normalIntensity;
        }
    }
}