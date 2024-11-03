using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    [SerializeField] TabTargeter targeter;
    [SerializeField] Ability[] abilitySlots;
    [SerializeField] List<Ability> LearnedAbilities;

    private void Awake()
    {
        abilitySlots = new Ability[5];
        LearnedAbilities = new List<Ability>();
    }

    void Update()
    {
        // Asignar habilidades a teclas
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseAbility(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseAbility(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UseAbility(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UseAbility(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            UseAbility(4);
    }

    public void UseAbility(int slot)
    {
        if (this.abilitySlots[slot] == null)
            return;

        var target = targeter.CurrentObjective;
        if (target == null)
            return;

        var caster = gameObject;
        this.abilitySlots[slot].Trigger(caster, target.gameObject);
    }

    public void LearnAbility(Ability abilityPrefab)
    {
        // Already learned
        if (LearnedAbilities.Find(ability => ability.Code == abilityPrefab.Code))
            return;

        var abilityClone = abilityPrefab.DeepClone();
        LearnedAbilities.Add(abilityClone);
    }
}
