public class BlinkBehind : Ability
{
    public override bool IsAvailableAtTheStart => true;
    public override float CastTime => 0;
    public override float Cooldown => 10;
    public override float Range => float.MaxValue;

    protected override void CastInternal()
    {
        // Select closest target.
        // Caster tp's right to the target's back
    }
}
