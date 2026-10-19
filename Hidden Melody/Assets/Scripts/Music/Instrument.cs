/* Author: Anton Andersson */
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Instrument", menuName = "Music Objects/Instrument", order = 0)]
public class Instrument : ScriptableObject
{
    [Header("Instrument Info")]
    public string instrumentName;
    public Texture2D icon;
    public bool Collected = false;

    [Header("Combination")]
    public Note[] Combination;
    public int CombinationLength { get { return 3; } }
    public int CombinationIndex = 0;

    [Header("Abilities")]
    public List<Ability> abilities;
    public List<Ability> noteAbilities;

    public bool TryPlayCombination(Note n)
    {
        AddNoteToCombination(n);
        return CompletedCombination();
    }

    void AddNoteToCombination(Note n) { Combination[CombinationIndex++] = n; }
    bool CompletedCombination()
    {
        if (CombinationIndex == Combination.Length)
        {
            foreach (Ability ability in abilities)
                if (ability.Combination.SequenceEqual(Combination))
                    if (ability.Unlocked)
                        ability.Perform();

            return true;
        }

        return false;
    }
    /* Helper */
    public void ClearCombination() { Combination = new Note[CombinationLength]; CombinationIndex = 0; }
}
