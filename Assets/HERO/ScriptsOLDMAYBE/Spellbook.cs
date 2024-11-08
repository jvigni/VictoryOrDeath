using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    [SerializeField] AbilityLibrary library;
    [SerializeField] HeroMenu heroMenu;
    //List<Ability> abilities;

    private void Start()
    {
        //abilities = new List<Ability>();    
    }

    internal void LearnAbility(int index, AbilityCode code)
    {
        var ability = library.GetAbilityClone(code);
        //abilities.Add(ability);
        heroMenu.NewAbility(index, ability);
    }
    /*
    public void Cast(int index)
    {
        heroMenu.GetAbility(index).Trigger();
    }*/
}
