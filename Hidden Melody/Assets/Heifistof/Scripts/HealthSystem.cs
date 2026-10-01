using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/*
 * Author: Clara Lönnkrans
 * Script for the health system. Controlls the healthbar and spawns healtsystem boss. 
 * 
 * Use: Add script to player.
 * Add canvas containing healthbar to healthbar
 * Add the healthbarfill image to fillImage
 * Add global volume to global volume
*/
public class HealthSystem : MonoBehaviour
{
    [Header("Health values")]
    [SerializeField] private bool alwaysTickingDown = true;
    [SerializeField] private float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private float loosePerSecond = 1f;
    private healthLevel currentHealthLevel;
    [SerializeField] private PLayerRespawn checkpoints;

    [Header("HealthBar")]
    [SerializeField] public GameObject healthBar;
    [SerializeField] public Image fillImage;

    [SerializeField] private Volume globalVolume;
    private Vignette vignette;
    private FilmGrain filmgrain;
    private ColorAdjustments colorAdjustments;
    private Color colorHPHigh, colorHPMedium, colorHPLow;

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
        currentHealthLevel = healthLevel.high;

        colorHPHigh = Color.yellowGreen;
        colorHPMedium = Color.orange;
        colorHPLow = Color.darkRed;
        fillImage.color = colorHPHigh;

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
        fillImage.fillAmount = currentHealth / maxHealth;
        if(currentHealth <=0)
        {
            KillPlayer();
        }
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
        fillImage.color = colorHPMedium;
        currentHealthLevel = healthLevel.medium;
    }
    void HighHealth()
    {
        fillImage.color = colorHPHigh;
        currentHealthLevel = healthLevel.high;
        vignette.intensity.value = 0;
    }
    void LowHealth()
    {
        fillImage.color = colorHPLow;
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
        //Need to fix so its correct if max is changed
        colorAdjustments.saturation.value = -100 + (currentHealth * 5);
    }
    void KillPlayer()
    {
        checkpoints.Respawn();
        RegainAllHealth();
        EnemyManager.instance.SpawnSiren();
    }
}
