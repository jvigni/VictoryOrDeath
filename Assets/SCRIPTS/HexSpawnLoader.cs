using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSpawnManager : MonoBehaviour
{
    [SerializeField] List<HexSpawner> spawners;
    [SerializeField] List<Minion> minionsLv1;
    [SerializeField] List<Minion> minionsLv2;
    [SerializeField] List<Minion> minionsLv3;

    private void Start()
    {
        
    }
}
