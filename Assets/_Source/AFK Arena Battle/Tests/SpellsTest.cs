using NUnit.Framework;

public class SpellsTest
{
    private class CommonSpell : Spell
    {
        public override bool IsAvailableAtTheStart => false;
        public CommonSpell(float cooldown) : base(cooldown) { }
        protected override void UseInternal() { }
    }

    private class AvailableAtStartSpell : Spell
    {
        public override bool IsAvailableAtTheStart => true;
        public AvailableAtStartSpell(float cooldown) : base(cooldown) { }
        protected override void UseInternal() { }
    }

    private Spell availableAtStartSpell;
    private Spell commonSpell;

    private const float cooldown = 3;

    [SetUp]
    public void Setup()
    {
        availableAtStartSpell = new AvailableAtStartSpell(cooldown);
        commonSpell = new CommonSpell(cooldown);
    }

    [Test]
    public void Common_Spell_Not_Ready_When_Created()
    {
        var spell = commonSpell;

        Assert.IsFalse(spell.CanCast);
    }

    [Test]
    public void Common_Spell_Ready_After_Timespan()
    {
        var spell = commonSpell;

        spell.Update(cooldown);

        Assert.IsTrue(spell.CanCast);
    }

    [Test]
    public void Instant_Spell_Ready_When_Created()
    {
        var spell = availableAtStartSpell;

        Assert.IsTrue(spell.CanCast);
    }

    [Test]
    public void Spell_On_Cooldown_After_Use()
    {
        var spell = availableAtStartSpell;

        spell.Use();

        Assert.IsFalse(spell.CanCast);
    }
}
