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
                SpawnGoldRT(hex);
        });

        allHexagons.ForEach(hex =>
        {
            if (hex.spawnsMobs)
                SpawnMobs(hex);
        });
    }

    void SpawnGoldRT(Hexagon hex)
    {

    }

    void SpawnMobs(Hexagon hex)
    {

        var elapsedMinutes = Time.unscaledTime / 60;
        if (elapsedMinutes >= 5) actualMobLevel = 2;
        if (elapsedMinutes >= 10) actualMobLevel = 3;

        GameObject newMob = Instantiate(GetRndMobPrefab().gameObject, hex.transform.position, Quaternion.identity);
        newMob.GetComponent<LifeForm>().OnDeath += SpawnMobs;
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
}