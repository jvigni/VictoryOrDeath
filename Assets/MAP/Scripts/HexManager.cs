using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    [SerializeField] List<Hexagon> allHexagons;
    [SerializeField] List<Mob> mobsLv1Prefab;
    [SerializeField] List<Mob> mobsLv2Prefab;
    [SerializeField] List<Mob> mobsLv3Prefab;

    [SerializeField] int actualMobLevel = 1;
    [SerializeField] int spawnRadius = 25;
    [SerializeField] int minionsAmount = 10;

    private void Awake()
    {
        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsGoldRT)
                SpawnGoldRT(hex)
        });

        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsGoldRT)
                SpawnMobs(hex)
        });


        allHexagons.ForEach(spawner =>
            spawner.lv1MobPrefab = mobsLv1Prefab[Random.Range(0, mobsLv1Prefab.Count)]);

        allHexagons.ForEach(spawner =>
            spawner.lv2MobPrefab = mobsLv2Prefab[Random.Range(0, mobsLv2Prefab.Count)]);

        allHexagons.ForEach(spawner =>
            spawner.lv3MobPrefab = mobsLv3Prefab[Random.Range(0, mobsLv3Prefab.Count)]);
    }

    void SpawnGoldRT(Hexagon hex)
    {

    }

    void SpawnMobs(Hexagon hex)
    {
        var elapsedMinutes = Time.realtimeSinceStartup / 60;
        if (elapsedMinutes >= 5) actualMobLevel = 2;
        if (elapsedMinutes >= 10) actualMobLevel = 3;

        // Instantiate the mob and set its level
        GameObject newMob = Instantiate(mobPrefab, hex.transform.position, Quaternion.identity);
        newMob.GetComponent<Mob>().Initialize(mobLevel);
    }

    internal Mob GetRndMob()
    {
        throw new NotImplementedException();
    }
}