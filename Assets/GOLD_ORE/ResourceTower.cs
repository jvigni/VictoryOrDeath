using UnityEngine;
using TMPro;
using System.Collections;

public class ResourceTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI resourceText; // Reference to the TextMeshProUGUI component
    [SerializeField] private int resourcesPerCycle = 10; // Amount of resources to give each cycle
    [SerializeField] private float cycleDuration = 6f; // Duration between resource distributions

    private int totalResources; // Total resources collected

    private void Start()
    {
        // Start the coroutine to give resources
        StartCoroutine(GiveResources());
    }

    private IEnumerator GiveResources()
    {
        while (true) // Infinite loop to keep giving resources
        {
            // Simulate giving resources to the team
            GiveResourcesToTeam(resourcesPerCycle);

            // Update the displayed resource count
            UpdateResourceDisplay();

            // Wait for the specified duration before the next cycle
            yield return new WaitForSeconds(cycleDuration);
        }
    }

    private void GiveResourcesToTeam(int amount)
    {
        // Add resources to the total
        totalResources += amount;

        // Here you can implement the logic to actually distribute the resources to your team
        // For example, notifying team members or updating their resource counts.
    }

    private void UpdateResourceDisplay()
    {
        // Update the resource text display
        resourceText.text = "Resources: " + totalResources;
    }
}
