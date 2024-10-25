using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Human,
    Plague,

}

public class Hero : MonoBehaviour
{
    public Faction faction;
    public GoldOre nearGoldOre;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearGoldOre != null)
            nearGoldOre.CraftResourceTower(faction)
    }

    private void OnCollisionEnter(Collision collision)
    {
        GoldOre goldOre = collision.gameObject.GetComponent<GoldOre>();
        if (goldOre != null)
            nearGoldOre = goldOre;
    }

    private void OnCollisionExit(Collision collision)
    {
        GoldOre goldOre = collision.gameObject.GetComponent<GoldOre>();
        if (goldOre != null)
            nearGoldOre = null;
    }
}
