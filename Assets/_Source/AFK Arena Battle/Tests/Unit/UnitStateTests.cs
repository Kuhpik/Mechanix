using NUnit.Framework;
using UnityEngine;

public class UnitStateTests
{
    [Test]
    public void Unit_Is_Idle_If_Has_No_Target()
    {
        var unit = new Unit("Dummy", 10, 100);

        unit.Update(0.1f);

        Assert.AreEqual(EUnitState.Idle, unit.State);
    }

    [Test]
    public void Unit_Is_Moving_If_Target_Out_Of_Range()
    {
        var ability = new TestAbility(2, 10, true) { Range = 2 };
        var unit = new Unit("Dummy", 10, 100, ability);
        var target = new Unit("Target", 10, 100) { Position = new Vector2(3, 0) };

        unit.SetTarget(target);
        unit.Update(0.1f);

        Assert.AreEqual(EUnitState.Move, unit.State);
    }

    [Test]
    public void Unit_Is_Moveing_To_Target_When_Ability_On_Cooldown()
    {
        var ability = new TestAbility(2, 10, false) { Range = 2 };
        var unit = new Unit("Dummy", 10, 100, ability);
        var target = new Unit("Target", 10, 100) { Position = new Vector2(3, 0) };

        unit.SetTarget(target);
        unit.Update(0.1f);

        Assert.AreEqual(EUnitState.Move, unit.State);
    }

    [Test]
    public void Unit_Is_Attacking_While_Spell_Casted()
    {
        var ability = new TestAbility(2, 10, true) { Range = 2 };
        var unit = new Unit("Dummy", 10, 100, ability);
        var target = new Unit("Target", 10, 100) { Position = new Vector2(2, 0) };

        unit.SetTarget(target);

        unit.Update(0.1f);
        Assert.AreEqual(EUnitState.Attack, unit.State);

        unit.Update(0.1f);
        Assert.AreEqual(EUnitState.Attack, unit.State);

        unit.Update(0.1f);
        Assert.AreEqual(EUnitState.Attack, unit.State);

        unit.Update(0.1f);
        Assert.AreEqual(EUnitState.Attack, unit.State);

        unit.Update(0.1f);
        Assert.AreEqual(EUnitState.Attack, unit.State);

        unit.Update(1.5f);
        Assert.AreEqual(EUnitState.Attack, unit.State);
    }

    [Test]
    public void Unit_Is_Idle_When_Ability_On_Cooldown()
    {
        var ability = new TestAbility(2, 10, false) { Range = 2 };
        var unit = new Unit("Dummy", 10, 100, ability);
        var target = new Unit("Target", 10, 100) { Position = new Vector2(2, 0) };

        unit.SetTarget(target);

        unit.Update(0.1f);
        Assert.AreEqual(EUnitState.Idle, unit.State);
    }

    [Test]
    public void Unit_Is_Idle_After_Ability_Cast()
    {
        var ability = new TestAbility(2, 10, true) { Range = 2 };
        var unit = new Unit("Dummy", 10, 100, ability);
        var target = new Unit("Target", 10, 100) { Position = new Vector2(2, 0) };

        unit.SetTarget(target);

        unit.Update(0.1f);
        unit.Update(2);
        Assert.AreEqual(EUnitState.Idle, unit.State);
    }
}

