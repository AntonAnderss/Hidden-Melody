using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;


public class MiniGame : MonoBehaviour
{

    [SerializeField] Camera cameraShake;
    [SerializeField] private Slider slider;
    private bool goingUp = true;
    [SerializeField] private InputActionReference interactKey;
    [SerializeField] private Volume greenCorrect;
    [SerializeField] private List<HitCombination> combinations;
    [SerializeField] private List<Image> images;
    [SerializeField] private RectTransform sliderRect;


    private int currentCombination = 0;
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
            slider.value += 000.6f * Time.deltaTime;
            if(slider.value>=1)
            {
                goingUp = false;
            }
        }
        else if (!goingUp)
        {
            slider.value -= 000.6f * Time.deltaTime;
            if(slider.value<=0)
            {
                goingUp = true;
            }
        }
    }
    public void CheckHit()
    {
        Debug.Log("E pressed");
        HitCombination combination  = combinations[currentCombination];

        for (int i = 0; i < combination.zones.Count; i++)
        {
            HitZone zone = combination.zones[i];

            // Hoppa över zones som redan är träffade
            if (zone.completed)
                continue;

            if (slider.value >= zone.minValue &&
                slider.value <= zone.maxValue)
            {
                Debug.Log("Correct");

                zone.completed = true;

                // Ta bort motsvarande svarta stapel
                images[i].gameObject.SetActive(false);
                
                StartCoroutine(GreenVignette());

                CheckIfCombinationComplete();

                return;
            }
        }

        Debug.Log("Miss");
        StartCoroutine(CameraShake());

        //0.73 - 0.83 
        //0.44 - 0.54
        //0.18 - 0.28
    }

    public IEnumerator CameraShake()
    {
        Quaternion originalRotation = cameraShake.transform.localRotation;

        float duration = 0.15f;
        float strength = 3f;
        float timer = 0f;

        while (timer < duration)
        {
            float zRotation = Random.Range(-strength, strength);

            cameraShake.transform.localRotation = originalRotation * Quaternion.Euler(0, 0, zRotation);

            timer += Time.deltaTime;
            yield return null;
        }

        cameraShake.transform.localRotation = originalRotation;
    }
    public IEnumerator GreenVignette()
    {
        greenCorrect.weight = 1f;
        yield return new WaitForSeconds(0.15f);
        greenCorrect.weight = 0f;
    }

    [System.Serializable]
    public class HitZone
    {
        public float minValue;
        public float maxValue;

        public bool completed;
    }

    [System.Serializable]
    public class HitCombination
    {
        public List<HitZone> zones;
    }

    private void UpdateImages()
    {
        HitCombination combination = combinations[currentCombination];

        for (int i = 0; i < images.Count; i++)
        {
            HitZone zone = combination.zones[i];

            // Mitten av träffområdet
            float center = (zone.minValue + zone.maxValue) / 2f;

            // Gör om 0-1 till en position längs slidern
            float xPosition = Mathf.Lerp(
                sliderRect.rect.xMin,
                sliderRect.rect.xMax,
                center
            );

            RectTransform imageRect = images[i].rectTransform;

            imageRect.anchoredPosition = new Vector2(
                xPosition,
                imageRect.anchoredPosition.y
            );
        }
    }
    private void CheckIfCombinationComplete()
    {
        HitCombination combination = combinations[currentCombination];

        foreach (HitZone zone in combination.zones)
        {
            if (!zone.completed)
            {
                return;
            }
        }

        Debug.Log("Alla 3 träffade");

        NextCombination();
    }
    private void NextCombination()
    {
        currentCombination++;

        if (currentCombination >= combinations.Count)
        {
            currentCombination = 0;
        }

        ResetCombination();
    }


    private void ResetCombination()
    {

        Debug.Log("Reset");
        HitCombination combination = combinations[currentCombination];

        for (int i = 0; i < combination.zones.Count; i++)
        {
            combination.zones[i].completed = false;

            images[i].gameObject.SetActive(true);
        }

        UpdateImages();
    }

}
