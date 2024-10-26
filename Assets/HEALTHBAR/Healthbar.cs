using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private float maxHealth = 100f; // Maximum health defined in the inspector
    [SerializeField] private float decreaseSpeed = 2f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        healthBarImage.fillAmount = 1f; // Fill the bar to full
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount; // Only increase current health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure current health does not exceed max
        healthBarImage.fillAmount = currentHealth / maxHealth; // Update health bar
    }

    private void Update()
    {
        // Smoothly transition to the target fill amount (if needed)
        //healthBarImage.fillAmount = Mathf.MoveTowards(healthBarImage.fillAmount, currentHealth / maxHealth, decreaseSpeed * Time.deltaTime);
    }
}
