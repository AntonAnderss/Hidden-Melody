using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
