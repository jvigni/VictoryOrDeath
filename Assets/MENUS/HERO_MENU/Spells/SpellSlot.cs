using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public Ability ability;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag called");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag called");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag called");
    }

    internal void Init(Ability ability)
    {
        Debug.Log("Init method called");
        this.ability = ability;
        icon.sprite = ability.Icon;
    }
}
