using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using System.Linq;

/* ToDo
 *  Add Timers
 *  Clean Up code
 *  Split Up into different files
 */

[Serializable]
class Ability 
{
    public virtual void Use() { Debug.Log("Played Default Ability"); }
}

class TestAbility : Ability
{
    public override void Use() { Debug.Log("Played Test"); }

}

enum Note { Error, One, Two, Three }

public class Instrument : MonoBehaviour
{
    [SerializeField] Note[] combination = new Note[3];
    [SerializeField] Dictionary<string, Note[]> combinationToString = new Dictionary<string, Note[]>();
    [SerializeField] Dictionary<string, Ability> stringToAbility = new Dictionary<string, Ability>();

    int CombinationLength { get { return combination.Length; } }
    int CombinationIndex = 0;

    public InputActionReference noteOne;
    public InputActionReference noteTwo;
    public InputActionReference noteThree;

    int frameCounter = 0;
    int framesNeeded = 20;
    bool canPress = true;

    private void Start()
    {
        combinationToString.Add("Test", new Note[]{ Note.One, Note.Two, Note.Three });
        stringToAbility.Add("Test", new TestAbility());
    }

    private void OnEnable()
    {
        noteOne.action.started += NoteOne;
        noteTwo.action.started += NoteTwo;
        noteThree.action.started += NoteThree;

    }

    private void OnDisable()
    {
        noteOne.action.started -= NoteOne;
        noteTwo.action.started -= NoteTwo;
        noteThree.action.started -= NoteThree;
    }
    private void FixedUpdate() //FixedTimeStep = 0.02 / 50FPS
    {
        if (canPress == true)
            return;

        if (++frameCounter >= framesNeeded)
            return;

        canPress = true;
    }

    void NoteOne(InputAction.CallbackContext obj)
    {
        PlayedNote(Note.One);
    }
    void NoteTwo(InputAction.CallbackContext obj)
    {
        PlayedNote(Note.Two);
    }
    void NoteThree(InputAction.CallbackContext obj)
    {
        PlayedNote(Note.Three);
    }

    void PlayedNote(Note n)
    {
        //Debug.Log("Played: " + n);

        combination[CombinationIndex] = n;
        CombinationIndex++;

        //If completed combination
        if (CombinationIndex == combination.Length)
        {
            foreach (string abilityName in combinationToString.Keys)
            {
                /*Debug.Log("Combination:\n" +
                combination[0] + "\n" +
                combination[1] + "\n" +
                combination[2]);

                Debug.Log("Dictionary Combination:\n" +
                combinationToString[abilityName][0] + "\n" +
                combinationToString[abilityName][1] + "\n" +
                combinationToString[abilityName][2]); */
                if (combinationToString[abilityName].SequenceEqual(combination))
                {
                    //Debug.Log("Found Combination");
                    stringToAbility[abilityName].Use();
                }
            }

            ClearCombination();
        }

        canPress = false;
        frameCounter = 0;
    }

    void ClearCombination() { combination = new Note[CombinationLength]; CombinationIndex = 0; }
}
