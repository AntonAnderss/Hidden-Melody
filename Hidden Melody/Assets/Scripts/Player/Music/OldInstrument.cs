using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using System.Linq;

public class OldInstrument
{
    protected Note[] combination = new Note[3];
    //protected Dictionary<string, Note[]> combinationToString = new Dictionary<string, Note[]>();
    //protected Dictionary<string, Ability> stringToAbility = new Dictionary<string, Ability>();
    protected Dictionary<Note[], OldAbility> combinationToAbility = new Dictionary<Note[], OldAbility>();
    protected Dictionary<Note, OldAbility> noteToAbility = new Dictionary<Note, OldAbility>();

    protected int CombinationLength { get { return combination.Length; } }
    protected int CombinationIndex = 0;

    public virtual void InitInstrument() 
    {
        Debug.Log("Has not overriden InitInstrument");
    }

    public bool PlayedCombination(Note n)
    {
        //Debug.Log("Played: " + n);

        combination[CombinationIndex] = n;
        CombinationIndex++;

        //If completed combination
        if (CombinationIndex == combination.Length)
        {
            /* Debug.Log("Combination:\n" +
                combination[0] + "\n" +
                combination[1] + "\n" +
                combination[2]); */

            //foreach (string abilityName in combinationToString.Keys)
            //{
            /* Debug.Log("Dictionary Combination:\n" +
            combinationToString[abilityName][0] + "\n" +
            combinationToString[abilityName][1] + "\n" +
            combinationToString[abilityName][2]); */
            // if (combinationToString[abilityName].SequenceEqual(combination))
            // {
            //Debug.Log("Found Combination");
            //  stringToAbility[abilityName].Use();
            //  }
            //}

            foreach (Note[] c in combinationToAbility.Keys)
            {
                if (c.SequenceEqual(combination))
                {
                    combinationToAbility[c].Use();
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

    public void ClearCombination() { combination = new Note[CombinationLength]; CombinationIndex = 0; }

    /**/
    public void PlayNote(Note n)
    {
       foreach(Note note in noteToAbility.Keys)
            if(note == n)
                noteToAbility[note].Use();
    }
}
