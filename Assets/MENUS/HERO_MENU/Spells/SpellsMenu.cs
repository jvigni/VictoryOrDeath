using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsMenu : MonoBehaviour
{
    [SerializeField] List<AbilitySlot> spellSlots; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void NewAbility(Ability ability)
    {
        // Find the first empty spell slot
        var done = false;
        for (int i = 0; i < spellSlots.Count; i++)
        {
            if (!done && spellSlots[i].ability == null)
            {
                done = true;
                spellSlots[i].Init(ability);
            }
        }
    }
}
