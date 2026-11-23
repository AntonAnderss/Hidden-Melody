using UnityEngine;

public class HealController : MonoBehaviour
{

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private float healAmount = 40f;
    [SerializeField] private float cooldown = 30f;
    private float nextHealTime = 0f;
    private void Awake()
    {
        if (healthSystem == null)
        {
            healthSystem = GetComponent<HealthSystem>();
        }
    }
    public void TryHeal()
    {
        // Ability is still on cooldown
        if (Time.time < nextHealTime)
        {
            float timeLeft = nextHealTime - Time.time;
            return;
        }

        // Heal player
        healthSystem.RegainHealth(healAmount);

        // Start 30 second cooldown
        nextHealTime = Time.time + cooldown;

        Debug.Log("Player healed!");
    }
}
