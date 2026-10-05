using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Instrument", menuName = "Music Objects/Instrument", order = 0)]
public class Instrument : ScriptableObject
{
    [Header("Instrument Info")]
    public string instrumentName;

    [Header("Combination")]
    public Note[] Combination;
    public int CombinationLength { get { return 3; } }
    public int CombinationIndex = 0;

    [Header("Abilities")]
    public List<Ability> abilities;
    public List<Ability> noteAbilities;

    public bool PlayedCombination(Note n)
    {
        //Debug.Log("Played: " + n);

        Combination[CombinationIndex] = n;
        CombinationIndex++;

        //If completed combination
        if (CombinationIndex == Combination.Length)
        {
            foreach (Ability ability in abilities)
            {
                if (ability.Combination.SequenceEqual(Combination))
                {
                    ability.Perform();
                }
            }

            //foreach (Note[] c in combinationToAbility.Keys)
            //{
            //    if (c.SequenceEqual(combination))
            //    {
            //        combinationToAbility[c].Use();

            //        Debug.Log("Found Combination : " + abilityName);
            //        combinationToAbility[abilityName].Use();
            //    }
            //}

            ClearCombination();
            return true;
        }

        return false;
    }

    public void ClearCombination() { Combination = new Note[CombinationLength]; CombinationIndex = 0; }

    /**/
    public void PlayNote(Note n)
    {
        Debug.Log("Hasn't been done");
        return;

        //foreach (Ability ability in abilities)
            //if (ability.Combination[0] )
               //noteToAbility[note].Use();
    }
}
