using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    [SerializeField] List<Hexagon> allHexagons;
    [SerializeField] GoldRT goldRTPrefab;
    [SerializeField] List<Mob> mobsLv1Prefab;
    [SerializeField] List<Mob> mobsLv2Prefab;
    [SerializeField] List<Mob> mobsLv3Prefab;


    [SerializeField] int actualMobLevel = 1;
    [SerializeField] int spawnRadius = 25;
    [SerializeField] int mobsAmount = 10;
    [SerializeField] AppManager appManager;

    private void Awake()
    {
        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsGoldRT)
                SpawnGoldRT(hex);
        });

        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsMobs)
            {
                var parentObj = new GameObject("MOB_CAMP");
                for(int i =0; i < mobsAmount; i++)
                    SpawnMob(hex, parentObj.transform);
            }
        });
    }

    void SpawnMob(Hexagon hex, Transform parentObj)
    {
        var elapsedMinutes = appManager.GetElapsedGameSeconds() / 60;
        if (elapsedMinutes >= 5) actualMobLevel = 2;
        if (elapsedMinutes >= 10) actualMobLevel = 3;

        var rndSpawnPosition = transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);
        GameObject newMob = Instantiate(GetRndMobPrefab().gameObject, rndSpawnPosition, Quaternion.identity);
        newMob.transform.SetParent(parentObj.transform);
        newMob.GetComponent<LifeForm>().OnDeath += () => SpawnMob(hex, parentObj);
    }

    internal Mob GetRndMobPrefab()
    {
        List<Mob> selectedMobs = null;

        if (actualMobLevel == 1) selectedMobs = mobsLv1Prefab;
        else if (actualMobLevel == 2) selectedMobs = mobsLv2Prefab;
        else if (actualMobLevel == 3) selectedMobs = mobsLv3Prefab;

        // Pick a random mob from the selected list
        int randomIndex = UnityEngine.Random.Range(0, selectedMobs.Count);
        return selectedMobs[randomIndex];
    }

    void SpawnGoldRT(Hexagon hex)
    {
        int goldSpawnchance = UnityEngine.Random.Range(1, 11);
        if (goldSpawnchance == 1)
        {
            var goldRTSpawnLocation = transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
            goldRTSpawnLocation.y = Terrain.activeTerrain.SampleHeight(goldRTSpawnLocation);
            Instantiate(goldRTPrefab, goldRTSpawnLocation, Quaternion.identity);
        }
    }
}