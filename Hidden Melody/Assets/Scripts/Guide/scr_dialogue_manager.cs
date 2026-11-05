using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button closeButton;

    private GuideData currentGuide;
    private int lineIndex;
    private int hintIndex;

    public bool IsOpen => dialoguePanel.activeSelf;

    void Awake()
    {
        Instance = this;
        nextButton.onClick.AddListener(NextLine);
        hintButton.onClick.AddListener(ShowHint);
        closeButton.onClick.AddListener(Close);
    }

    public void Open(GuideData guide)
    {
        currentGuide = guide;
        lineIndex = 0;
        hintIndex = 0;

        nameText.text = guide.guideName;
        dialoguePanel.SetActive(true);
        ShowLine();

    }

    private void ShowLine()
    {
        bodyText.text = currentGuide.dialogueLines[lineIndex];
        bool lastLine = lineIndex >= currentGuide.dialogueLines.Length - 1;
        nextButton.interactable = !lastLine;
    }

    private void NextLine()
    {
        if (lineIndex < currentGuide.dialogueLines.Length - 1)
        {
            lineIndex++;
            ShowLine();
        }
    }

    private void ShowHint()
    {
        if (currentGuide.hints.Length == 0)
        {
            bodyText.text = "I don't have any hints right now.";
            return;
        }

        bodyText.text = currentGuide.hints[hintIndex];

        // move on to the next hint but make sure we don't go out of bounds
        if (hintIndex < currentGuide.hints.Length - 1)
            hintIndex++;
    }

    public void Close()
    {
        dialoguePanel.SetActive(false);
    }
}