public class Fireball : Ability
{
    public override bool IsAvailableAtTheStart => false;
    public override float CastTime => 0.5f;
    public override float Cooldown => 5;
    public override float Range => float.MaxValue;

    protected override void CastInternal(Unit caster)
    {
        // Select enemy with most health
        // Create fireball
        // Fireball moves to point where enemy was in the moment of cast
        // When point reached it's explodes causing AoE damage
    }
}
