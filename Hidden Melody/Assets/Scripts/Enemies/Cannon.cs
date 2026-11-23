using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Kraken kraken;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PowderBarrel>() == null)
        {
            return;
        }

        Destroy(collision.gameObject);
        Fire();
    }

    private void Fire()
    {
        if (kraken == null)
        {
            return;
        }

        GameObject cannonBall = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.identity);
        cannonBall.GetComponent<CannonBall>().target = kraken;
    }
}
