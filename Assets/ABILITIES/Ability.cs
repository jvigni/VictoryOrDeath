using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityCode Code;
    public string Name;
    public Sprite Icon;
    public Action OnFinishCasting;

    [Space]
    [SerializeField] float Cooldown;
    [SerializeField] float castTime;
    bool cooldownReady; //?    

    public void Cast(AbilityCode code)
    {
        StartCoroutine(Cast2());
    }

    IEnumerator Cast2()
    {

    }

    public abstract void Trigger(GameObject caster, GameObject target);

}