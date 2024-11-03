using System;
using UniRx;
using UnityEngine;

public enum EffectBehaviour
{
    Unique, // If another one exists, gets replaced
    Stackeable, // If another one exists, will merge and upgrade the first
    Independent, // If another one exists, it dont cares. works as another same effect
}

public enum EffectType
{
    Buff,
    Debuff
}

public enum EffectID
{
    Stun,
    Bleed,
    JudgementOfLight,
    Weak,
    SpikeArmor,
    Rage,
    Frozen,
    Block,
    Curse,
    Scorch,
    FireWard,
    Armor,
    VampiricTouch,
    Strength,
    Renew,
    ThunderStorm,
    IceBarrier,
    StoneSkin,
    Poison,
    Endurance,
    BloodTaste,
    Wound,
    FireAura
}

[Serializable]
public abstract class Effect
{
    public int Charges { get; protected set; }
    public Action<int> OnChargerChange;

    public EffectID ID;
    public LifeForm Owner;
    public LifeForm Caster;
    public event Action<Effect> OnExpires;
    public EffectType Type;
    public EffectBehaviour BehaviourType { get; protected set; }
    public string Description;

    private bool ticksOnTurnStart;
    private bool ticksOnTurnEnd;
    private int ticksDelayCountdown;

    public Effect(EffectID id, string desc, int charges, EffectType type, EffectBehaviour behaviourType)
    {
        ID = id;
        Type = type;
        Description = desc;
        BehaviourType = behaviourType;
        Charges = charges;
    }

    public void Expire()
    {
        DoOnExpire();
        OnExpires?.Invoke(this);
    }

    protected void Tick()
    {
        Charges--;
        OnChargerChange?.Invoke(Charges);
        if (Charges == 0)
            Expire();
    }

    protected void SetTickOnTurnStart(int delay = 0)
    {
        ticksOnTurnStart = true;
        ticksDelayCountdown = delay;
    }

    protected void SetTickOnTurnEnd(int delay = 0)
    {
        ticksOnTurnEnd = true;
        ticksDelayCountdown = delay;
    }

    public void OnStack(Effect anotherEffect)
    {
        Charges += anotherEffect.Charges;
        DoOnStack(anotherEffect);
    }

    public virtual void DoOnExpire() { }
    public virtual void DoOnStack(Effect anotherEffect) { }
    public virtual void OnBeingAttacked(DmgInfo dmgInfo) { }
    public virtual void OnDamageReceibed(DmgInfo dmgInfo) { }
    public virtual void OnDamageDealt(int dmgAmount) { }
    public virtual DmgInfo ModifyOutgoingDamage(DmgInfo originalDmgInfo) { return originalDmgInfo; }
    public virtual DmgInfo ModifyIncomingDamage2(DmgInfo originalDmgInfo) { return originalDmgInfo; }
    public virtual DmgInfo ModifyIncomingDamage(DmgInfo originalDmgInfo) { return originalDmgInfo; }
    public virtual void OnPostStun() { } // Ya que el stun tiene parte de su logica en el Combat, necesita un estado especifico


    protected virtual void DoOnTurnStart() { }
    protected virtual void DoOnTurnEnd() { }
}