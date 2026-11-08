using System.Collections.Generic;
using UnityEngine;

public class LevitateAbilityController : MonoBehaviour
{
    [SerializeField] private List<LevitateController> objectToLevitate;

    public void ActivateLevitate()
    {
        if (objectToLevitate == null)
            return;

        for(int i = 0; objectToLevitate.Count > i; i++)
        {
            objectToLevitate[i].Levitate();
        }
    }
}