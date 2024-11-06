using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] TabTargeter targeter;
    [SerializeField] int dmg;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot(new DmgInfo(dmg, DmgType.Fire));
    }

    public void Shoot(DmgInfo dmgInfo)
    {
        var target = targeter.CurrentObjective;
        if (target != null)
            target.TakeDamage(dmgInfo);
    }
}
