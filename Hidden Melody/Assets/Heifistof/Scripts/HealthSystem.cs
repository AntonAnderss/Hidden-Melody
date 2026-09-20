using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private bool alwaysTickingDown = true;
    [SerializeField] private float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private float loosePerSecond = 1f;
    private healthLevel currentHealthLevel;

    //Healthbar
    [SerializeField] private Slider healthBar;
    private ColorBlock color1, color2, color3;
    [SerializeField] private Image vignette;
    private Color vignetteColor;

    //Siren
    private bool sirenSpawned;
    [SerializeField] GameObject sirenPrefab;
    enum healthLevel
    {
        high, 
        medium, 
        low
    };
 

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

        vignetteColor = vignette.color;
        vignetteColor.a = 0;
    }

    void Update()
    {
        if (alwaysTickingDown)
        {
            LooseHealth(loosePerSecond * Time.deltaTime);
            if(currentHealth > maxHealth / 2 && currentHealthLevel != healthLevel.high)
            {
                AboveHalfHealth();
            }
            else if (currentHealth <= maxHealth/2 && currentHealth > maxHealth/5 && currentHealthLevel != healthLevel.medium)
            {
                UnderHalfHealth();
            }
            else if (currentHealth <= maxHealth/5 && currentHealthLevel != healthLevel.low)
            {
                ZeroHealth();
            }
        }
        healthBar.value = currentHealth;
        vignetteColor.a = 1f - (currentHealth/maxHealth);
        vignette.color = vignetteColor;
    }
    void LooseHealth (float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {  currentHealth = 0; }
    }
    void RegainHealth(float healing)
    {
        currentHealth += healing;
        if(currentHealth > maxHealth) 
        { currentHealth = maxHealth; }
    }
    void UnderHalfHealth()
    {
        healthBar.colors = color2;
        currentHealthLevel = healthLevel.medium;
    }
    void AboveHalfHealth()
    {
        healthBar.colors = color1;
        currentHealthLevel = healthLevel.high;
    }
    void ZeroHealth()
    {
        healthBar.colors = color3;
        currentHealthLevel = healthLevel.low;
        SpawnSiren();
    }
    void SpawnSiren()
    {
        Debug.Log("Siren spawned");
        if (!sirenSpawned)
        {
            sirenSpawned = true;
            Instantiate(sirenPrefab);
        }
    }
}
