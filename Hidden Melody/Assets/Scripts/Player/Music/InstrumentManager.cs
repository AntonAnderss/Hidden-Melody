using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentManager : MonoBehaviour
{
    [SerializeField] Instrument currInstrument;
    //public Note[] combination = new Note[3];

    public InputActionReference noteOne;
    public InputActionReference noteTwo;
    public InputActionReference noteThree;

    [SerializeField] bool inCombination = false;
    [SerializeField] float combinationTime = 1.5f;
    Timer combinationTimer;

    private void Start()
    {
        currInstrument = new TestInstrument();
        currInstrument.InitInstrument();

        combinationTimer = new Timer(combinationTime);
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

    public void Update()
    {
        //for(int i = 0; i < 3;  i++)
        //{
            //combination[i] = currInstrument.combination[i];
        //}

        if (!inCombination)
            return;

        if(combinationTimer.TargetTimeReached())
        {
            currInstrument.ClearCombination();
            inCombination = false;
        }
    }

    void NoteOne(InputAction.CallbackContext obj)
    {
        PlayNote(Note.One);
    }
    void NoteTwo(InputAction.CallbackContext obj)
    {
        PlayNote(Note.Two);
    }
    void NoteThree(InputAction.CallbackContext obj)
    {
        PlayNote(Note.Three);
    }

    void PlayNote(Note n)
    {
        if(!inCombination)
        {
            inCombination = true;
            combinationTimer.Reset();
        }

        if(currInstrument.PlayedNote(n)) //If reached a combination
            inCombination = false;
    }
}
