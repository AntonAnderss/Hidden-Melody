using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float moveDistance = 3f;

    private Vector3 startPosition;
    private Transform player;
    private Renderer bodyRenderer;
    private Color normalColor;
    private int maxHp;
    private bool isResting = false;

    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bodyRenderer = GetComponent<Renderer>();
        normalColor = bodyRenderer.material.color;
        maxHp = hp;
        StartCoroutine(Fight());
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);
        if (distanceToPlayer > 3f)
        {
            float targetX = Mathf.Clamp(player.position.x, startPosition.x - moveDistance, startPosition.x + moveDistance);
            Vector3 target = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, 2f * Time.deltaTime);
        }
    }

    private IEnumerator Fight()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(AimedShots());
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(GroundShots());
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(SpreadShot());
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(Rest());
        }
    }

    private IEnumerator AimedShots()
    {
        int shots = 3;
        float timeBetweenShots = 0.6f;
        if (IsAngry())
        {
            shots = 5;
            timeBetweenShots = 0.35f;
        }

        yield return StartCoroutine(Charge());

        for (int i = 0; i < shots; i++)
        {
            Vector3 direction = DirectionToPlayer();
            Shoot(transform.position + direction * 2f, direction);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private IEnumerator GroundShots()
    {
        int shots = 2;
        if (IsAngry())
        {
            shots = 4;
        }

        yield return StartCoroutine(Charge());

        for (int i = 0; i < shots; i++)
        {
            Vector3 direction = Vector3.left;
            if (player.position.x > transform.position.x)
            {
                direction = Vector3.right;
            }

            Vector3 position = transform.position + direction * 2f + Vector3.down * 1.5f;
            Shoot(position, direction);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SpreadShot()
    {
        int shots = 3;
        float angle = -15f;
        if (IsAngry())
        {
            shots = 5;
            angle = -30f;
        }

        yield return StartCoroutine(Charge());

        Vector3 direction = DirectionToPlayer();
        for (int i = 0; i < shots; i++)
        {
            Vector3 shotDirection = Quaternion.Euler(0, 0, angle) * direction;
            Shoot(transform.position + shotDirection * 2f, shotDirection);
            angle += 15f;
        }
    }

    private IEnumerator Charge()
    {
        bodyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        bodyRenderer.material.color = normalColor;
    }

    private IEnumerator Rest()
    {
        float restTime = 3f;
        if (IsAngry())
        {
            restTime = 2f;
        }

        isResting = true;
        bodyRenderer.material.color = Color.cyan;

        float timer = 0;
        while (isResting && timer < restTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        isResting = false;
        bodyRenderer.material.color = normalColor;
    }

    private void Shoot(Vector3 position, Vector3 direction)
    {
        GameObject spawnedProjectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        BossProjectile projectileScript = spawnedProjectile.GetComponent<BossProjectile>();
        projectileScript.direction = direction;
    }

    private Vector3 DirectionToPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.z = 0;
        return direction.normalized;
    }

    private bool IsAngry()
    {
        return hp <= maxHp / 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (isResting)
        {
            TakeDamage(1);
            isResting = false;
        }
        else
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            playerHealth.LooseHealth(damage);
        }

        collision.rigidbody.linearVelocity = Vector3.up * 8f;
    }
}
