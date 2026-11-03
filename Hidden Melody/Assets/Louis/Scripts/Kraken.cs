using System.Collections;
using UnityEngine;

public class Kraken : Enemy
{
    [Header("Throwing")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float flightTime = 1.2f;
    [SerializeField] private float restTime = 2f;

    private Transform player;
    private Renderer bodyRenderer;
    private Color normalColor;
    private int maxHp;
    private GameObject currentBarrel;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bodyRenderer = GetComponentInChildren<Renderer>();
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
    }

    public void Hit(int amount)
    {
        TakeDamage(amount);
        StartCoroutine(Flash(Color.white));
    }

    private IEnumerator Fight()
    {
        while (true)
        {
            yield return new WaitForSeconds(restTime);
            yield return StartCoroutine(AimedRocks());
            yield return new WaitForSeconds(restTime);
            ThrowBarrel();
            yield return new WaitForSeconds(restTime);
            yield return StartCoroutine(RockRain());
        }
    }

    private IEnumerator AimedRocks()
    {
        int rocks = 3;
        float timeBetweenRocks = 0.8f;
        if (IsAngry())
        {
            rocks = 5;
            timeBetweenRocks = 0.5f;
        }

        yield return StartCoroutine(Flash(Color.red));

        for (int i = 0; i < rocks; i++)
        {
            ThrowRock(player.position);
            yield return new WaitForSeconds(timeBetweenRocks);
        }
    }

    private IEnumerator RockRain()
    {
        int rocks = 3;
        if (IsAngry())
        {
            rocks = 5;
        }

        yield return StartCoroutine(Flash(Color.red));

        float offset = -(rocks - 1);
        for (int i = 0; i < rocks; i++)
        {
            ThrowRock(player.position + Vector3.right * offset);
            offset += 2f;
        }
    }

    private void ThrowRock(Vector3 target)
    {
        GameObject rock = Instantiate(rockPrefab, throwPoint.position, Quaternion.identity);
        rock.GetComponent<KrakenRock>().velocity = ThrowVelocity(target);
    }

    private void ThrowBarrel()
    {
        if (currentBarrel != null)
        {
            return;
        }

        currentBarrel = Instantiate(barrelPrefab, throwPoint.position, Quaternion.identity);
        currentBarrel.GetComponent<Rigidbody>().linearVelocity = ThrowVelocity(player.position);
    }

    private Vector3 ThrowVelocity(Vector3 target)
    {
        target.z = throwPoint.position.z;
        Vector3 distance = target - throwPoint.position;
        return distance / flightTime - Physics.gravity * flightTime / 2f;
    }

    private IEnumerator Flash(Color color)
    {
        bodyRenderer.material.color = color;
        yield return new WaitForSeconds(0.3f);
        bodyRenderer.material.color = normalColor;
    }

    private bool IsAngry()
    {
        return hp <= maxHp / 2;
    }
}
