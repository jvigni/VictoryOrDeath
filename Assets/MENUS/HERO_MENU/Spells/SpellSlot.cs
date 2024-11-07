using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    public Ability ability;

    public void OnDrop(PointerEventData eventData)
    {

    }

    internal void Init(Ability ability)
    {
        this.ability = ability;
        icon.sprite = ability.Icon;
    }
}
