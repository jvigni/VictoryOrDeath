using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStarter : MonoBehaviour
{
    [SerializeField] Camera camera3D;
    [SerializeField] GameObject combatMenu;
    [SerializeField] GameObject heroGraphics;
    [SerializeField] CombatManager combatManager;
    [SerializeField] ParticleSystem glowFX;
    [SerializeField] LifeForm myLifeform;
    [SerializeField] GameObject enterCombatPanelFX;

    private void OnTriggerEnter(Collider other)
    {
        var lifeform = other.GetComponent<LifeForm>();
        if (lifeform != null)
        {
            if (lifeform.team == myLifeform.team)
                return;

            Debug.Log("COMBAT!" + lifeform.name);
            
            heroGraphics.SetActive(false);
            Cursor.visible = true;
            camera3D.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = false;
            glowFX.Play();

            enterCombatPanelFX.SetActive(true);

            combatMenu.SetActive(true);
            combatManager.StartCombat(myLifeform, lifeform);
        }
    }
}
