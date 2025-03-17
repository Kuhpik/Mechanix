using UnityEngine;
using NUnit.Framework;

public class UnitAbilitiesTests
{
    [Test]
    public void Only_One_Ability_Can_Be_Casted_At_A_Time()
    {
        var ability1 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var ability2 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var unit = new Unit("Dummy", 10, 100, ability1, ability2);
        var targetUnit = new Unit("Target", 10, 100);

        unit.SetTarget(targetUnit);
        unit.Update(0.1f);

        Assert.AreEqual(ability1, unit.AbilityCasted);
        Assert.AreEqual(true, ability1.IsCasting);
        Assert.AreEqual(false, ability2.IsCasting);
    }

    [Test]
    public void Next_Ability_Casted_After_First()
    {
        var ability1 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var ability2 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var unit = new Unit("Dummy", 10, 100, ability1, ability2);
        var targetUnit = new Unit("Target", 10, 100);

        unit.SetTarget(targetUnit);
        unit.Update(0.1f);
        unit.Update(2);

        Assert.AreEqual(ability2, unit.AbilityCasted);
        Assert.AreEqual(false, ability1.IsCasting);
        Assert.AreEqual(true, ability2.IsCasting);
    }

    [Test]
    public void Target_Out_Of_Range_Abilities_Not_Casted()
    {
        var ability1 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var ability2 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var unit = new Unit("Dummy", 10, 100, ability1, ability2) { Position = new Vector2(15, 0) };
        var targetUnit = new Unit("Target", 10, 100);

        unit.SetTarget(targetUnit);
        unit.Update(0.1f);
        unit.Update(0.1f);
        unit.Update(0.1f);

        Assert.AreEqual(null, unit.AbilityCasted);
        Assert.AreEqual(false, ability1.IsCasting);
        Assert.AreEqual(false, ability2.IsCasting);
    }

    [Test]
    public void Target_Out_Of_Range_For_One_Ability_Another_Is_Casted()
    {
        var ability1 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var ability2 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true) { Range = 15 };
        var unit = new Unit("Dummy", 10, 100, ability1, ability2) { Position = new Vector2(15, 0) };
        var targetUnit = new Unit("Target", 10, 100);

        unit.SetTarget(targetUnit);
        unit.Update(0.1f);

        Assert.AreEqual(ability2, unit.AbilityCasted);
        Assert.AreEqual(false, ability1.IsCasting);
        Assert.AreEqual(true, ability2.IsCasting);
    }
}
