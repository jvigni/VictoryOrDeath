using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potion")]
public class Potion : Item
{
    public float healthRestorationAmount; // Cantidad de salud que restaurar� la poci�n

    public override void Use(GameObject character)
    {
        Debug.Log($"Heal: {healthRestorationAmount}");
    }
}
