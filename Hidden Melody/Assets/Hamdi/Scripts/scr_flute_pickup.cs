using UnityEngine;
using UnityEngine.UI;
 
public class scr_flute_pickup : MonoBehaviour
{
    public Image flute;
    void OnTriggerEnter(Collider other)
    {
        scr_player_controller player = other.GetComponent<scr_player_controller>();
        if (player == null) return;
 
        player.hasFlute = true;

            if (flute != null)
        {
            flute.color = Color.white;
        }

        Destroy(gameObject);
    }
}
 