using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    public float craftingTimeInSeconds = 15f;
    public GameObject goldOre;
    public GameObject buildingResourceTowerPrefab;
    public GameObject humanResourceTowerPrefab;
    public GameObject plagueResourceTowerPrefab;
    private GameObject building;

    public void CraftResourceTower(Faction faction)
    {
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
        healthbar.UpdateHealthbar(craftingTimeInSeconds, 0f); // Initialize health bar to empty

        while (elapsedTime < craftingTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;
            healthbar.UpdateHealthbar(craftingTimeInSeconds, elapsedTime); // Update fill based on progress
            yield return null; // Wait until the next frame
        }

        // Instantiate the final resource tower and remove placeholder
        Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);
        Destroy(building);
        Debug.Log($"{resourceTowerPrefab.name} built.");
    }
}
