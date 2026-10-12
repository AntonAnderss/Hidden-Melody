using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

/* ToDo
 *  Add GUI
 *  Add instrument management
 *  Make instruments to Scriptable Objects
 *  Add unlockable abilities (In abilities)
 *  Add Timer for Note Abilities
 *  Refactor
 */

public class InstrumentManager : MonoBehaviour
{
    public enum PlayMode { Combination, Single }
    public PlayMode playMode;

    [SerializeField] public Instrument[] instruments;
    [SerializeField] int currentInstrumentID = 0;
    Instrument CurrentInstrument { get { return instruments[currentInstrumentID]; } }

    public InputActionReference modeChange;
    public InputActionReference noteOne;
    public InputActionReference noteTwo;
    public InputActionReference noteThree;
    public InputActionReference previousInstrument;
    public InputActionReference nextInstrument;


    [SerializeField] bool inCombination = false;
    [SerializeField] float combinationTime = 1.5f;
    Timer combinationTimer;

    private void Start()
    {
        combinationTimer = new Timer(combinationTime);
    }

    private void OnEnable()
    {
        noteOne.action.started += NoteOne;
        noteTwo.action.started += NoteTwo;
        noteThree.action.started += NoteThree;

        modeChange.action.started += ChangeMode;

        previousInstrument.action.started += PreviousInstrument;
        nextInstrument.action.started += NextInstrument;
    }

    private void OnDisable()
    {
        noteOne.action.started -= NoteOne;
        noteTwo.action.started -= NoteTwo;
        noteThree.action.started -= NoteThree;

        modeChange.action.started -= ChangeMode;

        previousInstrument.action.started -= PreviousInstrument;
        nextInstrument.action.started -= NextInstrument;

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
            CurrentInstrument.ClearCombination();
            inCombination = false;
        }
    }

    /* Note Change*/
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
        if (!CurrentInstrument.Collected)
            return;

        if(playMode == PlayMode.Combination)
        {
            if (!inCombination)
            {
                inCombination = true;
                combinationTimer.Reset();
            }

            if (CurrentInstrument.PlayedCombination(n)) //If reached a combination
                inCombination = false;
        }
        
        if(playMode == PlayMode.Single)
        {
            CurrentInstrument.PlayNote(n);
        }
    }

    /* Mode Change*/
    void ChangeMode(InputAction.CallbackContext obj) 
    {
        ModeSwitch();
        ResetCombination();
    }

    void ModeSwitch()
    {
        if (playMode == PlayMode.Combination)
            playMode = PlayMode.Single;
        else if (playMode == PlayMode.Single)
            playMode = PlayMode.Combination;
    }

    void ResetCombination()
    {
        CurrentInstrument.ClearCombination();
        combinationTimer.Reset();
        inCombination = false;
    }

    void PreviousInstrument(InputAction.CallbackContext obj)
    {
        int originalID = currentInstrumentID;
        bool searchingForValidID = true;

        while(searchingForValidID)
        {
            currentInstrumentID--;
            if (currentInstrumentID < 0)
                currentInstrumentID = instruments.Length - 1;

            if (currentInstrumentID == originalID || CurrentInstrument.Collected == true)
                searchingForValidID = false;
        }
    }

    void NextInstrument(InputAction.CallbackContext obj)
    {
        int originalID = currentInstrumentID;
        bool searchingForValidID = true;

        while (searchingForValidID)
        {
            currentInstrumentID++;
            if (currentInstrumentID > instruments.Length - 1)
                currentInstrumentID = 0;

            if (currentInstrumentID == originalID || CurrentInstrument.Collected == true)
                searchingForValidID = false;
        }
    }
}
