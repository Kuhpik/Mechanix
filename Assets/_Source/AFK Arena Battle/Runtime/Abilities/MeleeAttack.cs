//public class MeleeAttack : Ability
//{
//    public MeleeAttack(float castTime, float cooldown, bool isAvailableAtTheStart) : base(castTime, cooldown, isAvailableAtTheStart)
//    {
//    }

//    //public override bool IsAvailableAtTheStart => true;
//    //public override float CastTime => 0.25f;
//    //public override float Cooldown => 1;
//    //public override float Range => 1;

//    protected override void CastInternal(IUnit caster)
//    {
//        var damage = caster.Damage;
//        var target = caster.Target;

//        target.ApplyDamage(caster, damage);
//    }
//}
