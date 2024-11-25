using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] TabTargeter targeter;
    [SerializeField] int dmg;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
            //Shoot(new DmgInfo(dmg, DmgType.Fire));
    }
    /*
    public void Shoot(DmgInfo dmgInfo)
    {
        GameObject target = targeter.CurrentObjective;
        if (target != null)
            target.TakeDamage(dmgInfo/*, gameObject); // todo??
    }*/
}
