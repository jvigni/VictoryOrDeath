using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    [SerializeField] AppManager appManager_;
    [SerializeField] List<Hexagon> allHexagons;
    [Header("GOLD RTs")]
    [SerializeField] GoldOre goldOrePrefab;
    [SerializeField] int goldOrespawnPercentage = 20;
    [Space(10)]
    [SerializeField] int maxGoldOreAmount = 12;
    [SerializeField] int goldOresInMap;
    [SerializeField] int minGoldOreAmount = 6;
    int goldOresPlaced;
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
        goldOresInMap = UnityEngine.Random.Range(minGoldOreAmount, maxGoldOreAmount + 1);
        allHexagons.RemoveAll(hex => !hex.spawnsGoldRT);
        allHexagons.Shuffle();
        allHexagons.ForEach(hex =>
        {
            if (goldOresPlaced >= goldOresInMap)
                return;

            SpawnGoldOre(hex, parent.transform);
            goldOresPlaced++;
        });


        var allMobsParent = new GameObject("MOBS");
        allHexagons.RemoveAll(hex => !hex.spawnsMobs);
        var mobsCampParent = new GameObject("CAMP");
        allHexagons.ForEach(hex =>
        {
        for (int i = 0; i < mobsAmountPerHex; i++)
            {
                var mob = SpawnMob(hex, hex.mobsLevel, mobsCampParent.transform);
                mobsCampParent.gameObject.transform.SetParent(allMobsParent.transform);
            }
        });
    }

    void SpawnGoldOre(Hexagon hex, Transform parent)
    {
        // To spawn random position as mobs and not in the center of the hexagon: DEPRECATED (center spawn = good)
        //var goldRTSpawnLocation = hex.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
        //goldRTSpawnLocation.y = Terrain.activeTerrain.SampleHeight(goldRTSpawnLocation);

        var ore = Instantiate(goldOrePrefab, hex.transform.position, Quaternion.identity);
        var newTransformPosition = ore.transform.position;
        newTransformPosition.y = Terrain.activeTerrain.SampleHeight(ore.transform.position);
        ore.transform.position = newTransformPosition;
        ore.transform.SetParent(parent.transform);
        hex.goldOre = ore;
    }

    Mob SpawnMob(Hexagon hex, int level, Transform parentObj)
    {
        var rndSpawnPosition = hex.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);
        Mob newMob = Instantiate(GetRndMobPrefab(level), rndSpawnPosition, Quaternion.identity);
        newMob.transform.SetParent(parentObj.transform);
        //TODO FIX INFINITE MOB RESPAWNING. lambda is not the solution
        //newMob.GetComponent<LifeForm>().OnDeath += () => SpawnMob(hex, level, parentObj);
        hex.mobs = newMob;
        return newMob;
    }

    internal Mob GetRndMobPrefab(int level)
    {
        var selectedMobsList = mobsLv1Prefab;

        var elapsedMinutes = appManager_.GetElapsedGameSeconds() / 60;
        if (elapsedMinutes >= 5 || level == 2) selectedMobsList = mobsLv2Prefab;
        if (elapsedMinutes >= 10 || level == 3) selectedMobsList = mobsLv3Prefab;

        // Pick a random mob from the selected list
        int randomIndex = UnityEngine.Random.Range(0, selectedMobsList.Count);
        return selectedMobsList[randomIndex];
    }
}