using Moq;
using UnityEngine;
using NUnit.Framework;

public class AbilityTests
{
    [Test]
    public void Ability_On_Cooldown_When_Battle_Started()
    {
        var ability = new TestAbility(castTime: 0, cooldown: 10, isAvailableAtTheStart: false);

        Assert.False(ability.CanCast);
    }

    [Test]
    public void Ability_Ready_When_Battle_Started_If_Marked_As_Available()
    {
        var ability = new TestAbility(castTime: 0, cooldown: 10, isAvailableAtTheStart: true);

        Assert.True(ability.CanCast);
    }

    [Test]
    public void Ability_On_Cooldown_If_Not_Enough_Time_Passed()
    {
        var ability = new TestAbility(castTime: 0, cooldown: 10, isAvailableAtTheStart: false);

        ability.Update(9.9f);

        Assert.False(ability.CanCast);
    }

    [Test]
    public void Ability_Ready_After_Time_Passed()
    {
        var ability = new TestAbility(castTime: 0, cooldown: 10, isAvailableAtTheStart: false);

        ability.Update(10);

        Assert.True(ability.CanCast);
    }

    [Test]
    public void Ability_Is_Not_Casting_When_On_Cooldown()
    {
        var ability = new TestAbility(castTime: 0, cooldown: 10, isAvailableAtTheStart: false);
        var unitMock = new Mock<IUnit>();
        var unit = unitMock.Object;

        ability.Cast(unit);

        Assert.False(ability.IsCasting);
    }

    [Test]
    public void Ability_Casting_When_Ready()
    {
        var ability = new TestAbility(castTime: 1, cooldown: 10, isAvailableAtTheStart: true);
        var unitMock = new Mock<IUnit>();
        var unit = unitMock.Object;

        ability.Cast(unit);

        Assert.True(ability.IsCasting);
    }

    [Test]
    public void Ability_Still_Casting_If_Not_Enough_Time_Passed()
    {
        var ability = new TestAbility(castTime: 1, cooldown: 10, isAvailableAtTheStart: true);
        var unitMock = new Mock<IUnit>();
        var unit = unitMock.Object;

        ability.Cast(unit);
        ability.Update(0.99f);

        Assert.True(ability.IsCasting);
    }

    [Test]
    public void Ability_Cast_Over_If_Cast_Time_Passed()
    {
        var ability = new TestAbility(castTime: 1, cooldown: 10, isAvailableAtTheStart: true);
        var unitMock = new Mock<IUnit>();
        var unit = unitMock.Object;

        ability.Cast(unit);
        ability.Update(1);

        Assert.False(ability.IsCasting);
    }

    [Test]
    public void Ability_Cooldown_Reduces_While_Casting()
    {
        var ability = new TestAbility(castTime: 5, cooldown: 10, isAvailableAtTheStart: true);
        var unitMock = new Mock<IUnit>();
        var unit = unitMock.Object;

        ability.Cast(unit);
        ability.Update(5);

        Assert.AreEqual(5, ability.CurrentCooldown);
    }

    [Test]
    public void Ability_Cast_Timer_Reduces_While_Casting()
    {
        var ability = new TestAbility(castTime: 5, cooldown: 10, isAvailableAtTheStart: true);
        var unitMock = new Mock<IUnit>();
        var unit = unitMock.Object;

        ability.Cast(unit);
        ability.Update(4);

        Assert.AreEqual(1, ability.CurrentCastTime);
    }

    [Test]
    public void Target_In_Range()
    {
        var ability = new TestAbility(castTime: 5, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var isInRange = ability.IsTargetInRange(new Vector2(0, 0), new Vector2(10, 0));

        Assert.True(isInRange);
    }

    [Test]
    public void Target_Out_Of_Range()
    {
        var ability = new TestAbility(castTime: 5, cooldown: 10, isAvailableAtTheStart: true) { Range = 10 };
        var isInRange = ability.IsTargetInRange(new Vector2(0, 0), new Vector2(11, 0));

        Assert.False(isInRange);
    }
}
