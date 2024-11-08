using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroMenu : MonoBehaviour
{

    public Camera mainCamera;

    [Header("EQUIPMENT")]
    public EquipmentSlot head;
    public EquipmentSlot cloak;
    public EquipmentSlot armor;
    public EquipmentSlot relic;
    public EquipmentSlot gloves;
    public EquipmentSlot boots;
    public EquipmentSlot main_hand;
    public EquipmentSlot off_hand;

    [Header("CLASS")]
    public TextMeshProUGUI class_name;
    public TextMeshProUGUI power_level;
    public Image element1Img;
    public Image element2Img;
    public TextMeshProUGUI element1txt;
    public TextMeshProUGUI element2txt;


    [Header("STATS")]
    public StatSlot damageStat;
    public StatSlot defenseStat;
    public StatSlot staminaStat;
    public StatSlot manaStat;


    [Header("INVENTORY")]
    public List<InventorySlot> inventory;
    public Transform trash;

    [Header("UPGRADES")]
    public TextMeshProUGUI resources;
    public List<UpgradeSlot> upgrades;
    public Button reroll;


    [Header("SPELLS")]
    [SerializeField] List<AbilitySlot> spellSlots;

    //[SerializeField] List<SpellSlot> spellSlots;

    private void OnEnable()
    {
        Cursor.visible = true;
        mainCamera.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = false;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        mainCamera.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = true;
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

    public void SetWeapon(Weapon wpn)
    {
        main_hand.weapon = wpn;
    }


}
