using UnityEngine;


/*
 * Author: Clara Lönnkrans
 * 
*/
public class WildBoar : Enemy
{
    private GameObject player;
    private Rigidbody playerRigidbody;
    private HealthSystem healthSystem;
    private int currentDirection;
    private float rushTimer;
    private float currentRushTime;
    private float cooldownTimer;
    private float currentCooldownTime;
    private Rigidbody rigidbody;
    private float speed = 7;
    private bool playerSpotted;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody>();
        healthSystem = player.GetComponent<HealthSystem>();

        damage = 10;
        currentDirection = 1;
        rushTimer = 4;
        cooldownTimer = 2;
        currentRushTime = 5;
        currentCooldownTime = 2;
        rigidbody = GetComponent<Rigidbody>();

        playerSpotted = false;
    }
    void Update()
    {
        if(playerSpotted)
        {
            currentCooldownTime += Time.deltaTime;
            Debug.Log(cooldownTimer);
            if(currentCooldownTime > cooldownTimer)
            {
                Debug.Log("Rush");
                RushPlayer();
            }
        }
    }
    void ChooseRushDirection()
    {
        int playerPos = (int)player.transform.position.x;
        int direction = playerPos - (int)transform.position.x;
        if (direction < 0)
        {
            currentDirection = -1;
            //Change rotation
        }
        else
        {
            currentDirection = 1;
            //Change rotation
        }
    }
    void RushPlayer()
    {
        if (currentRushTime > rushTimer)
        {
            ChooseRushDirection();
            ResetTimers();
        }
        currentRushTime += Time.deltaTime;
        rigidbody.linearVelocity = new Vector3(currentDirection * speed, rigidbody.linearVelocity.y, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerSpotted = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerSpotted = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            HitPlayer();
        }
        else
        {
            HitSomething();
        }
    }
    void HitSomething()
    {
        //Play animation
        //rigidbody.linearVelocity = Vector3.zero;
        //rigidbody.linearVelocity = new Vector3(-currentDirection * speed * 2, rigidbody.linearVelocity.y + 3, 0);
        //currentRushTime = 0;
    }
    void HitPlayer()
    {
        rigidbody.linearVelocity = new Vector3(-currentDirection * speed * 1.5f, rigidbody.linearVelocity.y + 3, 0);
        playerRigidbody.linearVelocity = new Vector3(currentDirection * speed * 2, rigidbody.linearVelocity.y + 3, 0);
        healthSystem.LooseHealth(damage);
        ResetTimers();
    }
    void ResetTimers()
    {
        currentRushTime = 0;
        currentCooldownTime = 0;
    }
    void Killed()
    {
        //Play animation
    }
}
