using System.Collections.Generic;
using UnityEngine;

public static class CreateCombinations
{
    public static HitCombination GetCombination(string ability)
    {
        switch (ability)
        {
            case "Dash":
                return DashCombo();

            case "Leap":
                return LeapAbility();

            case "Shield":
                return ShieldAbility();

            case "Levitate":
                return LevitateAbility();

            case "Note Control":
                return NoteControlAbility();
            default:
                return null;
        }
    }

    public static HitCombination DashCombo()
    {
        return new HitCombination(
        "Dash",
        new List<HitZone>
        {
            new HitZone(0.35f,0.45f, Note.One),
            new HitZone(0.45f,0.55f, Note.Two),
            new HitZone(0.55f,0.65f, Note.Three)
        }
        );

    }

    public static HitCombination LeapAbility()
    {
        return new HitCombination(
        "Leap",
        new List<HitZone>
        {
            new HitZone(0.75f,0.95f, Note.Three),
            new HitZone(0.45f,0.65f, Note.Two),
            new HitZone(0.05f,0.25f, Note.One)
        }
        );
    }

    public static HitCombination ShieldAbility()
    {
        return new HitCombination(
        "Shield",
        new List<HitZone>
        {
            new HitZone(0.80f,0.95f,Note.Two),
            new HitZone(0.15f,0.30f, Note.One),
            new HitZone(0.50f,0.70f,Note.Three)
        }
        );
    }
    public static HitCombination LevitateAbility()
    {
        return new HitCombination(
        "Levitate",
        new List<HitZone>
        {
            new HitZone(0.45f,0.60f,Note.Three),
            new HitZone(0.30f,0.45f,Note.Two),
            new HitZone(0.15f,0.30f, Note.One)
        }
        );
    }
    //NoteControlAbility
    public static HitCombination NoteControlAbility()
    {
        return new HitCombination(
        "Note Control",
        new List<HitZone>
        {
            new HitZone(0.15f,0.30f, Note.One),
            new HitZone(0.80f,0.99f,Note.Two),
            new HitZone(0.75f,0.90f,Note.Three)
        }
        );
    }

}
