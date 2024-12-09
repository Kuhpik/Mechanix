using System.Linq;
using UnityEngine;

public class Heal : Ability
{
    public override bool IsAvailableAtTheStart => false;
    public override float CastTime => 0;
    public override float Cooldown => 3;
    public override float Range => float.MaxValue;

    protected override void CastInternal(Unit caster)
    {
        var target = caster.Team.GetMembers().OrderBy(x => x.Health).First();
        var healAmount = Mathf.FloorToInt(target.MaxHealth * 0.5f);

        target.ApplyHeal(healAmount);
    }
}
