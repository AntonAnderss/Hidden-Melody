using UnityEngine;
using UnityEngine.UI;

public class InstrumentSwitcher : MonoBehaviour
{
    [SerializeField] private RectTransform[] instruments;
    [SerializeField] private InstrumentManager instrumentManager;

    [Header("Positions")]
    [SerializeField] private Vector2 leftPosition;
    [SerializeField] private Vector2 centerPosition;
    [SerializeField] private Vector2 rightPosition;

    //[Header("Image")]
    //[SerializeField] private Image leftInstrument;
    //[SerializeField] private Image currentInstrument;
    //[SerializeField] private Image rightInstrument;

    [Header("Scale")]
    [SerializeField] private float centerScale = 0.8f;
    [SerializeField] private float sideScale = 0.5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;


    private void Update()
    {
        UpdateInstrumentPositions();
    }

    private void UpdateInstrumentPositions()
    {
        int collectedCount = instrumentManager.GetInstrumentCount();

        int currentIndex = instrumentManager.CurrentInstrumentIndex;
        int leftIndex = instrumentManager.GetPreviousInstrumentIndex();
        int rightIndex = instrumentManager.GetNextInstrumentIndex();

        // Hide all instrument images first
        foreach (RectTransform instrument in instruments)
        {
            instrument.gameObject.SetActive(false);
        }

        // No collected instruments
        if (collectedCount == 0)
        {
            return;
        }

        // Current instrument is always visible
        instruments[currentIndex].gameObject.SetActive(true);

        MoveInstrument(instruments[currentIndex],centerPosition,centerScale);

        // Only one collected instrument
        if (collectedCount == 1)
        {
            return;
        }

        // Two collected instruments
        if (collectedCount == 2)
        {
            instruments[leftIndex].gameObject.SetActive(true);

            MoveInstrument(instruments[leftIndex],leftPosition,sideScale);
            return;
        }

        // Three or more collected instruments
        instruments[leftIndex].gameObject.SetActive(true);
        instruments[rightIndex].gameObject.SetActive(true);
        MoveInstrument(instruments[leftIndex],leftPosition,sideScale);
        MoveInstrument(instruments[rightIndex],rightPosition,sideScale);
    }

    private void MoveInstrument(
        RectTransform instrument,
        Vector2 targetPosition,
        float targetScale)
    {
        instrument.anchoredPosition = Vector2.Lerp(
            instrument.anchoredPosition,
            targetPosition,
            moveSpeed * Time.deltaTime);

        instrument.localScale = Vector3.Lerp(
            instrument.localScale,
            Vector3.one * targetScale,
            moveSpeed * Time.deltaTime);
    }
}
