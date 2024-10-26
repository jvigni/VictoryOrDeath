using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject buildingBrazier;
    [SerializeField] private float craftingTimeInSeconds = 5f;
    [SerializeField] private int healthSteps = 20;
    [SerializeField] private GameObject baseGoldOre;
    [SerializeField] private GameObject humanRTPrefab;
    [SerializeField] private GameObject plagueRTPrefab;

    private bool isBuilding;

    public void CraftResourceTower(Faction faction)
    {
        if (isBuilding) return;

        isBuilding = true;
        buildingBrazier.SetActive(true); 
        healthBar.gameObject.SetActive(true);
        healthBar.AdjustHealth(-healthBar.MaxHealth); // Initialize health to 0 for building


        GameObject resourceTowerPrefab = faction == Faction.Human ? humanRTPrefab : plagueRTPrefab;
        StartCoroutine(CraftResourceTowerRoutine(resourceTowerPrefab));
    }

    private IEnumerator CraftResourceTowerRoutine(GameObject resourceTowerPrefab)
    {
        float healthIncrement = healthBar.MaxHealth / healthSteps;
        float interval = craftingTimeInSeconds / healthSteps;

        for (int i = 0; i < healthSteps; i++)
        {
            yield return new WaitForSeconds(interval);
            healthBar.AdjustHealth(healthIncrement);
        }

        // Finalize tower construction
        baseGoldOre.SetActive(false);
        resourceTowerPrefab.SetActive(true);
        Debug.Log($"{resourceTowerPrefab.name} built.");
    }

    public void ResetGoldOre()
    {
        healthBar.InitializeHealth(); // Reset health to full if required
        isBuilding = false;
        buildingBrazier.SetActive(false);
        baseGoldOre.SetActive(true);
        humanRTPrefab.SetActive(false);
        plagueRTPrefab.SetActive(false);
    }
}
