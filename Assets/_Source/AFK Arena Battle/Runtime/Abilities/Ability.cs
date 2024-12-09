// TODO: Move settings to different class like Scriptable Object.
public abstract class Ability
{
    /// <summary>
    /// Shows whether skill can be used right after battle begins.
    /// </summary>
    public abstract bool IsAvailableAtTheStart { get; }
    public abstract float CastTime { get; }
    public abstract float Cooldown { get; }
    public abstract float Range { get; }
    // ^ Bad for testing omg

    public bool CanCast => cooldownTimer <= 0;
    public bool IsCasting { get; private set; }

    protected float cooldownTimer;
    protected float castTimer;

    protected Ability()
    {
        cooldownTimer = IsAvailableAtTheStart ? 0 : Cooldown;
    }

    public void Update(float deltaTime)
    {
        cooldownTimer -= deltaTime;
        HandleCastTimer(deltaTime);
        UpdateInternal(deltaTime);
    }

    public void Cast(Unit caster)
    {
        IsCasting = true;
        CastInternal(caster);
        ResetTimers();
    }

    protected abstract void CastInternal(Unit caster);

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

        castTimer -= deltaTime;

        if (castTimer <= 0)
            IsCasting = false;
    }

    private void ResetTimers()
    {
        cooldownTimer = Cooldown;
        castTimer = CastTime;
    }
}
