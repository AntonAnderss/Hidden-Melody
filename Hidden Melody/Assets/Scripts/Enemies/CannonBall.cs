using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private int damage = 5;

    public Kraken target;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            target.Hit(damage);
            Destroy(gameObject);
        }
    }
}
