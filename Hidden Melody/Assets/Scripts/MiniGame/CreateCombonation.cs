using System.Collections.Generic;
using UnityEngine;

public static class CreateCombinations
{
    public static HitCombination GetCombination(AbilityType ability)
    {
        switch (ability)
        {
            case AbilityType.Dash:
                return DashCombo();

            case AbilityType.DoubleJump:
                return DoubleJumpCombo();

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
            new HitZone(0.35f,0.45f),
            new HitZone(0.45f,0.55f),
            new HitZone(0.55f,0.65f)
        }
        );

    }

    public static HitCombination DoubleJumpCombo()
    {
        return new HitCombination(
        "DoubleJumpAbility",
        new List<HitZone>
        {
            new HitZone(0.05f,0.25f),
            new HitZone(0.45f,0.65f),
            new HitZone(0.75f,0.95f)
        }
        );


    }

    
}
