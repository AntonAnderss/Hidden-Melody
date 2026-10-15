using UnityEngine;

[CreateAssetMenu(fileName = "GuideData", menuName = "Dialogue/Guide Data")]
public class GuideData : ScriptableObject
{
    public string guideName = "Guide";

    [TextArea(2, 5)]
    public string[] dialogueLines;

    [TextArea(2, 5)]
    public string[] hints;
}