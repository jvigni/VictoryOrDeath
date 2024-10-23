using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public Item[] items = new Item[4]; // 4 ítems equipables

    void Update()
    {
        // Usar ítem asignado
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UseItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UseItem(3);
    }

    public void UseItem(int slot)
    {
        if (items[slot] != null)
        {
            items[slot].Use(gameObject);  // Usa el ítem
        }
    }
}
