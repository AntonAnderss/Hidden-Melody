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

    [SerializeField] Camera cameraShake;
    [SerializeField] private Slider slider;
    private bool goingUp = true;
    [SerializeField] private InputActionReference interactKey;
    [SerializeField] private Volume greenCorrect;
    [SerializeField] private List<Image> images;
    [SerializeField] private RectTransform sliderRect;
    [SerializeField] private Canvas miniGameCanvas;
    [SerializeField] private HealthSystem healthSystem;
    //[SerializeField] private GameObject cameraActive;
    public bool minigameCompleted { get; private set; }
    private HitCombination currentCombination;
    private bool minigameActive = false;


    public void StartMiniGame(HitCombination combination)
    {
        miniGameCanvas.gameObject.SetActive(true);

        currentCombination = combination;
        minigameCompleted = false;
        minigameActive = true;
        ResetCombination();
        //cameraActive.GameObject().SetActive(false);
    }
    void OnEnable()
    {
        interactKey.action.Enable();
    }

    void OnDisable()
    {
        interactKey.action.Disable();
    }

    void Update()
    {
        if(!minigameActive)
        {
            return;
        }

        SliderMovement();


        if (interactKey.action.WasPressedThisFrame())
        {
            Debug.Log("Interact Funkar");
            CheckHit();
        }
    }

    public void SliderMovement()
    {

        if(goingUp)
        {
            slider.value += 000.6f * Time.deltaTime; // Makes slider go right
            if(slider.value>=1)
            {
                goingUp = false;
            }
        }
        else if (!goingUp)
        {
            slider.value -= 000.6f * Time.deltaTime; // Makes slider go Left
            if (slider.value<=0)
            {
                goingUp = true;
            }
        }
    }
    public void CheckHit()
    {

        if(currentCombination == null)
        {
            Debug.Log("no Minigmae Combination is active");
            return;
        }
        Debug.Log("E pressed");
        HitCombination combination  = currentCombination;

        // Loops through all zones checking if interact key is pressed inside HitZone
        for (int i = 0; i < combination.zones.Count; i++) 
        {
            HitZone zone = combination.zones[i];

            // Skips zones that are already hit
            if (zone.completed) 
                continue;

            if (slider.value >= zone.minValue &&
                slider.value <= zone.maxValue)
            {
                Debug.Log("Correct");

                zone.completed = true;

                // Removes black staple when interct key is pressed inside HitZone
                images[i].gameObject.SetActive(false);  
                
                StartCoroutine(GreenVignette());

                CheckIfCombinationComplete();

                return;
            }
        }
        healthSystem.LooseHealth(5);
        Debug.Log("Miss");
        StartCoroutine(CameraShake());
    }

    public IEnumerator CameraShake()
    {

        // Saves camera's original rotation so it can be restored
        Quaternion originalRotation = cameraShake.transform.localRotation;

        // How long the shake lasts, and how strong rotation is
        float duration = 0.15f;
        float strength = 3f;
        float timer = 0f;

        // Keeps shaking until duration runs out
        while (timer < duration)
        {
            // Generate a random rotation
            float zRotation = Random.Range(-strength, strength);

            // Apply the random rotation 
            cameraShake.transform.localRotation = originalRotation * Quaternion.Euler(0, 0, zRotation);

            timer += Time.deltaTime;
            yield return null;
        }

        // restore the camera to original rotation
        cameraShake.transform.localRotation = originalRotation;
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

        for (int i = 0; i < images.Count; i++)
        {
            HitZone zone = combination.zones[i];

            // Center of the hitarea
            float center = (zone.minValue + zone.maxValue) / 2f; 

            // Makes 0-1 a position on the slider
            float xPosition = Mathf.Lerp( 
                sliderRect.rect.xMin,
                sliderRect.rect.xMax,
                center
            );

            RectTransform imageRect = images[i].rectTransform;

            // Moves the hit marker to correct position while keeping Y value the same
            imageRect.anchoredPosition = new Vector2( 
                xPosition,
                imageRect.anchoredPosition.y
            );
        }
    }
    private void CheckIfCombinationComplete()
    {
        // Loops through the zones, if not completed returns.
        foreach (HitZone zone in currentCombination.zones)
        {
            if (!zone.completed)
            {
                return;
            }
        }

        Debug.Log("Combo Complete: " + currentCombination.combinationName);
        FinishMiniGame();


    }

    private void FinishMiniGame()
    {
        minigameCompleted = true;
        minigameActive = false;
        miniGameCanvas.gameObject.SetActive(false);
        greenCorrect.weight = 0f;
        currentCombination = null;
        //cameraActive.GameObject().SetActive(true);

        Debug.Log("Minigame Complete");
    }

    private void ResetCombination()
    {

        Debug.Log("Reset");

        for (int i = 0; i < currentCombination.zones.Count; i++)
        {
            currentCombination.zones[i].completed = false;

            images[i].gameObject.SetActive(true);
        }

        UpdateImages();
    }

}
