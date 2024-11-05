using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] LifeForm Target;
    [SerializeField] int dmg;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot(Target, new DmgInfo(dmg, DmgType.Fire));
    }

    public void Shoot(LifeForm target, DmgInfo dmgInfo)
    {
        target.TakeDamage(dmgInfo);
    }
}
