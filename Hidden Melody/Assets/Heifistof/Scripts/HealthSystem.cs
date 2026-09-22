using UnityEngine;
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
    private ColorBlock color1, color2, color3;

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
    }

    void Update()
    {
        if (alwaysTickingDown)
        {
            LooseHealth(loosePerSecond * Time.deltaTime);
            if(currentHealth > maxHealth / 2 && currentHealthLevel != healthLevel.high)
            {
                HighHealth();
            }
            else if (currentHealth <= maxHealth/2 && currentHealth > maxHealth/5 && currentHealthLevel != healthLevel.medium)
            {
                MediumHealth();
            }
            else if (currentHealth <= maxHealth/5 && currentHealthLevel != healthLevel.low)
            {
                LowHealth();
            }
        }
        healthBar.value = currentHealth;
    }
    public void LooseHealth (float damage)
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
    void MediumHealth()
    {
        healthBar.colors = color2;
        currentHealthLevel = healthLevel.medium;
    }
    void HighHealth()
    {
        healthBar.colors = color1;
        currentHealthLevel = healthLevel.high;
    }
    void LowHealth()
    {
        healthBar.colors = color3;
        currentHealthLevel = healthLevel.low;
        EnemyManager.instance.SpawnSiren();
    }
}
