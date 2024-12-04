// TODO: Not implemented. Move spell settings to different class like Scriptable Object.
public abstract class Spell
{
    /// <summary>
    /// Shows whether skill can be used right after battle begins. Already cooled down.
    /// </summary>
    public abstract bool IsAvailableAtTheStart { get; }
    public readonly float Cooldown;

    public bool CanCast => currentCooldown <= 0;

    protected float currentCooldown;

    protected Spell(float cooldown)
    {
        Cooldown = cooldown;
        currentCooldown = IsAvailableAtTheStart ? 0 : cooldown;
    }

    public void Update(float deltaTime)
    {
        currentCooldown -= deltaTime;
    }

    public void Use()
    {
        StartCooldown();
        UseInternal();
    }

    protected abstract void UseInternal();

    private void StartCooldown()
    {
        currentCooldown = Cooldown;
    }
}
