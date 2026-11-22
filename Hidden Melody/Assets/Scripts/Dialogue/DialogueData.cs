using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [SerializeField] string name;
    public string Name { get { return name; } }

    [SerializeField] string[] dialogue;
    public string[] Dialogue { get { return dialogue; } }

    [Header("Logic")]
    int dialogueIndex = 0;

    public void GetCurrentDialogue(out string npcName, out string dialogue)
    {
        throw new NotImplementedException("No longr used");

        npcName = name;
        dialogue = Dialogue[dialogueIndex++];

        if(dialogueIndex >= Dialogue.Length) 
            dialogueIndex = 0;
    }

    public string GetCurrentDialogueString() { return $"{name}: {dialogue[dialogueIndex]}"; }

    public bool AdvanceDialogue()
    {
        dialogueIndex++;

        if(dialogueIndex >= dialogue.Length)
        {
            dialogueIndex = 0;
            return false;
        }

        return true;
    }

    public void Reset()
    {
        dialogueIndex = 0;
    }
}
