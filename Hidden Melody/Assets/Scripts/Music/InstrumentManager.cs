/* Author: Anton Andersson */
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentManager : MonoBehaviour
{
    [Header("Instrument")]
    [SerializeField] public Instrument[] instruments;
    [SerializeField] int currentInstrumentID = 0;
    public Instrument CurrentInstrument { get { return instruments[currentInstrumentID]; } }

    [Header("Inputs")]
    public InputActionReference noteOne;
    public InputActionReference noteTwo;
    public InputActionReference noteThree;
    public InputActionReference previousInstrument;
    public InputActionReference nextInstrument;

    [Header("Combination")]
    [SerializeField] public bool inCombination = false;
    bool shouldReset = false;
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

        if (combinationTimer.TargetTimeReached() || shouldReset)
            ResetCombination();
    }

    /* Note Change*/
    void NoteOne(InputAction.CallbackContext obj) { PlayNote(Note.One); }
    void NoteTwo(InputAction.CallbackContext obj) { PlayNote(Note.Two); }
    void NoteThree(InputAction.CallbackContext obj) { PlayNote(Note.Three); }

    void PlayNote(Note n)
    {
        if (!CollectedInstrument(currentInstrumentID))
            return;

        if (!inCombination)
            StartCombination();

        if (inCombination)
            if (CurrentInstrument.TryPlayCombination(n)) //If a combination is completed
                shouldReset = true;
    }

    /* Combination-based */
    void StartCombination() { inCombination = true; }
    void ResetCombination()
    {
        CurrentInstrument.ClearCombination();
        combinationTimer.Reset();
        inCombination = false;
        shouldReset = false;
    }

    /* Switching Between Instruments */
    public int CurrentInstrumentIndex { get { return currentInstrumentID; } }
    /// <summary>
    /// Returns the Index of the previous Collected Instrument
    /// </summary>
    public int GetPreviousInstrumentIndex()
    {
        int originalID = currentInstrumentID;
        int searchID = currentInstrumentID;
        bool searchingForValidID = true;

        while (searchingForValidID)
        {
            searchID++;
            if (searchID > instruments.Length - 1)
                searchID = 0;

            if (searchID == originalID || CollectedInstrument(searchID))
                searchingForValidID = false;
        }

        return searchID;
    }
    /// <summary>
    /// Returns the Index of the next Collected Instrument
    /// </summary>
    public int GetNextInstrumentIndex()
    {
        int originalID = currentInstrumentID;
        int searchID = currentInstrumentID;
        bool searchingForValidID = true;

        while (searchingForValidID)
        {
            searchID++;
            if (searchID > instruments.Length - 1)
                searchID = 0;

            if (searchID == originalID || CollectedInstrument(searchID))
                searchingForValidID = false;
        }

        return searchID;
    }
    void PreviousInstrument(InputAction.CallbackContext obj) { currentInstrumentID = GetPreviousInstrumentIndex(); }
    void NextInstrument(InputAction.CallbackContext obj) { currentInstrumentID = GetNextInstrumentIndex(); }

    /* Helper */
    bool CollectedInstrument(int instrumentID) { return instruments[instrumentID].Collected; }

    /* HUD Functions */
    /// <summary>
    /// Returns the amount of collected instruments
    /// </summary>
    public int GetInstrumentCount()
    {
        int i = 0;
        foreach (Instrument instrument in instruments)
            if (instrument != null && instrument.Collected)
                i++;

        return i;
    }
    /// <summary>
    /// Returns the amount of instruments. Both collected & uncollected
    /// </summary>
    public int GetTotalInstrumentCount()
    {
        int i = 0;
        foreach (Instrument instrument in instruments)
            if (instrument != null)
                i++;

        return i;
    }

    public Instrument GetPreviousInstrument() { return instruments[GetPreviousInstrumentIndex()]; }
    public Instrument GetNextInstrument() { return instruments[GetNextInstrumentIndex()]; }
}