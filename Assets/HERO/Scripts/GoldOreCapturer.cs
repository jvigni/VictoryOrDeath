using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO esto va en Hero.cs
public enum Faction
{
    Human,
    Plague,
}

public class GoldOreCapturer : MonoBehaviour
{
    [SerializeField] Faction faction;

    void OnTriggerEnter(Collider other)
    {
        var goldOre = other.GetComponent<GoldOre>();
        if (goldOre != null)
        {
            //var faction = GetComponent<Hero>().faction;
            goldOre.CraftResourceTower(faction);
        }
    }
}
