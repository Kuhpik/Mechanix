using UnityEngine;

/// <summary>
/// Serves for tests purposes
/// </summary>
public class DummyUnit : Unit
{
    public DummyUnit() : base("Dummy", 0, 1000) { }

    public override void Update(float deltaTime)
    {
        Move(deltaTime);
    }
}
