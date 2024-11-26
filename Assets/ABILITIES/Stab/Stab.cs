using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : Ability
{
    [SerializeField] DmgInfo dmgType;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Trigger(gameObject, Provider.Targeter.CurrentTarget);
    }

    public override void Trigger(GameObject caster, GameObject target)
    {
        Debug.Log("STABING " + target.name);

        var lifeform = target.GetComponent<LifeForm>();
        if (lifeform != null)
            lifeform.TakeDamage(dmgType);

        Provider.RodlakAnimator.SetTrigger("Stab");
    }
}
