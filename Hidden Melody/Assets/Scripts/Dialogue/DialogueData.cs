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
        npcName = name;
        dialogue = Dialogue[dialogueIndex++];

        if(dialogueIndex >= Dialogue.Length) 
            dialogueIndex = 0;
    }
}
