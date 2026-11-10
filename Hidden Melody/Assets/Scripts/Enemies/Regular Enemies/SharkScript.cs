using System.Collections;
using UnityEngine;

public class SharkScript : MonoBehaviour
{
    [SerializeField] float speed = 2.5f;
    private GameObject player;
    private HealthSystem healthSystem;
    private PlayerMovement playerMovement;
    private float damage = 10;
    private Vector3 startPos;
    [SerializeField] float jumpHeight = 4;
    private bool isJumping = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();
        playerMovement = player.GetComponent<PlayerMovement>();
        startPos = transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player in shark");
            if (playerMovement.IsGrounded == false && isJumping == false)
            {
                isJumping = true;
                StartCoroutine(JumpUp());
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            healthSystem.LooseHealth(damage);
        }
    }
    private IEnumerator JumpDown()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        while (transform.position.y > startPos.y - 2)
        {
            transform.position += new Vector3(0, -5f * Time.deltaTime, 0);
            yield return null;
        }
        StartCoroutine(SwimUp());
    }
    private IEnumerator JumpUp()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        while (transform.position.y < startPos.y + jumpHeight)
        {
            transform.position += new Vector3(0, 13f * Time.deltaTime, 0);
            yield return null;
        }
        StartCoroutine(JumpDown());
    }
    private IEnumerator SwimUp()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        while (transform.position.y < startPos.y)
        {
            transform.position += new Vector3(0, 3f * Time.deltaTime, 0);
            yield return null;
        }
        isJumping = false;
    }
}
