using UnityEngine;
using TMPro;
using System.Collections;
public class NotesPressedUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] noteTexts;
    [SerializeField] private float resetDelay = 0.5f;
    [SerializeField] InstrumentManager instrumentManager;
    private float resetTimer = 0f;
    private bool waitingForReset = false;
    private bool inCombination = false;
    private int currentIndex = 0;
    

    private void Start()
    {
        ResetNotes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddNote("J");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AddNote("K");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            AddNote("L");
        }
        if (inCombination && !instrumentManager.inCombination)
        {
            waitingForReset = true;
            resetTimer = resetDelay;
            Debug.Log("Reset Notes");
        }

        inCombination = instrumentManager.inCombination;

        if(waitingForReset)
        {
            resetTimer -= Time.deltaTime;
            if(resetTimer <= 0f)
            {
                ResetNotes();
                waitingForReset=false;
            }
        }
    }

    private void AddNote(string note)
    {
        if (currentIndex >= noteTexts.Length)
            return;

        noteTexts[currentIndex].text = note;
        currentIndex++;

        if (currentIndex >= noteTexts.Length && !instrumentManager.inCombination && resetTimer <= 0f)
        {
            waitingForReset = true;
            resetTimer = resetDelay;
        }
    }

    public void ResetNotes()
    {
        currentIndex = 0;

        for (int i = 0; i < noteTexts.Length; i++)
        {
            noteTexts[i].text = "";
        }
    }
}