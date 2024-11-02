using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobID
{
    Boar,
    Wolf,
    Bear,
    // Add more mob types here
}

[System.Serializable]
public class MobInfo
{
    public MobID id;
    public GameObject model;
    public MobData mobData;
}

public class MobPool : MonoBehaviour
{
    [SerializeField] private List<MobInfo> mobsInfo;

    public Mob InstantiateRndMob(int level, Vector3 position)
    {
        // Find random mobInfo by level
        var mobsInfoFilteredByLevel = mobsInfo.FindAll(info => info.mobData.level == level);
        if (mobsInfoFilteredByLevel.Count == 0)
        {
            Debug.LogWarning("No mobs found for the given level.");
            return null;
        }

        int rndIndex = Random.Range(0, mobsInfoFilteredByLevel.Count);
        MobInfo mobInfo = mobsInfoFilteredByLevel[rndIndex];

        // Instantiate the random mob at the specified position
        GameObject mobInstance = Instantiate(mobInfo.model, position, Quaternion.identity);

        // Load mob data if necessary (e.g., setting health, behavior)
        mobInstance.GetComponent<LifeForm>().Init(mobInfo.mobData.health, Team.GAIA);
        return mobInstance;
    }
}
