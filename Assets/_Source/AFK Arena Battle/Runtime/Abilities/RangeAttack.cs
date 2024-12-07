public class RangeAttack : Ability
{
    public override bool IsAvailableAtTheStart => true;
    public override float CastTime => 0.1f;
    public override float Cooldown => 1;
    public override float Range => float.MaxValue;

    protected override void CastInternal()
    {
        // Hit unit's target
    }
}
