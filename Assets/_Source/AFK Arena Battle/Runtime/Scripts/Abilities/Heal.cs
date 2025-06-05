using System.Linq;
using UnityEngine;

public class Heal : Ability
{
    public Heal(float castTime, float cooldown, bool isAvailableAtTheStart) :
        base(castTime, cooldown, isAvailableAtTheStart)
    {
        Range = float.MaxValue;
    }

    protected override void CastInternal(IUnit caster)
    {
        var target = caster.Team.GetMembers().OrderBy(x => x.Health).First();
        var healAmount = Mathf.FloorToInt(target.MaxHealth * 0.5f);

        target.ApplyDamage(caster, -healAmount);
    }
}
