using UnityEngine;

public class LevitateAbilityController : MonoBehaviour
{
    [SerializeField] private LevitateController objectToLevitate;

    public void ActivateLevitate()
    {
        if (objectToLevitate == null)
            return;

        objectToLevitate.Levitate();
    }
}