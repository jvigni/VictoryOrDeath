using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float craftingTimeInSeconds = 5f;
    [SerializeField] private GameObject dust;
    [SerializeField] private GameObject humanRTPrefab;
    [SerializeField] private GameObject plagueRTPrefab;

    bool isBuilding;
    bool conquered;

    public void CraftResourceTower(Faction faction)
    {
        if (isBuilding) return;

        isBuilding = true;
        conquered = true;
        dust.SetActive(true);
        healthBar.gameObject.SetActive(true);
        healthBar.AdjustHealth(-healthBar.MaxHealth); // Initialize health to 0 for building

        GameObject resourceTowerPrefab = faction == Faction.Human ? humanRTPrefab : plagueRTPrefab;
        StartCoroutine(CraftResourceTowerRoutine(resourceTowerPrefab));
    }

    private IEnumerator CraftResourceTowerRoutine(GameObject resourceTowerPrefab)
    {
        float targetHealth = healthBar.MaxHealth;
        float elapsedTime = 0f;
        float healthIncrement = targetHealth / (craftingTimeInSeconds * 100); // Set a smaller increment

        // Gradually increase health to targetHealth over crafting time
        while (elapsedTime < craftingTimeInSeconds)
        {
            healthBar.AdjustHealth(healthIncrement); // Add a small increment
            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure health reaches maximum at the end
        healthBar.AdjustHealth(targetHealth); // Adjust to max health if needed

        // Finalize tower construction
        healthBar.gameObject.SetActive(false);
        resourceTowerPrefab.SetActive(true);
        gameObject.SetActive(false);
        Debug.Log($"{resourceTowerPrefab.name} built.");
    }

    /* DEPRECATED
    public void ResetGoldOre()
    {
        healthBar.InitializeHealth(); // Reset health to full if required
        isBuilding = false;
        conquered = false;
        dust.SetActive(false);
        humanRTPrefab.SetActive(false);
        plagueRTPrefab.SetActive(false);
    }*/
}
