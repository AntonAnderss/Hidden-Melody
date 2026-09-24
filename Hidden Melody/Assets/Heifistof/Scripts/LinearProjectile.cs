using UnityEngine;

/*
 * Author: Clara Lönnkrans
 *A projectile that has a linear travel path
*/
public class LinearProjectile : Projectile
{
    private float timer;
    [SerializeField] private float timeExisting = 5f;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
        transform.position.x + (speed * Time.deltaTime),
        transform.position.y,
        transform.position.z);

        timer += Time.deltaTime;

        if (timer > timeExisting)
        {
            Destroy(gameObject);
        }
    }
}
