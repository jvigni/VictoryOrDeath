using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    private float targetFillAmount = 0f; // Initialize to 0
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        // Initialize health bar to start empty
        healthbarSprite.fillAmount = 0f; // Start empty
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        // Ensure we calculate fill amount correctly
        targetFillAmount = Mathf.Clamp01(currentHealth / maxHealth);
    }

    private void Update()
    {
        FaceCamera();
        // Smoothly transition to the target fill amount
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, targetFillAmount, reduceSpeed * Time.deltaTime);
    }

    private void FaceCamera()
    {
        if (mainCamera != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        }
    }
}
