using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : Ability
{
    [SerializeField] DmgInfo dmgType;

    public override void Trigger(GameObject caster, GameObject target)
    {
        var lifeform = target.GetComponent<LifeForm>();
        if (lifeform != null)
            lifeform.TakeDamage(dmgType);

        Provider.RodlakAnimator.SetTrigger("Stab");
    }
}
