using UnityEngine;

public class DeadlySpikes : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerRespawn player =
            other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
            player.Respawn();
    }
}