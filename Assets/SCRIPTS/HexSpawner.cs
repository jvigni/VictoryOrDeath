using System.Collections;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public Minion minionPrefab;
    
    [SerializeField] int radius = 50;
    [SerializeField] int minionsAmount = 5;

    private void Start()
    {
        for (int i = 0; i < minionsAmount; i++)
            SpawnMob();
    }

    void SpawnMob()
    {
        var rndSpawnPosition = transform.position + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
        rndSpawnPosition.y = Terrain.activeTerrain.SampleHeight(rndSpawnPosition);

        var minion = Instantiate(minionPrefab, rndSpawnPosition, Quaternion.identity);
        //TODO minion.IsMob(rndSpawnPosition);

        minion.OnDeath += SpawnMob;
    }
}
