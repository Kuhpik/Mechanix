using UnityEngine;
using NUnit.Framework;
using System;

// Documentation:
// Spell creates Tornado that
// - Slows characters affected by it for X%
// - Also pulling them to its center with speed of N
public class TornadoSpellTest
{
    private const float simulationTime = 1;
    private Vector2 spellPosition = Vector2.right;
    private Vector2 unitPosition = Vector2.zero;
    private Vector2 unitMoveDirection = Vector2.left;

    [Test]
    public void Test_Pull_With_Not_Moving_Character()
    {
        var slowValue = 0.5f;
        var pullValue = 1;
        var moveSpeed = 0;
        var spell = GetSpell(pullValue, slowValue);
        var unit = GetUnit(moveSpeed);
        var expectedUnitPosition = GetExpectedUnitPosition(pullValue, slowValue, moveSpeed);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    [Test]
    public void Test_Slow_Without_Pulling()
    {
        var slowValue = 0.3f;
        var pullValue = 0;
        var moveSpeed = 1;
        var spell = GetSpell(pullValue, slowValue);
        var unit = GetUnit(moveSpeed);
        var expectedUnitPosition = GetExpectedUnitPosition(pullValue, slowValue, moveSpeed);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    [Test]
    public void Test_Character_Affected_By_Both_Effects()
    {
        var slowValue = 0.3f;
        var pullValue = 1;
        var moveSpeed = 1;
        var spell = GetSpell(pullValue, slowValue);
        var unit = GetUnit(moveSpeed);
        var expectedUnitPosition = GetExpectedUnitPosition(pullValue, slowValue, moveSpeed);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    // This method exist only to help with mental calculations of expected unit position.
    // Don't consider this as a math documentation of tornado spell.
    private Vector2 GetExpectedUnitPosition(float pullValue, float slowValue, float moveSpeed)
    {
        var movePosition = unitPosition + moveSpeed * (1 - slowValue) * simulationTime * unitMoveDirection;
        var pullDirection = unitPosition.GetDirectionTo(spellPosition);
        var pullDelta = pullValue * simulationTime * pullDirection;

        return movePosition + pullDelta;
    }

    private TornadoSpell GetSpell(float pullValue, float slowValue)
    {
        return new TornadoSpell
        {
            Position = spellPosition,
            Values = new TornadoSpell.Settings { PullValue = pullValue, SlowValue = slowValue }
        };
    }

    private IUnit GetUnit(float moveSpeed)
    {
        return new DummyUnit
        {
            MoveSpeed = moveSpeed,
            Position = unitPosition,
            MoveDirection = unitMoveDirection
        };
    }
}
