using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using System.Linq;

/* ToDo
 *  Add Timers
 *  Clean Up code
 */

[Serializable]
//public class Ability 
//{
//    public virtual void Use() { Debug.Log("Played Default Ability"); }
//}

public enum Note { Error, One, Two, Three }

public class Instrument
{
    protected Note[] combination = new Note[3];
    protected Dictionary<string, Note[]> combinationToString = new Dictionary<string, Note[]>();
    protected Dictionary<string, Ability> stringToAbility = new Dictionary<string, Ability>();

    protected int CombinationLength { get { return combination.Length; } }
    protected int CombinationIndex = 0;

    public virtual void InitInstrument() 
    {
        Debug.Log("Has not overriden InitInstrument");
    }

    public bool PlayedNote(Note n)
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

            foreach (string abilityName in combinationToString.Keys)
            {
                /* Debug.Log("Dictionary Combination:\n" +
                combinationToString[abilityName][0] + "\n" +
                combinationToString[abilityName][1] + "\n" +
                combinationToString[abilityName][2]); */
                if (combinationToString[abilityName].SequenceEqual(combination))
                {
                    Debug.Log("Found Combination : " + abilityName);
                    stringToAbility[abilityName].Use();
                }
            }

            ClearCombination();
            return true;
        }

        return false;
    }

    public void ClearCombination() { combination = new Note[CombinationLength]; CombinationIndex = 0; }
}
