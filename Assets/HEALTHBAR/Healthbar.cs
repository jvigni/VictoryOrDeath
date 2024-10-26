using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private TextMeshProUGUI healthText;
    public float MaxHealth = 100f;

    private float currentHealth;

    private void Awake()
    {
        InitializeHealth();
    }

    public void InitializeHealth()
    {
        currentHealth = MaxHealth;
        UpdateHealthUI();
    }

    public void AdjustHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString("F0");
        healthBarFillImage.fillAmount = currentHealth / MaxHealth;
    }
}
