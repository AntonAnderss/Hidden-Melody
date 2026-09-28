using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class Ability 
{
    public virtual void Use() { Debug.Log("Played Default Ability"); }
}

public enum Note { Error, One, Two, Three }

public class Instrument
{
    protected Note[] combination = new Note[3];
    //protected Dictionary<string, Note[]> combinationToString = new Dictionary<string, Note[]>();
    //protected Dictionary<string, Ability> stringToAbility = new Dictionary<string, Ability>();
    protected Dictionary<Note[], Ability> combinationToAbility = new Dictionary<Note[], Ability>();
    protected Dictionary<Note, Ability> noteToAbility = new Dictionary<Note, Ability>();

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

            if(combinationToAbility.ContainsKey(combination))
            {
                foreach (Note[] c in combinationToAbility.Keys)
                {
                    if(c.SequenceEqual(combination))
                    {
                        combinationToAbility[combination].Use();
                    }
                }
            }

            foreach (Note[] c in combinationToAbility.Keys)
            {
                if (c.SequenceEqual(combination))
                {
                    combinationToAbility[c].Use();
                }
            }

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
