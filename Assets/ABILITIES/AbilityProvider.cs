using UnityEngine;

public enum AbilityCode
{
    Fireball,
    Fireblast,
}

public class AbilityProvider : MonoBehaviour
{
    [SerializeField] Ability fireballPrefab;
    [SerializeField] Ability fireblastPrefab;
    [SerializeField] HeroMenu heroMenu;

    public void TeachAbility(AbilityCode code, CharacterAbilities characterAbilities)
    {
        var abilityPrefab = GetAbilityPrefab(code);

        var abilityClone = Instantiate(abilityPrefab, transform.position, Quaternion.identity);
        abilityClone.transform.SetParent(gameObject.transform);

        characterAbilities.Abilities.Add(abilityClone);
        heroMenu.AbilityAdded(abilityClone);
    }

    private Ability GetAbilityPrefab(AbilityCode code)
    {
        Ability ability = null;
        if (code.Equals(AbilityCode.Fireball))
            ability = fireballPrefab;

        if (code.Equals(AbilityCode.Fireblast))
            ability = fireblastPrefab;
        

        // Ad infinitum..
        return ability;
    }
}