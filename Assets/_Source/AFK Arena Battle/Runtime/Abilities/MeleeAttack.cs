public class MeleeAttack : Ability
{
    public MeleeAttack(float castTime, float cooldown, bool isAvailableAtTheStart) : 
        base(castTime, cooldown, isAvailableAtTheStart)
    {
        Range = 1;
    }

    protected override void CastInternal(IUnit caster)
    {
        var target = caster.Target;

        target.ApplyDamage(caster, caster.Damage);
    }
}
