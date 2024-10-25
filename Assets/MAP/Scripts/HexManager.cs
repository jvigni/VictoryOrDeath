using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    [SerializeField] AppManager appManager_;
    [SerializeField] List<Hexagon> allHexagons;
    [Header("GOLD RTs")]
    [SerializeField] GoldRT goldRtPrefab;
    [SerializeField] int goldOrespawnPercentage = 20;
    [SerializeField] int maxGoldOreAmount = 12;
    [SerializeField] int spawnedGoldOresCount_;
    [SerializeField] int minGoldOreAmount = 6;
    [Header("MOBS")]
    [SerializeField] int mobsAmountPerHex = 10;
    [SerializeField] int spawnRadius = 25;
    [SerializeField] List<Mob> mobsLv1Prefab;
    [SerializeField] List<Mob> mobsLv2Prefab;
    [SerializeField] List<Mob> mobsLv3Prefab;

    private void Awake()
    {
        allHexagons.ForEach(hex =>
        {
            var parent = new GameObject("GOLD_ORES");
            if (hex.spawnsGoldRT)
                SpawnGoldRT(hex, parent.transform);
        });

        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsMobs)
            {
                var parentObj = new GameObject("MOB_CAMP");
                for(int i =0; i < mobsAmountPerHex; i++)
                    SpawnMob(hex, parentObj.transform);
            }
        });
    }

    void SpawnMob(Hexagon hex, Transform parentObj)
    {
        var rndSpawnPosition = hex.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);
        GameObject newMob = Instantiate(GetRndMobPrefab().gameObject, rndSpawnPosition, Quaternion.identity);
        newMob.transform.SetParent(parentObj.transform);
        newMob.GetComponent<LifeForm>().OnDeath += () => SpawnMob(hex, parentObj);
    }

    internal Mob GetRndMobPrefab()
    {
        var selectedMobsList = mobsLv1Prefab;

        var elapsedMinutes = appManager_.GetElapsedGameSeconds() / 60;
        if (elapsedMinutes >= 5) selectedMobsList = mobsLv2Prefab;
        if (elapsedMinutes >= 10) selectedMobsList = mobsLv3Prefab;

        // Pick a random mob from the selected list
        int randomIndex = UnityEngine.Random.Range(0, selectedMobsList.Count);
        return selectedMobsList[randomIndex];
    }

    void SpawnGoldRT(Hexagon hex, Transform parent)
    {
        // IDEA: sacar cantidad total entre min/max, y deidir rnd los hexs a setear
        spawnedGoldOresCount_ = UnityEngine.Random.Range(minGoldOreAmount, maxGoldOreAmount +1);
        var spawnedOresCountdown = spawnedGoldOresCount_;
        while (spawnedOresCountdown > 0) {
            spawnedOresCountdown--;

            int goldSpawnChance = UnityEngine.Random.Range(1, 101);
            if (goldSpawnChance > goldOrespawnPercentage) 
                return;

            var goldRTSpawnLocation = hex.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
            goldRTSpawnLocation.y = Terrain.activeTerrain.SampleHeight(goldRTSpawnLocation);
            var ore = Instantiate(goldRtPrefab, goldRTSpawnLocation, Quaternion.identity);
            ore.transform.SetParent(parent.transform);

            spawnedGoldOresCount_++;
        }
    }
}