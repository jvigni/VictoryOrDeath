using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [Space(10)]
    public TextMeshProUGUI class_name;
    public TextMeshProUGUI power_level;
    public Image element1Img;
    public Image element2Img;
    public TextMeshProUGUI element1txt;
    public TextMeshProUGUI element2txt;
    [Space(10)]
    public StatSlot damageStat;
    public StatSlot defenseStat;
    public StatSlot staminaStat;
    public StatSlot manaStat;
    [Space(10)]
    public List<InventorySlot> inventory;
    public Transform trash;
    [Space(10)]
    public TextMeshProUGUI resources;
    public List<UpgradeSlot> upgrades;
    public Button reroll;
    public Camera mainCamera;

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
}