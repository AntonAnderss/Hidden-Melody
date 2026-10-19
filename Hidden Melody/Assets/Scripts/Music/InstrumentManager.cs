/* Author: Anton Andersson */
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentManager : MonoBehaviour
{
    [Header("Instrument")]
    [SerializeField] public Instrument[] instruments;
    [SerializeField] int currentInstrumentID = 0;
    Instrument CurrentInstrument { get { return instruments[currentInstrumentID]; } }

    [Header("Inputs")]
    public InputActionReference noteOne;
    public InputActionReference noteTwo;
    public InputActionReference noteThree;
    public InputActionReference previousInstrument;
    public InputActionReference nextInstrument;

    [Header("Combination")]
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

        previousInstrument.action.started += PreviousInstrument;
        nextInstrument.action.started += NextInstrument;
    }

    private void OnDisable()
    {
        noteOne.action.started -= NoteOne;
        noteTwo.action.started -= NoteTwo;
        noteThree.action.started -= NoteThree;

        previousInstrument.action.started -= PreviousInstrument;
        nextInstrument.action.started -= NextInstrument;
    }

    public void Update()
    {
        if (!inCombination)
            return;

        if (combinationTimer.TargetTimeReached())
            ResetCombination();
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
        if (!CollectedInstrument(currentInstrumentID))
            return;

        if (!inCombination)
            StartCombination();

        if(inCombination)
            if (CurrentInstrument.TryPlayCombination(n)) //If a combination is completed
                ResetCombination();
    }

    /* Combination-based */
    void StartCombination() { inCombination = true; }
    void ResetCombination()
    {
        CurrentInstrument.ClearCombination();
        combinationTimer.Reset();
        inCombination = false;
    }

    /* Switching Between Instruments */
    void PreviousInstrument(InputAction.CallbackContext obj)
    {
        int originalID = currentInstrumentID;
        bool searchingForValidID = true;

        while(searchingForValidID)
        {
            currentInstrumentID--;
            if (currentInstrumentID < 0)
                currentInstrumentID = instruments.Length - 1;

            if (currentInstrumentID == originalID || CollectedInstrument(currentInstrumentID))
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

            if (currentInstrumentID == originalID || CollectedInstrument(currentInstrumentID))
                searchingForValidID = false;
        }
    }

    /* Helper */
    bool CollectedInstrument(int instrumentID) { return instruments[instrumentID].Collected; }
}