using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;


public class MiniGame : MonoBehaviour
{

    [SerializeField] PlatformerCamera platformerCamera;
    [SerializeField] private Slider slider;
    [SerializeField] private InputActionReference noteOneInput;
    [SerializeField] private InputActionReference noteTwoInput;
    [SerializeField] private InputActionReference noteThreeInput;
    [SerializeField] private Volume greenCorrect;
    [SerializeField] private List<Image> images;
    [SerializeField] private float hitZoneRadius = 200f;
    [SerializeField] private Canvas miniGameCanvas;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image fillImage;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject abilityControllers;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private StateMachine stateMachine;
    private Color backgroundColor;
    private Color fillColor;
    private bool colorsSwapped = false;
    private bool reachedEnd = false;
    private int currentZoneIndex = 0;
    [SerializeField] private HealthSystem healthSystem;

    public bool minigameCompleted { get; private set; }
    private HitCombination currentCombination;
    private bool minigameActive = false;

    private void Start()
    {
        backgroundColor = backgroundImage.color;
        fillColor = fillImage.color;
    }
    public void StartMiniGame(HitCombination combination)
    {

        miniGameCanvas.gameObject.SetActive(true);

        currentCombination = combination;
        minigameCompleted = false;
        minigameActive = true;
        reachedEnd = false;
        slider.value = 0f;
        // Force idle animation
        stateMachine.ForceIdle();
        stateMachine.enabled = false;
        //Disable player controlls
        playerMovement.enabled = false;
        abilityControllers.SetActive(false);
        // Then stop the Rigidbody
        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;

        ResetCombination();
    }
    void OnEnable()
    {
        noteOneInput.action.Enable();
        noteTwoInput.action.Enable();
        noteThreeInput.action.Enable();
    }

    void OnDisable()
    {
        noteOneInput.action.Disable();
        noteTwoInput.action.Disable();
        noteThreeInput.action.Disable();
    }

    void Update()
    {

        if(!minigameActive)
        {
            return;
        }
        SliderMovement();




        CheckHit();
        
    }

    public void SliderMovement()
    {
        slider.value += 0.3f * Time.deltaTime;

        if (slider.value >= 1f && !reachedEnd)
        {
            reachedEnd = true;

            colorsSwapped = !colorsSwapped;

            if (colorsSwapped)
            {
                backgroundImage.color = fillColor;
                fillImage.color = backgroundColor;
            }
            else
            {
                backgroundImage.color = backgroundColor;
                fillImage.color = fillColor;
            }

            slider.value = 0f;
        }

        if (slider.value > 0.05f)
        {
            reachedEnd = false;
        }
    }
    public void CheckHit()
    {
        if (currentCombination == null)
            return;

        Note? pressedNote = null;

        if (noteOneInput.action.WasPressedThisFrame())
            pressedNote = Note.One;
        else if (noteTwoInput.action.WasPressedThisFrame())
            pressedNote = Note.Two;
        else if (noteThreeInput.action.WasPressedThisFrame())
            pressedNote = Note.Three;

        if (pressedNote == null)
            return;

        HitZone zone = currentCombination.zones[currentZoneIndex];

        bool insideZone =
            slider.value >= zone.minValue &&
            slider.value <= zone.maxValue;

        if (insideZone && pressedNote.Value == zone.requiredNote)
        {
            zone.completed = true;

            Image correctImage = GetImageForNote(zone.requiredNote);
            correctImage.gameObject.SetActive(false);
            healthSystem.RegainHealth(10f);

            StartCoroutine(GreenVignette());

            currentZoneIndex++;

            if (currentZoneIndex >= currentCombination.zones.Count)
            {
                FinishMiniGame();
            }

            return;
        }

        platformerCamera.Shake(0.15f, 3f);
        healthSystem.LooseHealth(5f);
    }
    private Image GetImageForNote(Note note)
    {
        if (note == Note.One)
            return images[0];

        if (note == Note.Two)
            return images[1];

        if (note == Note.Three)
            return images[2];

        return null;
    }
    public IEnumerator GreenVignette()
    {
        greenCorrect.weight = 1f;
        yield return new WaitForSeconds(0.15f);
        greenCorrect.weight = 0f;
    }


    private void UpdateImages()
    {
        HitCombination combination = currentCombination;

        for (int i = 0; i < combination.zones.Count; i++)
        {
            HitZone zone = combination.zones[i];

            float center = (zone.minValue + zone.maxValue) / 2f;

            float angle = -90f - center * 360f;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 position = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * hitZoneRadius;

            // Hämta rätt bild beroende på vilken Note zonen använder
            Image correctImage = GetImageForNote(zone.requiredNote);

            RectTransform imageRect = correctImage.rectTransform;

            imageRect.anchoredPosition = position;

            imageRect.localRotation =
                Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }

    private void FinishMiniGame()
    {
        minigameCompleted = true;
        minigameActive = false;

        //Enable player controlls
        playerMovement.ResetMovementInput();
        playerMovement.enabled = true;
        abilityControllers.SetActive(true);

        //Enable animation
        stateMachine.enabled = true;

        miniGameCanvas.gameObject.SetActive(false);
        greenCorrect.weight = 0f;
        currentCombination = null;
    }

    private void ResetCombination()
    {
        currentZoneIndex = 0;

        foreach (HitZone zone in currentCombination.zones)
        {
            zone.completed = false;
        }

        foreach (Image image in images)
        {
            image.gameObject.SetActive(true);
        }

        UpdateImages();
    }

}
