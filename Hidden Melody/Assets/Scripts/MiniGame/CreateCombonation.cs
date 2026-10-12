using System.Collections.Generic;
using UnityEngine;

public static class CreateCombinations
{
    public static HitCombination GetCombination(string ability)
    {
        switch (ability)
        {
            case "DashAbility":
                return DashCombo();

            case "LeapAbility":
                return LeapAbility();

            case "ShieldAbility":
                return ShieldAbility();

            default:
                return null;
        }
    }

    public static HitCombination DashCombo()
    {
        return new HitCombination(
        "DashAbility",
        new List<HitZone>
        {
            new HitZone(0.35f,0.45f, KeyCode.J),
            new HitZone(0.45f,0.55f, KeyCode.K),
            new HitZone(0.55f,0.65f, KeyCode.L)
        }
        );

    }

    public static HitCombination LeapAbility()
    {
        return new HitCombination(
        "LeapAbility",
        new List<HitZone>
        {
            new HitZone(0.05f,0.25f, KeyCode.J),
            new HitZone(0.45f,0.65f, KeyCode.K),
            new HitZone(0.75f,0.95f, KeyCode.L)
        }
        );
    }

    public static HitCombination ShieldAbility()
    {
        return new HitCombination(
        "ShieldAbility",
        new List<HitZone>
        {
            new HitZone(0.15f,0.30f, KeyCode.J),
            new HitZone(0.80f,0.95f,KeyCode.K),
            new HitZone(0.50f,0.70f,KeyCode.L)
        }
        );
    }

    
}
