using UnityEngine;

public abstract class Ability
{
    public bool IsAvailableAtTheStart { get; set; }
    public float CastTime { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }

    public float CurrentCooldown { get; private set; }
    public float CurrentCastTime { get; private set; }
    public bool CanCast => CurrentCooldown <= 0;
    public bool IsCasting => CurrentCastTime > 0;

    protected Ability(float castTime, float cooldown, bool isAvailableAtTheStart)
    {
        CastTime = castTime;
        Cooldown = cooldown;
        IsAvailableAtTheStart = isAvailableAtTheStart;

        CurrentCooldown = IsAvailableAtTheStart ? 0 : Cooldown;
    }

    public void Update(float deltaTime)
    {
        CurrentCooldown -= deltaTime;
        HandleCastTimer(deltaTime);
        UpdateInternal(deltaTime);
    }

    public void Cast(IUnit caster)
    {
        if (!CanCast)
        {
            return;
        }

        CastInternal(caster);
        ResetTimers();
    }

    public bool IsTargetInRange(Vector2 caster, Vector2 target)
    {
        return Vector2.Distance(caster, target) <= Range;
    }

    protected abstract void CastInternal(IUnit caster);

    /// <summary>
    /// For cases such as projectile fly animation
    /// </summary>
    protected virtual void UpdateInternal(float deltaTime)
    {

    }

    private void HandleCastTimer(float deltaTime)
    {
        if (!IsCasting)
            return;

        CurrentCastTime -= deltaTime;
    }

    private void ResetTimers()
    {
        CurrentCooldown = Cooldown;
        CurrentCastTime = CastTime;
    }
}
