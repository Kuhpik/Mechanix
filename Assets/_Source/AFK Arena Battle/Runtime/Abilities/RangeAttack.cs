//public class RangeAttack : Ability
//{
//    public RangeAttack(float castTime, float cooldown, bool isAvailableAtTheStart) : base(castTime, cooldown, isAvailableAtTheStart)
//    {
//    }

//    //public override bool IsAvailableAtTheStart => true;
//    //public override float CastTime => 0.25f;
//    //public override float Cooldown => 5;
//    //public override float Range => float.MaxValue;

//    protected override void CastInternal(Unit caster)
//    {
//        var damage = caster.Damage;
//        var target = caster.Target;

//        target.ApplyDamage(caster, damage);
//    }
//}
