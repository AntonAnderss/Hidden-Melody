using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    [Header("System")]
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] bool inDialogue;
    [SerializeField] bool shouldStop;
    [SerializeField] InputActionReference talk;

    [Header("NPC Variable")]
    [SerializeField] DialogueData currentData;

    private void OnEnable() { talk.action.started += AdvanceDialogue; }
    private void OnDisable() { talk.action.started -= AdvanceDialogue; }

    public void StartDialogue(DialogueData data)
    {
        // If dialogue box isn't valid, the dialogue shouldn't start
        if (!ValidateBox() || !ValidateText())
            return;

        //Prepares for starting dialogue
        currentData = data;
        dialogueBox.SetActive(true);
        SetMapToDialogue();
        
        //Starts dialogue
        inDialogue = true;
        AdvanceDialogue();
    }

    void AdvanceDialogue(InputAction.CallbackContext obj) { AdvanceDialogue(); }
    void AdvanceDialogue()
    {
        if (!CurrentlyTalking())
            return;

        if(shouldStop)
        {
            StopDialogue();
            return;
        }

        dialogueText.text = currentData.GetCurrentDialogueString();
        if(!currentData.AdvanceDialogue())
            shouldStop = true;
    }

    private void StopDialogue()
    {
        dialogueBox.SetActive(false);
        currentData = null;
        SetMapToGameplay();

        inDialogue = false;
        shouldStop = false;
    }



    /* Player based */
    GameObject GetPlayer() { return GameObject.FindWithTag("Player"); }
    InputManager GetInputManager() { return GetPlayer().GetComponent<InputManager>(); }
    void SetMapToDialogue() { GetInputManager().SetMapToDialogue(); }
    void SetMapToGameplay() { GetInputManager().SetMapToGameplay(); }

    /* Validation */
    bool ValidateBox()
    {
        if (dialogueBox != null)
            return true;

        if (dialogueBox.tag != "DialogueBox")
            return true;

        Debug.LogError("Dialoge box is not valid");
        return false;
    }

    bool ValidateText()
    {
        if (dialogueText != null)
            return true;

        Debug.LogError("Dialoge text is not valid");
        return false;
    }

    bool CurrentlyTalking()
    {
        if (inDialogue && currentData != null)
            return true;

        Debug.LogError("Currently not talking");
        return false;
    }
}
