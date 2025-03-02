using UnityEngine;
using NUnit.Framework;

// Documentation:
// Spell creates Tornado that
// - Slows characters affected by it for X%
// - Also pulling them to its center with speed of N
// ...
// Spell tests is more like integration tests
// ...
// TODO: Maybe it's all stupid and we need to create just 2 effects. Pull and Slow.
public class TornadoSpellTest
{
    [Test]
    public void Character_Not_Moving_Only_Being_Sucked_In()
    {
        var simulationTime = 1f;
        var spell = GetSpell(Vector2.right, 1, 0.5f);
        var unit = GetUnit(Vector2.zero, Vector2.left, 0);
        var expectedUnitPosition = GetExpectedPosition(unit, spell, simulationTime);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    [Test]
    public void Character_Moving_And_Pulled()
    {
        var simulationTime = 1f;
        var spell = GetSpell(Vector2.right, 1, 0.5f);
        var unit = GetUnit(Vector2.zero, Vector2.left, 1);
        var expectedUnitPosition = GetExpectedPosition(unit, spell, simulationTime);

        spell.Apply(unit);
        spell.Update(simulationTime);
        unit.Update(simulationTime);

        Assert.AreEqual(expectedUnitPosition, unit.Position);
    }

    // TODO: Re-check math in visuals
    private Vector2 GetExpectedPosition(IUnit unit, TornadoSpell spell, float simulationTime)
    {
        var movePosition = unit.Position + unit.GetMoveDistance(simulationTime);
        var pullDistance = spell.GetPullDistance(unit, simulationTime);
        return movePosition - pullDistance;
    }

    private TornadoSpell GetSpell(Vector2 position, float pullValue, float slowValue)
    {
        return new TornadoSpell
        {
            Position = Vector2.right,
            Values = new TornadoSpell.Settings { PullValue = 1f, SlowValue = 0.5f }
        };
    }

    private IUnit GetUnit(Vector2 position, Vector2 moveDirection, float moveSpeed)
    {
        return new DummyUnit
        {
            MoveSpeed = moveSpeed,
            Position = position,
            MoveDirection = moveDirection
        };
    }
}
