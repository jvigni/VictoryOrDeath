using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public List<Ability> learnedAbilities = new List<Ability>();
    public Ability[] abilitySlots = new Ability[4]; // 4 habilidades asignables

        void Update()
    {
        // Asignar habilidades a teclas
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseAbility(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseAbility(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UseAbility(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UseAbility(3);
    }

    public void UseAbility(int slot)
    {
        if (abilitySlots[slot] != null)
            abilitySlots[slot].Activate(gameObject);
        else
            Debug.Log("No ability assigned on slot " + slot);
    }
}
