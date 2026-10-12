using UnityEngine;

public class DeadlySpikes : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PLayerRespawn player =
            other.GetComponentInParent<PLayerRespawn>();

        if (player != null)
            player.Respawn();
    }
}