using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class NotesPressedUI : MonoBehaviour
{
    [Header("Hud")]
    [SerializeField] private string[] hudCombination;
    [SerializeField] private int combinationLength = 3;
    [SerializeField] TMP_Text noteOne;
    [SerializeField] TMP_Text noteTwo;
    [SerializeField] TMP_Text noteThree;

    [Header("Instrument Manager")]
    [SerializeField] InstrumentManager instrumentManager;
    Note[] CurrentCombination { get { return instrumentManager.CurrentInstrument.Combination; } }
    [SerializeField] Note[] latestCombination;

    [Header("Other")]
    [SerializeField] int currentIndex = 0;
    [SerializeField] Dictionary<Note, string> NoteToString = new Dictionary<Note, string>();
    Timer resetTimer;
    bool shouldReset;
    bool startedReset;
    bool startedCombination;


    private void Start() 
    {
        resetTimer = new Timer(5.0f);

        CreateDictionary();
        ResetNotes();
        ApplyHUD();
    }

    private void Update()
    {
        if (latestCombination == CurrentCombination) //Combination is the same, no need to change anything
            return;

        if (!shouldReset)
            UpdateCombination();

        SetHUD();
    }

    void SetHUD() 
    {
        if (!instrumentManager.inCombination && !shouldReset) //If not in a combination, clear the combination
            shouldReset = true;

        if (shouldReset && !startedReset && startedCombination)
        {
            startedReset = true;
            StartCoroutine(ResetC());
            /*
            if (resetTimer.TargetTimeReached())
            {
                startedReset = true;
                StartCoroutine(ResetNotes());
            } */

            return;
        }

        //If has a combination 
        while (currentIndex < combinationLength)
        {
            Note n = CurrentCombination[currentIndex];
            if (n == Note.Error) //If reahced end of combination
                break;
            
            if(!startedCombination)
                startedCombination = true;
            SetNote(n);
        }

        ApplyHUD();
    }

    void ApplyHUD()
    {
        noteOne.text = hudCombination[0];
        noteTwo.text = hudCombination[1];
        noteThree.text = hudCombination[2];
    }

    private void SetNote(Note n)
    {
        hudCombination[currentIndex++] = NoteToString[n];
    }

    IEnumerator ResetC() 
    {
        yield return new WaitForSeconds(1.5f);

        ResetNotes();
    }
    void ResetNotes()
    {
        hudCombination = new string[combinationLength];
        for (int i = 0; i < combinationLength; i++)
            hudCombination[i] = "";

        currentIndex = 0;
        latestCombination = new Note[3];

        shouldReset = false;
        startedReset = false;
        startedCombination = false;
    }

    void UpdateCombination() 
    { 
        for(int i = 0; i < combinationLength;i++)
            latestCombination[i] = CurrentCombination[i]; 
    }

    void CreateDictionary()
    {
        NoteToString.Add(Note.Error, "E");
        NoteToString.Add(Note.One, "J");
        NoteToString.Add(Note.Two, "K");
        NoteToString.Add(Note.Three, "L");
    }
}