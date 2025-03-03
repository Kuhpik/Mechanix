using System;
using UnityEngine;

public class TornadoSpell
{
    [Serializable]
    public class Settings
    {
        public float SlowValue;
        public float PullValue;
    }

    public Vector2 Position { get; set; }
    public Settings Values  { get; set; }

    private IUnit[] units;

    public void Apply(params IUnit[] units)
    {
        this.units = units;

        foreach (var unit in units)
        {
            unit.MoveSpeed *= (1 - Values.SlowValue);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var unit in units)
        {
            unit.Position -= GetPullDistance(unit, deltaTime);
        }
    }

    public Vector2 GetPullDistance(IUnit unit, float deltaTime)
    {
        var direction = unit.Position - Position;
        return deltaTime * Values.PullValue * direction.normalized;
    }
}
