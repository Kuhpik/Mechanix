public class TestAbility : Ability
{
    public TestAbility(float castTime, float cooldown, bool isAvailableAtTheStart)
        : base(castTime, cooldown, isAvailableAtTheStart) { }
    protected override void CastInternal(IUnit caster) { }
}
