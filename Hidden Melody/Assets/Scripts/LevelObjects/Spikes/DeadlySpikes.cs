using UnityEngine;

public class DeadlySpikes : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        HealthSystem health = other.GetComponentInParent<HealthSystem>();

        if (health != null)
            health.TickingDown = true;
    }

    void OnTriggerExit(Collider other)
    {
        HealthSystem health = other.GetComponentInParent<HealthSystem>();

        if (health != null)
            health.TickingDown = false;
    }
}