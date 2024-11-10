using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    [SerializeField] TabTargeter targeter;
    [SerializeField] HeroMenu heroMenu;

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
        if (Input.GetKeyDown(KeyCode.Alpha5))
            UseAbility(4);
    }

    public void UseAbility(int slot)
    {
        heroMenu.spellSlots[slot].ability.Trigger(gameObject, targeter.CurrentObjective.gameObject);
    }
}
