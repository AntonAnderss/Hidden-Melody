using UnityEngine;

public class InstrumentSwitcher : MonoBehaviour
{
    [SerializeField] private RectTransform[] instruments;

    [Header("Positions")]
    [SerializeField] private Vector2 leftPosition;
    [SerializeField] private Vector2 centerPosition;
    [SerializeField] private Vector2 rightPosition;

    [Header("Scale")]
    [SerializeField] private float centerScale = 0.8f;
    [SerializeField] private float sideScale = 0.5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;

    private int currentInstrument = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInstrument++;

            if (currentInstrument >= instruments.Length)
                currentInstrument = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentInstrument--;

            if (currentInstrument < 0)
                currentInstrument = instruments.Length - 1;
        }

        UpdateInstrumentPositions();
    }

    private void UpdateInstrumentPositions()
    {
        int leftIndex = (currentInstrument - 1 + instruments.Length) % instruments.Length;

        int rightIndex = (currentInstrument + 1) % instruments.Length;

        MoveInstrument(instruments[currentInstrument], centerPosition, centerScale);
        MoveInstrument(instruments[leftIndex], leftPosition, sideScale);
        MoveInstrument(instruments[rightIndex], rightPosition, sideScale);
    }

    private void MoveInstrument(
        RectTransform instrument,
        Vector2 targetPosition,
        float targetScale)
    {
        instrument.anchoredPosition = Vector2.Lerp(
            instrument.anchoredPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        instrument.localScale = Vector3.Lerp(
            instrument.localScale,
            Vector3.one * targetScale,
            moveSpeed * Time.deltaTime
        );
    }
}
