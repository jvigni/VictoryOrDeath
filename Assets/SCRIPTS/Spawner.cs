using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Minion minionPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Minion minionsTarget;

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
            StartCoroutine(SpawnMobRoutine());
            yield return new WaitForSeconds(secondsBetweenWaves);
        }
    }

    IEnumerator SpawnMobRoutine()
    {
        while(spawnCount < spawnsAmount)
        {
            var minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
            minion.SetTarget(minionsTarget);
            spawnCount++;
            yield return new WaitForSeconds(secondsToSpawn);
        }
        yield return null;
    }
}
