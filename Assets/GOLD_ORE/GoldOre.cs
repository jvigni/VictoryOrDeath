using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    public enum OreState { Normal, Building }
    bool isBuilding;

    [SerializeField] private HealthBar healthBar; // Updated to match the new HealthBar class
    public float craftingTimeInSeconds = 5f;
    public GameObject humanResourceTowerPrefab;
    public GameObject plagueResourceTowerPrefab;

    private GameObject building;
    private OreState currentState = OreState.Normal;

    public void CraftResourceTower(Faction faction)
    {
        if (isBuilding) 
            return;

        isBuilding = true;

        // Select appropriate tower prefab based on faction
        GameObject resourceTowerPrefab = faction == Faction.Human ? humanResourceTowerPrefab : plagueResourceTowerPrefab;

        // Instantiate placeholder building
        //building = Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);

        // Start crafting coroutine
        StartCoroutine(CraftResourceTowerRoutine(resourceTowerPrefab));
    }

    private IEnumerator CraftResourceTowerRoutine(GameObject resourceTowerPrefab)
    {
        float elapsedTime = 0f;

        // Initialize health bar to start empty
        healthBar.Swap();
        healthBar.IncreaseHealth(float.NegativeInfinity); // Reset the health bar to zero

        while (elapsedTime < craftingTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;

            // Calculate and set the normalized health based on elapsed time
            float normalizedHealth = Mathf.Clamp01(elapsedTime / craftingTimeInSeconds);
            healthBar.IncreaseHealth(normalizedHealth);

            yield return null; // Wait until the next frame
        }

        // Finalize tower construction
        Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);
        Debug.Log($"{resourceTowerPrefab.name} built.");
        Destroy(gameObject);
    }

    // Optional: A method to reset the GoldOre state back to Normal
    public void ResetGoldOre()
    {
        currentState = OreState.Normal;
        healthBar.IncreaseHealth(float.NegativeInfinity); // Reset health bar to zero
    }
}
