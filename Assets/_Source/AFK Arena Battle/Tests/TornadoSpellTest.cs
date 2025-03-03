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

    [Test]
    public void Test_Pull_With_Not_Moving_Character()
    {
        var spell = new TornadoSpell()
        {
            Position = new Vector2(1, 0),
            SlowValue = 0,
            PullValue = 1
        };

        var unit = new DummyUnit()
        {
            Position = new Vector2(0, 0),
            MoveDirection = new Vector2(-1, 0),
            MoveSpeed = 0
        };

        var expectedUnitPosition = new Vector2(1, 0);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    [Test]
    public void Test_Slow_Without_Pulling()
    {
        var spell = new TornadoSpell()
        {
            Position = new Vector2(1, 0),
            SlowValue = 0.3f,
            PullValue = 0
        };

        var unit = new DummyUnit()
        {
            Position = new Vector2(0, 0),
            MoveDirection = new Vector2(-1, 0),
            MoveSpeed = 1
        };

        var expectedUnitPosition = new Vector2(-0.7f, 0);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    [Test]
    public void Test_Character_Affected_By_Both_Effects()
    {
        var spell = new TornadoSpell()
        {
            Position = new Vector2(1, 0),
            SlowValue = 0.3f,
            PullValue = 1
        };

        var unit = new DummyUnit()
        {
            Position = new Vector2(0, 0),
            MoveDirection = new Vector2(-1, 0),
            MoveSpeed = 1
        };

        var expectedUnitPosition = new Vector2(0.3f, 0);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    // This method exist only to help with mental calculations of expected unit position.
    // Don't consider this as a math documentation of tornado spell.
    // GPT-sensei still thinks it's bad practice so i'll just replace it with values
    [Obsolete] 
    private Vector2 GetExpectedUnitPosition(float pullValue, float slowValue, float moveSpeed)
    {
        //var movePosition = unitPosition + moveSpeed * (1 - slowValue) * simulationTime * unitMoveDirection;
        //var pullDirection = unitPosition.GetDirectionTo(spellPosition);
        //var pullDelta = pullValue * simulationTime * pullDirection;

        //return movePosition + pullDelta;
        return Vector2.zero;
    }
}
