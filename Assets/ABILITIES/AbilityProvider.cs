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

    public Ability GetAbilityClone(AbilityCode code)
    {
        if (code.Equals(AbilityCode.Fireball))
            return fireball.DeepClone();

        if (code.Equals(AbilityCode.Fireblast))
            return fireblast.DeepClone();

        

        // Ad infinitum..
        return null;
    }
}