using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMenu : MonoBehaviour
{
    public EquipmentSlot head;
    public EquipmentSlot cloak;
    public EquipmentSlot armor;
    public EquipmentSlot relic;
    public EquipmentSlot gloves;
    public EquipmentSlot boots;
    public EquipmentSlot main_hand;
    public EquipmentSlot off_hand;

    public string class_name;
    public int power_level;
    public Image element1Img;
    public Image element2Img;
    public string element1txt;
    public string element2txt;

    public StatSlot damageStat;
    public StatSlot defenseStat;
    public StatSlot staminaStat;
    public StatSlot manaStat;

    public List<InventorySlot> inventory;
    public GameObject trash;

    public int resources;
    public List<UpgradeSlot> Upgrades;
}
