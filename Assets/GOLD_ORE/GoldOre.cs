using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    [SerializeField] private HealthBar buildingBar;
    [SerializeField] private float craftingTimeInSeconds = 5f;
    [SerializeField] private ResourceTower humanRTPrefab;
    [SerializeField] private ResourceTower plagueRTPrefab;
    private bool isBuilding;
    
    private void Update()
    {
        // Ensure the health bar always faces the main camera
        Vector3 directionToCamera = Camera.main.transform.position - buildingBar.transform.position;
        directionToCamera.y = 0; // Optional: Ignore vertical rotation if you want it to stay upright

        // Create a rotation that looks at the camera
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);

        // Apply the rotation to the health bar
        buildingBar.transform.rotation = lookRotation;
    }


    public void CraftResourceTower(Faction faction)
    {
        if (isBuilding) return;

        isBuilding = true;
        buildingBar.gameObject.SetActive(true);
        buildingBar.AdjustHealth(-buildingBar.MaxHealth); // Initialize health to 0 for building

        ResourceTower resourceTowerPrefab = faction == Faction.Human ? humanRTPrefab : plagueRTPrefab;
        StartCoroutine(CraftResourceTowerRoutine(resourceTowerPrefab));
    }
    private IEnumerator CraftResourceTowerRoutine(ResourceTower resourceTowerPrefab)
    {
        float targetHealth = buildingBar.MaxHealth;
        float elapsedTime = 0f;

        // Gradually increase health over the crafting time
        while (elapsedTime < craftingTimeInSeconds)
        {
            float progress = elapsedTime / craftingTimeInSeconds;
            float currentHealth = progress * targetHealth;
            buildingBar.SetHealth(currentHealth); // Adjust health to match current progress

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure health reaches maximum at the end
        buildingBar.SetHealth(targetHealth);

        // Finalize tower construction
        Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);
        Debug.Log($"{resourceTowerPrefab.name} built.");
        Destroy(gameObject);
    }
}
