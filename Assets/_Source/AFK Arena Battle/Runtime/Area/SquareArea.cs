using UnityEngine;

public class SquareArea : IArea
{
    private float length;
    private Vector2 center;

    public SquareArea(float length, Vector2 center)
    {
        this.length = length;
        this.center = center;
    }

    public bool IsPointInside(Vector2 point)
    {
        var half = length / 2;
        var X1 = center.x + half;
        var Y1 = center.y + half;
        var X2 = center.x - half;
        var Y2 = center.y - half;

        return point.x <= X1 && point.x >= X2 && point.y <= Y1 && point.y >= Y2;
    }
}
