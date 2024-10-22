using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSpawnManager : MonoBehaviour
{
    [SerializeField] List<HexSpawner> hexSpawners;
    [SerializeField] List<Minion> minionsLv1Prefab;
    [SerializeField] List<Minion> minionsLv2Prefab;
    [SerializeField] List<Minion> minionsLv3Prefab;

    private void Awake()
    {
        hexSpawners.ForEach(spawner =>
            spawner.lv1MinionPrefab = minionsLv1Prefab[Random.Range(0, minionsLv1Prefab.Count)]);

        hexSpawners.ForEach(spawner =>
            spawner.lv2MinionPrefab = minionsLv2Prefab[Random.Range(0, minionsLv2Prefab.Count)]);

        hexSpawners.ForEach(spawner =>
            spawner.lv3MinionPrefab = minionsLv3Prefab[Random.Range(0, minionsLv3Prefab.Count)]);
    }
}
