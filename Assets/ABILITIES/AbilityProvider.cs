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

    public void LearnAbility(AbilityCode code, CharacterAbilities characterAbilities)
    {
        var abilityPrefab = GetAbilityPrefab(code);
        characterAbilities.LearnAbility(abilityPrefab);
    }

    private Ability GetAbilityPrefab(AbilityCode code)
    {
        Ability ability = null;
        if (code.Equals(AbilityCode.Fireball))
            ability = fireball;

        if (code.Equals(AbilityCode.Fireblast))
            ability = fireblast;


        // Ad infinitum..
        return ability
    }
}