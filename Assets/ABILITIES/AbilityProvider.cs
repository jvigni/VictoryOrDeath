using UnityEngine;

public enum AbilityCode
{
    Fireball,
    Fireblast,
}


public class AbilityProvider : MonoBehaviour
{
    [SerializeField] Ability fireball;
    [SerializeField] Ability fireblast;

    public void TeachAbility(AbilityCode code, CharacterAbilities characterAbilities)
    {
        var abilityPrefab = GetAbilityPrefab(code);
        
        if (characterAbilities.LearnedAbilities.Find(ability => ability.Code == abilityPrefab.Code))
            return; // Already learned

        var abilityClone = abilityPrefab.DeepClone();
        characterAbilities.LearnedAbilities.Add(abilityClone);
    }

    private Ability GetAbilityPrefab(AbilityCode code)
    {
        Ability ability = null;
        if (code.Equals(AbilityCode.Fireball))
            ability = fireball;

        if (code.Equals(AbilityCode.Fireblast))
            ability = fireblast;


        // Ad infinitum..
        return ability;
    }
}