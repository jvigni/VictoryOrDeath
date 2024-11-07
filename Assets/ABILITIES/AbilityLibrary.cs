using UnityEngine;

public enum AbilityCode
{
    Fireball,
    Fireblast,
}

public class AbilityLibrary : MonoBehaviour
{
    [SerializeField] Ability fireballPrefab;
    [SerializeField] Ability fireblastPrefab;

    public Ability GetAbilityClone(AbilityCode code)
    {
        var abilityPrefab = GetAbilityPrefab(code);
        var abilityClone = Instantiate(abilityPrefab, transform.position, Quaternion.identity);
        return abilityClone;
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