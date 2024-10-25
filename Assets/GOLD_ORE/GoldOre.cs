using System.Collections;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    public float craftingTimeInSeconds = 15f;
    public GameObject buildingResourceTowerPrefab;
    public GameObject humanResourceTowerPrefab; 
    public GameObject plagueResourceTowerPrefab;
    
    public void CraftResourceTower(Faction faction)
    {
        if (faction == Faction.Human)
            StartCoroutine(CraftHumanResourceTower());

        else if (faction == Faction.Plague)
            StartCoroutine(CraftPlagueResourceTower());
    }

    private IEnumerator CraftHumanResourceTower()
    {
        yield return new WaitForSeconds(craftingTimeInSeconds);
        BuildHumanResourceTower();
    }

    private IEnumerator CraftPlagueResourceTower()
    {
        yield return new WaitForSeconds(craftingTimeInSeconds);
        BuildPlagueResourceTower();
    }

    private void BuildHumanResourceTower()
    {
        Instantiate(humanResourceTowerPrefab, transform.position, Quaternion.identity);
        Debug.Log("Human Resource Tower built.");
    }

    private void BuildPlagueResourceTower()
    {
        Instantiate(plagueResourceTowerPrefab, transform.position, Quaternion.identity);
        Debug.Log("Plague Resource Tower built.");
    }
}
