using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public static class AbilityUnlockState
{
    private static List<AbilityType> unlockedAbilities = new List<AbilityType>();

    public static void Unlock(AbilityType ability)
    {
        if(!unlockedAbilities.Contains(ability))
        {
            unlockedAbilities.Add(ability);

            Debug.Log("Ability unlocked : "+ ability);
        }
    }


    public static bool IsUnlocked(AbilityType ability)
    {
        return unlockedAbilities.Contains(ability);
    }
}
