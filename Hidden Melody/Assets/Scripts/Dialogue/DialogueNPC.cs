using System;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] DialogueData data;


    public void StartDialogue()
    {
        DialogueSystem dialogueSystem = FindAnyObjectByType<DialogueSystem>();
        dialogueSystem.StartDialogue(data);
    }

    public void Talk()
    {
        throw new NotImplementedException("Talk in NPC is no longer used");
        data.GetCurrentDialogue(out string npc, out string dialogue);
        LogDialogue(npc, dialogue);
    }


    /* Debug */
    void LogDialogue(string npc, string dialogue)
    {
        Debug.Log($"{npc}: {dialogue}");
    }
}
