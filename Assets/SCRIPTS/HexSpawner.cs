using System;
using System.Collections;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public Minion lv1MinionPrefab;
    public Minion lv2MinionPrefab;
    public Minion lv3MinionPrefab;

    [SerializeField] int actualMinionLevel = 1;
    [SerializeField] int radius = 50;
    [SerializeField] int minionsAmount = 5;

    private void Start()
    {
        for (int i = 0; i < minionsAmount; i++)
            SpawnMob(actualMinionLevel);
    }

    void  SpawnMob(int actualMinionLevel)
    {
        var rndSpawnPosition = transform.position + new Vector3(UnityEngine.Random.Range(-radius, radius), 0, UnityEngine.Random.Range(-radius, radius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);

        Minion minionPrefab = null;
        if (actualMinionLevel == 1) minionPrefab = lv1MinionPrefab;
        if (actualMinionLevel == 2) minionPrefab = lv2MinionPrefab;
        if (actualMinionLevel == 3) minionPrefab = lv3MinionPrefab;

        var minion = Instantiate(minionPrefab, rndSpawnPosition, Quaternion.identity);
        minion.OnDeath += () => SpawnMob(actualMinionLevel + 1);
    }
}
