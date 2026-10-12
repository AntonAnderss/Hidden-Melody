using UnityEngine;
using TMPro;
using System.Collections;
public class NotesPressedUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] noteTexts;
    [SerializeField] private float resetDelay = 1f;
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
    }

    private void AddNote(string note)
    {
        if (currentIndex >= noteTexts.Length)
            return;

        noteTexts[currentIndex].text = note;
        currentIndex++;

        if (currentIndex >= noteTexts.Length)
        {
            StartCoroutine(ResetAfterDelay());
        }
    }
    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);

        ResetNotes();
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