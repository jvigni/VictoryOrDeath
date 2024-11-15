using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;

public class NexusSpawner : MonoBehaviour
{
    [System.Serializable]
    public class MinionType
    {
        public Minion minionPrefab;
        public int amountToSpawn;
    }

    [SerializeField] Team team;
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
                {
                    Vector3 spawnPosition = spawnPoint.position;
                    NavMeshHit hit;

                    if (NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
                    {
                        spawnPosition = hit.position;
                    }

                    var minion = Instantiate(minionType.minionPrefab, spawnPosition, Quaternion.identity);
                    minion.SetMySide(team);
                    minion.SetNexusToOBLITERATE(enemyNexus);
                    minion.AdjustHeightToTerrain();
                    minion.MoveTowardsNexus();

                    minionsForTheNight.Add(minion);
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
        }
    }

    public void NightStarts()
    {
        FX.gameObject.SetActive(true);
        StartCoroutine(SpawnMinions());
    }

    public void DayStarts()
    {
        FX.gameObject.SetActive(false);
    }

    public List<Minion> GetMinionsForTheNight()
    {
        return minionsForTheNight;
    }
}
