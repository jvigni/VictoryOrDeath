using System;
using System.Collections;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public Minion lv1MinionPrefab;
    public Minion lv2MinionPrefab;
    public Minion lv3MinionPrefab;

    [SerializeField] bool spawnMinions;
    [SerializeField] int actualMinionLevel = 1;
    [SerializeField] int radius = 50;
    [SerializeField] int minionsAmount = 5;

    private void Start()
    {
        if (spawnMinions)
            for (int i = 0; i < minionsAmount; i++)
                SpawnMob();
    }

    void  SpawnMob() // After 5/10 minutes, dying minions spawns lv2/3 minions respectively
    {
        var rndSpawnPosition = transform.position + new Vector3(UnityEngine.Random.Range(-radius, radius), 0, UnityEngine.Random.Range(-radius, radius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);

        var elapsedMinutes = Time.realtimeSinceStartup / 60;
        if (elapsedMinutes >= 5) actualMinionLevel = 2;
        if (elapsedMinutes >= 10) actualMinionLevel = 3;

        Minion minionPrefab = null;
        if (actualMinionLevel == 1) minionPrefab = lv1MinionPrefab;
        if (actualMinionLevel == 2) minionPrefab = lv2MinionPrefab;
        if (actualMinionLevel >= 3) minionPrefab = lv3MinionPrefab;

        var minion = Instantiate(minionPrefab, rndSpawnPosition, Quaternion.identity);
        minion.OnDeath += () => SpawnMob();
    }
}
