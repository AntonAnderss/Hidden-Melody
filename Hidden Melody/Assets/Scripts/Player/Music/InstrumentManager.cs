using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* ToDo
 *  Add GUI
 *  Add instrument management
 *  Add unlockable abilities
 *  Add Timer for Note Abilities
 *  Refactor
 */

public class InstrumentManager : MonoBehaviour
{
    public enum PlayMode { Combination, Single }
    public PlayMode playMode;

    [SerializeField] Instrument currInstrument;
    //public Note[] combination = new Note[3];

    public InputActionReference modeChange;
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
            currInstrument.ClearCombination();
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
        if (playMode == PlayMode.Combination)
        {
            if (!inCombination)
            {
                inCombination = true;
                combinationTimer.Reset();
            }

            if (currInstrument.PlayedCombination(n)) //If reached a combination
                inCombination = false;
        }
        
        if(playMode == PlayMode.Single)
        {
            currInstrument.PlayNote(n);
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
        currInstrument.ClearCombination();
        combinationTimer.Reset();
        inCombination = false;
    }
}
