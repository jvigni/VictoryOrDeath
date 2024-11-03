using System;
using UnityEngine;

public enum DmgType
{
    Fire,
    Frost,
    Holy,
    Shadow,
    Nature,
    Arcane,
    Melee,
    Ranged
}

[Serializable]
public struct DmgInfo
{
    public int Amount;
    public DmgType Type;
    public bool Unbloqueable;
    public bool Pure;
    public int DamagePreventedCounter;
    public bool LifeSteal;

    public DmgInfo(float amount, DmgType type)
    {
        Amount = Mathf.FloorToInt(amount);
        Type = type;
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