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


    [SerializeField] int mobsAmountPerHex = 10;
    [SerializeField] int spawnRadius = 25;
    [SerializeField] AppManager appManager_;

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

    void SpawnGoldRT(Hexagon hex)
    {
        int goldSpawnchance = UnityEngine.Random.Range(1, 11);
        if (goldSpawnchance == 1)
        {
            var goldRTSpawnLocation = hex.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
            goldRTSpawnLocation.y = Terrain.activeTerrain.SampleHeight(goldRTSpawnLocation);
            Instantiate(goldRTPrefab, goldRTSpawnLocation, Quaternion.identity);
        }
    }
}