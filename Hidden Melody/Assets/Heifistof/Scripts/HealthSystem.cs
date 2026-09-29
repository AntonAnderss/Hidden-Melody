using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/*
 * Author: Clara Lönnkrans
 * Script for controlling the health system for the player. Add script to player.
*/
public class HealthSystem : MonoBehaviour
{
    [Header("Health values")]
    [SerializeField] private bool alwaysTickingDown = true;
    [SerializeField] private float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private float loosePerSecond = 1f;
    private healthLevel currentHealthLevel;

    [Header ("HealthBar")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Volume globalVolume;
    private Vignette vignette;
    private FilmGrain filmgrain;
    private ColorAdjustments colorAdjustments;
    private ColorBlock color1, color2, color3;

    enum healthLevel
    {
        high, 
        medium, 
        low
    };
    public bool AlwaysTickingDown
    {
        set { alwaysTickingDown = value; }
    }
 

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        currentHealthLevel = healthLevel.high;
        healthBar.value = currentHealth;

        color1 = healthBar.colors;
        color2 = healthBar.colors;
        color3 = healthBar.colors;
        color1.disabledColor = Color.green;
        color2.disabledColor = Color.red;
        color3.disabledColor = Color.darkRed;
        healthBar.colors = color1;

        globalVolume.profile.TryGet(out vignette);
        vignette.intensity.value = 0;
        globalVolume.profile.TryGet(out filmgrain);
        globalVolume.profile.TryGet(out colorAdjustments);
        colorAdjustments.saturation.value = 100; 

    }

    void Update()
    {
        if (alwaysTickingDown)
        {
            LooseHealth(loosePerSecond * Time.deltaTime);
            
        }
        if (currentHealthLevel != healthLevel.high)
        {
            UpdateVignette();
            if (currentHealthLevel == healthLevel.low)
            {
                UpdateColor();
            }
        }

        if (currentHealth > maxHealth / 2 && currentHealthLevel != healthLevel.high)
        {
            HighHealth();

        }
        else if (currentHealth <= maxHealth / 2 && currentHealth > maxHealth / 5 && currentHealthLevel != healthLevel.medium)
        {
            MediumHealth();
        }
        else if (currentHealth <= maxHealth / 5 && currentHealthLevel != healthLevel.low)
        {
            LowHealth();
        }
        healthBar.value = currentHealth;
    }
    public void LooseHealth (float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {  currentHealth = 0; }
    }
    public void RegainAllHealth()
    {
        currentHealth = maxHealth;
    }
    public void RegainHealth(float healing)
    {
        currentHealth += healing;
        if(currentHealth > maxHealth) 
        { currentHealth = maxHealth; }
    }
    void MediumHealth()
    {
        healthBar.colors = color2;
        currentHealthLevel = healthLevel.medium;
    }
    void HighHealth()
    {
        healthBar.colors = color1;
        currentHealthLevel = healthLevel.high;
        vignette.intensity.value = 0;
    }
    void LowHealth()
    {
        healthBar.colors = color3;
        currentHealthLevel = healthLevel.low;
        EnemyManager.instance.SpawnSiren();
        alwaysTickingDown = false;
    }
    void UpdateVignette()
    {
        vignette.intensity.value = (0.5f - (currentHealth/ maxHealth) + 0.1f);
    }
    void UpdateColor()
    {
        colorAdjustments.saturation.value = -100 + (currentHealth * 5);
    }
    void KillPlayer()
    {

    }
}
