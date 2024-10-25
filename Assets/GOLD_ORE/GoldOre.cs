using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    public enum OreState { Normal, Building, Finished }

    [SerializeField] private Healthbar healthbar;
    public float craftingTimeInSeconds = 5f;
    public GameObject goldOre;
    public GameObject buildingResourceTowerPrefab;
    public GameObject humanResourceTowerPrefab;
    public GameObject plagueResourceTowerPrefab;

    private GameObject building;
    private OreState currentState = OreState.Normal;

    public void CraftResourceTower(Faction faction)
    {
        if (currentState != OreState.Normal) return; // Prevent crafting if not in normal state

        currentState = OreState.Building;
        goldOre.SetActive(false);

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
        healthbar.SetHealth(0f, craftingTimeInSeconds);

        while (elapsedTime < craftingTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;

            // Calculate and set the normalized health
            float normalizedHealth = elapsedTime / craftingTimeInSeconds;
            healthbar.SetHealth(normalizedHealth, craftingTimeInSeconds);

            yield return null; // Wait until the next frame
        }

        // Finalize tower construction
        Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);
        Destroy(building);

        // Set health to full after construction
        healthbar.SetHealth(1f, craftingTimeInSeconds);

        currentState = OreState.Finished; // Update state to finished
        Debug.Log($"{resourceTowerPrefab.name} built.");
    }

    // Optional: A method to reset the GoldOre state back to Normal
    public void ResetGoldOre()
    {
        currentState = OreState.Normal;
        goldOre.SetActive(true);
        healthbar.SetHealth(0f, craftingTimeInSeconds); // Reset healthbar
    }
}
