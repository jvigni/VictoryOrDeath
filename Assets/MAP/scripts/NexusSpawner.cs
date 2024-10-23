using System.Collections;
using UnityEngine;

public class NexusSpawner : MonoBehaviour
{
    [SerializeField] Minion minionPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] NexusSpawner enemyNexus;

    public bool spawning = true;
    public float secondsToSpawn = 0.5f; // Time between individual minions
    public int spawnsAmount = 3; // Number of minions in each wave
    public int secondsBetweenWaves = 30; // Time between waves (standard in LoL)

    private int spawnCount;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (spawning)
        {
            // Reset spawn count for each wave
            spawnCount = 0;

            // Create a parent GameObject for the minion wave
            var minionsWaveEmptyObj = new GameObject("MINIONS_WAVE");

            // Spawn the minions one by one
            while (spawnCount < spawnsAmount)
            {
                var minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
                minion.SetNexusToOBLITERATE(enemyNexus); // Set the enemy Nexus as the target

                // Parent the minion under the wave object
                minion.transform.SetParent(minionsWaveEmptyObj.transform);

                spawnCount++;

                // Wait before spawning the next minion
                yield return new WaitForSeconds(secondsToSpawn);
            }

            // Wait before starting the next wave
            yield return new WaitForSeconds(secondsBetweenWaves);
        }
    }
}
