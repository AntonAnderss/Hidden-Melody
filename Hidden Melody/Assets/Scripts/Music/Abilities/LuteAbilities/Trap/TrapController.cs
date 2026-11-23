using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private Transform trapSpawnPoint;

    private GameObject currentTrap;

    public void UseTrap()
    {
        // No trap placed yet
        if (currentTrap == null)
        {
            PlaceTrap();
        }
        else
        {
            ActivateTrap();
        }
    }

    private void PlaceTrap()
    {
        currentTrap = Instantiate(trapPrefab,trapSpawnPoint.position,Quaternion.identity);

        Debug.Log("Trap placed");
    }

    private void ActivateTrap()
    {
        TrapObject trap = currentTrap.GetComponent<TrapObject>();

        if (trap != null)
        {
            trap.Activate();
        }

        currentTrap = null;

        Debug.Log("Trap activated");
    }
}