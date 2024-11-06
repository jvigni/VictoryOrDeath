using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Team
{
    Humans,
    Plague,
    GAIA
}

[Serializable]
public class LifeForm : MonoBehaviour
{
    [SerializeField] public Team Team;
    [SerializeField] int MaxHealth;
    [SerializeField] DmgMarker dmgMarker;
    int originalMaxHealth;

    [Space]
    private int Health;
    public event Action OnDeath;
    public event Action<int> OnDamageTaken;

    public List<Effect> Effects { get; private set; } // should not be public but compiler vult?
    public event Action<Effect> OnEffectApplied;
    public event Action<Effect> OnEffectRemoved;    
    public bool IsAlive { get; private set; }
    

    private void Start()
    {
        ResetToDefault();
    }

    public void ResetToDefault()
    {
        IsAlive = true;
        originalMaxHealth = MaxHealth;
        Health = MaxHealth;
        Effects = new List<Effect>();
    }

    public void RestoreAllHealth()
    {
        Health = MaxHealth;
    }

    public int TakeDamage(DmgInfo dmgInfo)
    {
        foreach (Effect effect in Effects.ToList())
            effect.OnBeingAttacked(dmgInfo);

        foreach (Effect effect in Effects.ToList())
            dmgInfo = effect.ModifyIncomingDamage(dmgInfo);

        foreach (Effect effect in Effects.ToList())
            dmgInfo = effect.ModifyIncomingDamage2(dmgInfo);

        foreach (Effect effect in Effects.ToList())
            effect.OnDamageReceibed(dmgInfo);

        Health -= dmgInfo.Amount;

        //Provider.VFXManager.Play(VFX.Explosion1, GetPosition());
        //Provider.VFXManager.ShowHitAlert(dmgInfo, GetPosition());

        //string textMsg = dmgInfo.Amount > 0 ? dmgInfo.Amount.ToString() : "-";
        //Color textColor = dmgInfo.Amount > 0 ? Color.red : Color.gray;
        //Provider.FloatingTextManager.PrintOnPosition(textMsg, textColor, GetPosition());

        dmgMarker.Show(dmgInfo.Amount);

        OnDamageTaken?.Invoke(dmgInfo.Amount);

        if (Health <= 0)
            Death();

        return dmgInfo.Amount;
    }

    public void ApplyEffect(Effect originalEffect, LifeForm caster)
    {
        Effect effectClone = originalEffect.DeepClone();
        var existingEffect = GetEffect(effectClone.ID);

        if (effectClone.BehaviourType == EffectBehaviour.Independent)
            ApplyNewEffect(effectClone, caster);

        if (effectClone.BehaviourType == EffectBehaviour.Stackeable)
        {
            if (existingEffect != null)
                existingEffect.OnStack(effectClone);
            else
                ApplyNewEffect(effectClone, caster);
        }

        if (effectClone.BehaviourType == EffectBehaviour.Unique)
        {
            if (existingEffect != null)
                RemoveEffect(effectClone.ID);

            ApplyNewEffect(effectClone, caster);
        }
    }

    private void ApplyNewEffect(Effect effect, LifeForm caster)
    {
        Effects.Add(effect);
        effect.Owner = this;
        effect.Caster = caster;
        effect.OnExpires += RemoveEffect;

        //Provider.Combat.View.ApplyEffect(effect, ID);
        //OnEffectApplied?.Invoke(effect);

        //Debug.Log($"<color=orange>{effect} Applied</color>");
    }

    public bool HasEffect(EffectID id)
    {
        foreach (Effect effect in Effects)
        {
            if (effect.ID == id)
                return true;
        }
        return false;
    }

    public void IncreaseHealth(int amount)
    {
        if (HasEffect(EffectID.Poison))
            amount = 0;

        if (Health + amount > MaxHealth)
            Health = MaxHealth;
        else
            Health += amount;

        //if (amount > 0)
        //    Provider.FloatingTextManager.PrintOnPosition($"{amount}", Color.green, GetPosition());
    }

    public void ExecutePostStunEffects()
    {
        foreach (Effect effect in Effects.ToList())
            effect.OnPostStun();
    }

    private void Death()
    {
        //Debug.Log("Monster death");
        IsAlive = false;

        foreach (Effect effect in Effects.ToList())
            effect.Expire();

        OnDeath?.Invoke();
    }

    public void RemoveRandomEffect(EffectType effectType)
    {
        var desiredEffect = Effects.Where(effect => effect.Type == effectType).FirstOrDefault();
        if (desiredEffect != null)
        {
            //Debug.Log($"Removing effect: {desiredEffect.ID}");
            RemoveEffect(desiredEffect);
        }
    }

    public T GetEffect<T>() where T : Effect
    {
        return (T)Effects.Where(effect => effect is T).FirstOrDefault();
    }

    public Effect GetEffect(EffectID id)
    {
        return Effects.Where(effect => effect.ID == id).FirstOrDefault();
    }

    public void RemoveAllEffects()
    {
        foreach (Effect effect in Effects.ToList())
            RemoveEffect(effect);
    }

    public void RemoveEffect(EffectID id)
    {
        var desiredEffect = Effects.Where(effect => effect.ID == id).FirstOrDefault();
        if (desiredEffect != null)
        {
            //Debug.Log($"Removing effect: {desiredEffect.ID}");
            RemoveEffect(desiredEffect);
        }
    }

    private void RemoveEffect(Effect effect)
    {
        OnEffectRemoved?.Invoke(effect);
        Effects.Remove(effect);

        //Provider.Combat.View.RemoveEffect(effect, ID);
    }
}