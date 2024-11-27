using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    [SerializeField] List<Ability> abilities;
    [SerializeField] HeroMenu heroMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            abilities[0].Trigger(gameObject, Provider.Targeter.CurrentTarget);
    }
}
