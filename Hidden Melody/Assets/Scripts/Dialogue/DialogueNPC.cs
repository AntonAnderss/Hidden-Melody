using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] DialogueData data;

    public void Talk()
    {
        data.GetCurrentDialogue(out string npc, out string dialogue);
        LogDialogue(npc, dialogue);
    }


    /* Debug */
    void LogDialogue(string npc, string dialogue)
    {
        Debug.Log($"{npc}: {dialogue}");
    }
}
