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
    public int minDmg;
    public int maxDmg;
    public DmgType Type;
    public bool Unbloqueable;
    public bool Pure;
    public int DamagePreventedCounter;
    public bool LifeSteal;

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

    public int Damage()
    {
        return UnityEngine.Random.Range(minDmg, maxDmg);
    }
}