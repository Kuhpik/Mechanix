public class Heal : Ability
{
    public override bool IsAvailableAtTheStart => false;
    public override float CastTime => 0;
    public override float Cooldown => 3;
    public override float Range => float.MaxValue;

    protected override void CastInternal()
    {
        // Select ally with less health.
        // Heal.
    }
}
