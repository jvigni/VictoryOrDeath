using System;
using UnityEngine;

[Serializable]
public struct DmgInfo
{
    public int Amount;
    public bool Unbloqueable;
    public bool Pure;
    public int DamagePreventedCounter;
    public bool LifeSteal;

    public DmgInfo(float amount)
    {
        Amount = Mathf.FloorToInt(amount);
        Unbloqueable = false;
        Pure = false;
        DamagePreventedCounter = 0;
        LifeSteal = false;
    }

    public DmgInfo IsUnbloqueable()
    {
        Unbloqueable = true;
        return this;
    }

    public DmgInfo IsPure()
    {
        Pure = true;
        return this;
    }
}