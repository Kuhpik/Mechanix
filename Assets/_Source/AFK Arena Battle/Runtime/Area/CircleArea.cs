using UnityEngine;

public class CircleArea : IArea
{
    private float radius;
    private Vector2 center;

    public CircleArea(float radius, Vector2 center)
    {
        this.radius = radius;
        this.center = center;
    }

    public bool IsPointInside(Vector2 point)
    {
        return false;
    }
}
