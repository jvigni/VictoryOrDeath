using System;
using System.Collections;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    [SerializeField] Mob mobs;
    [SerializeField] GoldRT goldRT;
    [SerializeField] HexManager hexManager;

    [SerializeField] bool spawnMinions = false;
    [SerializeField] int actualMobLevel = 1;
    [SerializeField] int radius = 25;
    [SerializeField] int minionsAmount = 10;

    private void Start()
    {
        if (spawnMinions)
        {
            var mobsEmptyObj = new GameObject("MOBS_LV_" + actualMobLevel);
            for (int i = 0; i < minionsAmount; i++)
                SpawnMob(mobsEmptyObj.transform);

            int goldSpawnchance = UnityEngine.Random.Range(1, 11);
            if (goldSpawnchance == 1)
            {
                var goldRTSpawnLocation = transform.position + new Vector3(UnityEngine.Random.Range(-radius, radius), 0, UnityEngine.Random.Range(-radius, radius));
                goldRTSpawnLocation.y = Terrain.activeTerrain.SampleHeight(goldRTSpawnLocation);
                Instantiate(hexManager.GetRndMob(), goldRTSpawnLocation, Quaternion.identity);
            }
        }
    }

    void SpawnMob(Transform emptyParent)
    {
        var rndSpawnPosition = transform.position + new Vector3(UnityEngine.Random.Range(-radius, radius), 0, UnityEngine.Random.Range(-radius, radius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);

        var elapsedMinutes = Time.realtimeSinceStartup / 60;
        if (elapsedMinutes >= 5) actualMobLevel = 2;
        if (elapsedMinutes >= 10) actualMobLevel = 3;

        Mob mobPrefab = null;
        if (actualMobLevel == 1) mobPrefab = lv1MobPrefab;
        if (actualMobLevel == 2) mobPrefab = lv2MobPrefab;
        if (actualMobLevel >= 3) mobPrefab = lv3MobPrefab;

        if (mobPrefab == null)
        {
            Debug.LogError("Mob prefab is null for level " + actualMobLevel);
            return;
        }

        var mob = Instantiate(mobPrefab, rndSpawnPosition, Quaternion.identity);
        mob.transform.SetParent(emptyParent);

        LifeForm lifeForm = mob.GetComponent<LifeForm>();
        if (lifeForm == null)
        {
            Debug.LogError("LifeForm component is missing on the mob prefab.");
            return;
        }

        lifeForm.OnDeath += HandleMobDeath;
    }

    void HandleMobDeath()
    {
        // You may need to pass the emptyParent if required in this context
        SpawnMob(transform);
    }

}
