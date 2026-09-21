using UnityEngine;
using UnityEngine.InputSystem;

enum Note { Error, One, Two, Three }

public class Instrument : MonoBehaviour
{
    [SerializeField] Note[] combination = new Note[3];
    int CombinationLength { get { return combination.Length; } }
    int CombinationIndex = 0;

    public InputActionReference noteOne;
    public InputActionReference noteTwo;
    public InputActionReference noteThree;

    int frameCounter = 0;
    int framesNeeded = 20;
    bool canPress = true;

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
        Debug.Log("Played: " + n);

        combination[CombinationIndex] = n;
        CombinationIndex++;

        //If completed combination
        if (CombinationIndex == combination.Length)
        {
            Debug.Log("Combination:\n" +
                combination[0] + "\n" +
                combination[1] + "\n" +
                combination[2]);

            ClearCombination();
        }

        canPress = false;
        frameCounter = 0;
    }

    void ClearCombination() { combination = new Note[CombinationLength]; CombinationIndex = 0; }
}
