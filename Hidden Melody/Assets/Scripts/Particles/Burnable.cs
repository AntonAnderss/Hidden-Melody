using UnityEngine;

public class Burnable : MonoBehaviour
{
    public Transform player;
    public ParticleSystem flames;

    private bool burning;

    void Start()
    {
        if (flames != null)
            flames.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        if (burning || player == null || flames == null) return;

        if (Input.GetKeyDown(KeyCode.E) &&
            Vector3.Distance(player.position, transform.position) <= 3f)
        {
            burning = true;
            flames.gameObject.SetActive(true);
            flames.Play();

            Destroy(gameObject, 2f);
        }
    }

}
