using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class NexusSpawner : MonoBehaviour
{
    [System.Serializable]
    public class MinionType
    {
        public Minion minionPrefab;
        public int amountToSpawn;
    }

    [SerializeField] List<MinionType> minionTypesToSpawn;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform FX;
    [SerializeField] NexusSpawner enemyNexus;

    public float spawnInterval = 1f;

    private List<Minion> minionsForTheNight;

    IEnumerator SpawnMinions()
    {
        minionsForTheNight = new List<Minion>();

        foreach (var minionType in minionTypesToSpawn)
        {
            for (int i = 0; i < minionType.amountToSpawn; i++)
            {
                var minion = Instantiate(minionType.minionPrefab, spawnPoint.position, Quaternion.identity);
                minion.SetNexusToOBLITERATE(enemyNexus);

                minionsForTheNight.Add(minion);
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        FX.gameObject.SetActive(false);
    }

    public void NightStarts()
    {
        FX.gameObject.SetActive(true);
        StartCoroutine(SpawnMinions());
    }

    public List<Minion> GetMinionsForTheNight()
    {
        return minionsForTheNight;
    }
}
