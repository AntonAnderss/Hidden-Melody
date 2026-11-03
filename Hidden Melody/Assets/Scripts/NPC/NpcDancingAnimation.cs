using UnityEngine;

public class NpcDanceController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private InstrumentManager instrumentManager;
    [SerializeField] private float danceDuration = 3f;
    [SerializeField] private float minDanceDuration = 3f;
    [SerializeField] private float maxDanceDuration = 6f;
    [SerializeField] private float minTalkTime = 2f;
    [SerializeField] private float maxTalkTime = 5f;

    private float talkTimer;
    private bool wasInCombination = false;
    private bool playerInRange = false;

    private float danceTimer = 0f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        talkTimer = Random.Range(minTalkTime, maxTalkTime);
    }

    private void Update()
    {
        // Start dance
        if (playerInRange &&
            instrumentManager.inCombination &&
            !wasInCombination)
        {
            danceTimer = Random.Range(3f, 6f);

            int randomDance = Random.Range(1, 7);

            animator.SetInteger("danceIndex", randomDance);
            animator.SetBool("isDancing", true);

            // Stop talking while dancing
            animator.SetInteger("talkIndex", 0);
        }

        // Dance timer
        if (danceTimer > 0f)
        {
            danceTimer -= Time.deltaTime;

            if (danceTimer <= 0f)
            {
                animator.SetBool("isDancing", false);

                // talking timer after dancing
                talkTimer = Random.Range(minTalkTime, maxTalkTime);
            }
        }

        // Normal idle/talking 
        if (danceTimer <= 0f)
        {
            talkTimer -= Time.deltaTime;

            if (talkTimer <= 0f)
            {
                int randomTalk = Random.Range(1, 4);

                animator.SetInteger("talkIndex", randomTalk);

                talkTimer = Random.Range(minTalkTime, maxTalkTime);
            }
        }

        wasInCombination = instrumentManager.inCombination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}