using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    [SerializeField] AbilityLibrary library;

    internal void LearnAbility(AbilityCode code)
    {
        library.GetAbilityClone(code);

    }
}
