using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCard : Card
{
    public override void Trigger(LifeForm caster, LifeForm target)
    {
        target.TakeDamage(new DmgInfo(3, DmgType.Fire));
    }
}
