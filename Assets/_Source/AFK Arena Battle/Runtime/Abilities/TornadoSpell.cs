using System;
using UnityEngine;

public class TornadoSpell
{
    public float SlowValue { get; set; }
    public float PullValue { get; set; }
    public Vector2 Position { get; set; }

    private IUnit[] units;

    public void Apply(params IUnit[] units)
    {
        this.units = units;

        foreach (var unit in units)
        {
            unit.MoveSpeed *= (1 - SlowValue);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var unit in units)
        {
            unit.Position -= GetPullDistance(unit, deltaTime);
        }
    }

    private Vector2 GetPullDistance(IUnit unit, float deltaTime)
    {
        var direction = unit.Position - Position;
        return deltaTime * PullValue * direction.normalized;
    }
}
