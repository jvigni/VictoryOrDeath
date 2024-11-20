using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStarter : MonoBehaviour
{
    [SerializeField] Camera camera3D;
    [SerializeField] GameObject heroGraphics;
    [SerializeField] LifeForm myLifeform;
    [SerializeField] GameObject enterCombatPanelFX;
    [SerializeField] CombatManager combatManager;

    private void OnTriggerEnter(Collider other)
    {
        var lifeform = other.GetComponent<LifeForm>();
        if (lifeform != null)
        {
            if (lifeform.team == myLifeform.team)
                return;

            Debug.Log("COMBAT!" + lifeform.name);
            
            //heroGraphics.SetActive(false);
            Cursor.visible = true;
            camera3D.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = false;

            lifeform.gameObject.SetActive(false);
            enterCombatPanelFX.SetActive(true);
            combatManager.StartCombat(myLifeform, lifeform);


        }
    }
}
