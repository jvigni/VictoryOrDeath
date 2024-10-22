using System;
using System.Collections;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public Mob lv1MobPrefab;
    public Mob lv2MobPrefab;
    public Mob lv3MobPrefab;

    [SerializeField] bool spawnMinions;
    [SerializeField] int actualMobLevel = 1;
    [SerializeField] int radius = 50;
    [SerializeField] int minionsAmount = 5;

    private void Start()
    {
        if (spawnMinions)
        {
            var mobsEmptyObj = new GameObject("MOBS_LV_" + actualMobLevel);
            for (int i = 0; i < minionsAmount; i++)
                SpawnMob(mobsEmptyObj.transform);
        }
    }

    void  SpawnMob(Transform emptyParent) // After 5/10 minutes, dying minions spawns lv2/3 minions respectively
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

        var mob = Instantiate(mobPrefab, rndSpawnPosition, Quaternion.identity);
        mob.transform.SetParent(emptyParent);
        mob.OnDeath += () => SpawnMob(emptyParent);
    }
}
