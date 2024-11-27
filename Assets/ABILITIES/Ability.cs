using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public Sprite Icon; 
    public string NAME;
    public string DESCRIPTION;
    public DmgType Type;

    [Space]
    public float castSeconds;
    public Action OnFinishCasting;

    //[SerializeField] float Cooldown;
    //[SerializeField] float castTime;
    // TODO GCD???
    //bool cooldownReady; //?    

    public void Cast(GameObject target)
    {
        StartCoroutine(Cast2(target));
    }

    IEnumerator Cast2(GameObject target)
    {
        yield return new WaitForSeconds(castSeconds);
        OnFinishCasting?.Invoke();
        Trigger(gameObject, target);
    }

    public abstract void Trigger(GameObject caster, GameObject target);

}