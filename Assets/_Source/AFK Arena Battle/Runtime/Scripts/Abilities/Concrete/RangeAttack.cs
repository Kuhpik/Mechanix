using UnityEngine;

public class RangeAttack : Ability
{
    public RangeAttack(float castTime, float cooldown, bool isAvailableAtTheStart) : 
        base(castTime, cooldown, isAvailableAtTheStart)
    {
        Range = Mathf.Infinity;
    }

    protected override void CastInternal(IUnit caster)
    {
        var damage = caster.Damage;
        var target = caster.Target;

        target.ApplyDamage(caster, damage);
    }
}
