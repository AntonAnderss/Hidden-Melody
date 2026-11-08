using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

            Projectile projectile = collision.collider.GetComponentInParent<Projectile>();
            if(projectile != null )
            {
                projectile.Block();
                Destroy(projectile.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        
    }
}
