using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image icon;

    public void SetSprite(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
