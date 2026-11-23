using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPuzzle : MonoBehaviour
{
    private Transform player;
    public Transform lockCube;
    public Transform gate;
    public float gateLift = 4f;

    //gate speed normalised 
    public float gateSpeed = 0.5f;
    private bool isOpening;
    private Vector3 gateTarget;

    private bool hasKey;


    public AudioSource gateAudio;
    public ParticleSystem gateParticles;

    public InputActionReference interact;

    void OnEnable()
    {
        interact.action.Enable();
    }


    void Update()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            player = null;

            foreach (GameObject candidate in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (candidate.TryGetComponent<PlayerMovement>(out _))
                {
                    player = candidate.transform;
                    break;
                }
            }

            if (player == null)
                return;
        }


        if (!hasKey)
        {

            if (Vector3.Distance(transform.position, player.position) > 1.5f)
                return;

            hasKey = true;
        }

        // Makes the key hover aobe the player
        transform.position = player.position + Vector3.up * 2f;

        if (isOpening)
        {
           

            gate.position = Vector3.MoveTowards(
                gate.position, gateTarget, gateSpeed * Time.deltaTime);

            if (gate.position == gateTarget)
                Destroy(gameObject);


            return;
        }

        if (Vector3.Distance(player.position, lockCube.position) < 2f
            && interact.action.WasPressedThisFrame())
        {
            gateTarget = gate.position + Vector3.up * gateLift;
            isOpening = true;
            gateAudio.Play();
            //gateParticles.Play();
        }





    }
}