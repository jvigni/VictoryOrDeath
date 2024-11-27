using System;
using UnityEngine;

public class Fireblast : Ability
{
    [SerializeField] DmgInfo dmg;
    [SerializeField] ParticleSystem fireFX;

    public override void Trigger(GameObject caster, GameObject target)
    {
        Instantiate(fireFX, target.transform.position, Quaternion.identity);
        target.GetComponent<LifeForm>().TakeDamage(dmg);
    }
}
