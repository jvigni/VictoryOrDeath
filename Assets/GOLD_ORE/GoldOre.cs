using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    [SerializeField] HealthBar healthBar; // Updated to match the new HealthBar class
    [SerializeField] GameObject buildingBrazier;
    [SerializeField] float craftingTimeInSeconds = 5f;
    [SerializeField] GameObject baseGoldOre;
    [SerializeField] GameObject humanRTPrefab;
    [SerializeField] GameObject plagueRTPrefab;

    bool isBuilding;
    GameObject building;

    public void CraftResourceTower(Faction faction)
    {
        if (isBuilding) 
            return;

        isBuilding = true;
        buildingBrazier.gameObject.SetActive(true);
        healthBar.Show(true);

        GameObject resourceTowerPrefab = faction == Faction.Human ? humanRTPrefab : plagueRTPrefab;
        StartCoroutine(CraftResourceTowerRoutine(resourceTowerPrefab));
    }

    private IEnumerator CraftResourceTowerRoutine(GameObject resourceTowerPrefab)
    {
        float elapsedTime = 0f;

        // Initialize health bar to start empty
        healthBar.gameObject.SetActive(true);
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
        //Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);

        baseGoldOre.SetActive(false);
        resourceTowerPrefab.SetActive(true);
        Debug.Log($"{resourceTowerPrefab.name} built.");
        //Destroy(gameObject);
    }

    // Optional: A method to reset the GoldOre state back to Normal
    public void ResetGoldOre()
    { 
        healthBar.IncreaseHealth(float.NegativeInfinity);
        isBuilding = false;
        buildingBrazier.gameObject.SetActive(false);
        baseGoldOre.SetActive(true);
        humanRTPrefab.SetActive(false);
        plagueRTPrefab.SetActive(false);
    }
}
