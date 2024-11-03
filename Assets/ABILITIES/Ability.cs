using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityCode Code;
    public string Name;
    public Sprite Icon;


    [Space]
    public float castTime;
    public Action OnFinishCasting;

    //[SerializeField] float Cooldown;
    //[SerializeField] float castTime;
    // TODO GCD???
    //bool cooldownReady; //?    

    public void Cast(AbilityCode code, GameObject target)
    {
        StartCoroutine(Cast2(target));
    }

    IEnumerator Cast2(GameObject target)
    {
        yield return new WaitForSeconds(castTime);
        Trigger(gameObject, target);
    }

    public abstract void Trigger(GameObject caster, GameObject target);

}