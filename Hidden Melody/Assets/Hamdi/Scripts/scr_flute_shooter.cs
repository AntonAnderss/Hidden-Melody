using UnityEngine;


public class scr_flute_shooter : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform firePoint;
    public float noteSpeed = 10f;

    public void Shoot(Vector3 direction)
    {
        if (notePrefab == null) return;

        Transform spawnPoint = firePoint != null ? firePoint : transform;
        GameObject noteObj = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);

        scr_flute_note note = noteObj.GetComponent<scr_flute_note>();
        if (note != null)
            note.Init(direction, noteSpeed);
    }
}