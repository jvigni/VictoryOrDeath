using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [SerializeField] Mob mobs;
    [SerializeField] GoldRT goldRT;
    [SerializeField] HexManager hexManager;

    public bool spawnsMobs = true;
    public bool spawnsGoldRT = true;
}
