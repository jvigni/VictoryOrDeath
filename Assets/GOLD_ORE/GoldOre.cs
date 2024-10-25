using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    public float craftingTimeInSeconds = 15f;
    public GameObject goldOre;
    public GameObject buildingResourceTowerPrefab;
    public GameObject humanResourceTowerPrefab;
    public GameObject plagueResourceTowerPrefab;
    private GameObject building;

    public void CraftResourceTower(Faction faction)
    {
        goldOre.SetActive(false);
        building = Instantiate(buildingResourceTowerPrefab, transform.position, Quaternion.identity);

        GameObject resourceTowerPrefab = faction == Faction.Human ? humanResourceTowerPrefab : plagueResourceTowerPrefab;
        StartCoroutine(CraftResourceTowerRoutine(resourceTowerPrefab));
    }

    private IEnumerator CraftResourceTowerRoutine(GameObject resourceTowerPrefab)
    {
        yield return new WaitForSeconds(craftingTimeInSeconds);
        Instantiate(resourceTowerPrefab, transform.position, Quaternion.identity);
        Destroy(building);
        Debug.Log($"{resourceTowerPrefab.name} built.");
    }
}
