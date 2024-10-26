using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    public enum OreState { Normal, Building, Finished }

    [SerializeField] private HealthBar healthBar; // Updated to match the new HealthBar class
    public float craftingTimeInSeconds = 5f;
    public GameObject buildingResourceTowerPrefab;
    public GameObject humanResourceTowerPrefab;
    public GameObject plagueResourceTowerPrefab;

    private GameObject building;
    private OreState currentState = OreState.Normal;

    public void CraftResourceTower(Faction faction)
    {
        if (currentState != OreState.Normal) return; // Prevent crafting if not in normal state

        currentState = OreState.Building;

        // Select appropriate tower prefab based on faction
        GameObject resourceTowerPrefab = faction == Faction.Human ? humanResourceTowerPrefab : plagueResourceTowerPrefab;

        // Instantiate placeholder building
        building = Instantiate(buildingResourceTowerPrefab, transform.position, Quaternion.identity);

        // Start crafting coroutine
        StartCoroutine(CraftResourceTowerRoutine(resourceTowerPrefab));
    }

    private IEnumerator CraftResourceTowerRoutine(GameObject resourceTowerPrefab)
    {
        float elapsedTime = 0f;

        // Initialize health bar to start empty
        healthBar.IncreaseHealth(float.NegativeInfinity); // Reset the health bar to zero

        // Define the update frequency to speed up health bar updates
        float updateInterval = 0.1f; // Update health bar every 0.1 seconds
        float normalizedHealth;

        while (elapsedTime < craftingTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;

            // Check if it's time to update the health bar
            if (elapsedTime >= updateInterval)
            {
                // Calculate and set the normalized health based on elapsed time
                normalizedHealth = Mathf.Clamp01(elapsedTime / craftingTimeInSeconds);
                healthBar.IncreaseHealth(normalizedHealth); // Use IncreaseHealth instead
            }

            yield return null; // Wait until the next frame
        }

        // Finalize tower construction
        Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);
        Destroy(building);

        // Set health to full after construction
        healthBar.IncreaseHealth(1f); // Use IncreaseHealth instead

        currentState = OreState.Finished; // Update state to finished
        Debug.Log($"{resourceTowerPrefab.name} built.");
    }

    // Optional: A method to reset the GoldOre state back to Normal
    public void ResetGoldOre()
    {
        currentState = OreState.Normal;
        healthBar.IncreaseHealth(float.NegativeInfinity); // Reset health bar to zero
    }
}
