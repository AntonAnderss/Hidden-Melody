using System;
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
    Instrument CurrentInstrument { get { return instruments[0]; } }

    public InputActionReference modeChange;
    public InputActionReference noteOne;
    public InputActionReference noteTwo;
    public InputActionReference noteThree;

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
    }

    private void OnDisable()
    {
        noteOne.action.started -= NoteOne;
        noteTwo.action.started -= NoteTwo;
        noteThree.action.started -= NoteThree;

        modeChange.action.started -= ChangeMode;
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
        //if (playMode == PlayMode.Combination)
            //Debug.Log("PlayeNote check");

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
}
