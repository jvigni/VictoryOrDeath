using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    [SerializeField] AppManager appManager_;
    [SerializeField] List<Hexagon> allHexagons;
    [Header("GOLD RTs")]
    [SerializeField] GoldRT goldOrePrefab;
    [SerializeField] int goldOrespawnPercentage = 20;
    [Space(10)]
    [SerializeField] int maxGoldOreAmount = 12;
    [SerializeField] int goldOresInMap;
    [SerializeField] int minGoldOreAmount = 6;
    [Header("MOBS")]
    [SerializeField] int mobsAmountPerHex = 10;
    [SerializeField] int spawnRadius = 25;
    [Space(10)]
    [SerializeField] List<Mob> mobsLv1Prefab;
    [SerializeField] List<Mob> mobsLv2Prefab;
    [SerializeField] List<Mob> mobsLv3Prefab;

    private void Awake()
    {
        var parent = new GameObject("GOLD_ORES");
        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsGoldRT)
                SpawnGoldRT(hex, parent.transform);
        });

        var allMobsParent = new GameObject("MOBS");
        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsMobs)
            {
                var mobsCampParent = new GameObject("CAMP");
                for(int i =0; i < mobsAmountPerHex; i++)
                {
                    var mob = SpawnMob(hex, mobsCampParent.transform);
                    mobsCampParent.gameObject.transform.SetParent(allMobsParent.transform);
                }
                    
            }
        });
    }

    Mob SpawnMob(Hexagon hex, Transform parentObj)
    {
        var rndSpawnPosition = hex.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);
        Mob newMob = Instantiate(GetRndMobPrefab(), rndSpawnPosition, Quaternion.identity);
        newMob.transform.SetParent(parentObj.transform);
        newMob.GetComponent<LifeForm>().OnDeath += () => SpawnMob(hex, parentObj);
        hex.mobs = newMob;
        return newMob;
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
        goldOresInMap = UnityEngine.Random.Range(minGoldOreAmount, maxGoldOreAmount +1);
        var spawnedOresCountdown = goldOresInMap;
        while (spawnedOresCountdown > 0) {

            int goldSpawnChance = UnityEngine.Random.Range(1, 101);
            if (goldSpawnChance > goldOrespawnPercentage) 
                return;

            //var goldRTSpawnLocation = hex.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
            //goldRTSpawnLocation.y = Terrain.activeTerrain.SampleHeight(goldRTSpawnLocation);

            var ore = Instantiate(goldOrePrefab, hex.transform.position, Quaternion.identity);
            var newTransformPosition = ore.transform.position;
            newTransformPosition.y = Terrain.activeTerrain.SampleHeight(ore.transform.position);
            ore.transform.position = newTransformPosition;
            ore.transform.SetParent(parent.transform);
            hex.goldOre = ore;
            spawnedOresCountdown--;
        }
    }
}