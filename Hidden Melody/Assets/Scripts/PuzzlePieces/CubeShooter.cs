using UnityEditor.Compilation;
using UnityEngine;

public class CubeShooter : MonoBehaviour
{
    public LinearProjectile projectile;
    
    

    void Start()
    {
        InvokeRepeating(nameof(Shoot), 1f, 2f);
    }

    void Shoot()
    {
        LinearProjectile shot = Instantiate(
            projectile,
            transform.position + Vector3.left,
            Quaternion.identity);

        shot.direction = Vector3.left;
    }
}
