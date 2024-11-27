using UnityEngine;

public class Spellbook : MonoBehaviour
{
    [SerializeField] Ability Ability1;
    [SerializeField] Ability Ability2;
    [SerializeField] Ability Ability3;
    [SerializeField] Ability Ability4;
    [SerializeField] Ability Ability5;
    [SerializeField] Ability AbilityF;
    [SerializeField] Ability AbilityR;
    [SerializeField] Ability AbilityT;
    [SerializeField] Ability AbilityG;
    //[SerializeField] HeroMenu heroMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TriggerAbility(Ability1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            TriggerAbility(Ability2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            TriggerAbility(Ability3);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            TriggerAbility(Ability4);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            TriggerAbility(Ability5);
        else if (Input.GetKeyDown(KeyCode.F))
            TriggerAbility(AbilityF);
        else if (Input.GetKeyDown(KeyCode.R))
            TriggerAbility(AbilityR);
        else if (Input.GetKeyDown(KeyCode.T))
            TriggerAbility(AbilityT);
        else if (Input.GetKeyDown(KeyCode.G))
            TriggerAbility(AbilityG);
    }

    private void TriggerAbility(Ability ability)
    {
        if (ability != null)
            ability.Trigger(gameObject, Provider.Targeter.CurrentTarget);
    }
}