using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    private float target = 1f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found. Please assign a camera.");
        }
    }

    /// <summary>
    /// Updates the health bar's target fill amount based on max and current health.
    /// </summary>
    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        target = Mathf.Clamp01(currentHealth / maxHealth);
    }

    void Update()
    {
        if (cam != null)
        {
            // Face the health bar towards the camera
            transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        }

        // Smoothly move the fill amount towards the target
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
