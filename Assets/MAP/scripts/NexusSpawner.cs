using System.Collections;
using UnityEngine;

public class NexusSpawner : MonoBehaviour
{
    [SerializeField] Minion minionPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] NexusSpawner enemyNexus;

    public bool spawning = true;
    public float secondsToSpawn = .5f;
    public int spawnsAmount = 3;
    public int secondsBetweenWaves = 5;
    
    int spawnCount;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while(spawning)
        {
            spawnCount = 0;
            StartCoroutine(SpawnMinionsRoutine());
            yield return new WaitForSeconds(secondsBetweenWaves);
        }
    }

    IEnumerator SpawnMinionsRoutine()
    {
        var minionsWaveEmptyObj = new GameObject("MINIONS_WAVE");
        while (spawnCount < spawnsAmount)
        {
            var minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
            minion.SetNexusToOBLITERATE(enemyNexus);
            spawnCount++;
            minion.transform.SetParent(minionsWaveEmptyObj.transform);
            yield return new WaitForSeconds(secondsToSpawn);
        }
        yield return null;
    }
}
