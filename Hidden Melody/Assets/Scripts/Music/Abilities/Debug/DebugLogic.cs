using UnityEngine;

public class DebugLogic : AbilityLogic
{
    public override void Perform()
    {
        Debug.Log("Performed debug");
    }
}
