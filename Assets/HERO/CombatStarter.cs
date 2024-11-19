using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStarter : MonoBehaviour
{
    [SerializeField] Camera camera3D;
    [SerializeField] GameObject combatMenu;
    [SerializeField] GameObject heroGraphics;
    [SerializeField] CombatManager combatManager;

    private void OnTriggerEnter(Collider other)
    {
        var lifeform = other.GetComponent<LifeForm>();
        if (lifeform == null)
            return;

        Debug.Log("COMBAT!" + lifeform.name);
        combatMenu.SetActive(true);
        heroGraphics.SetActive(false);
        Cursor.visible = true;
        StartCoroutine(Utils.CenterCursor());
        camera3D.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = false;

        combatManager.StartCombat(lifeform);
    }
}
