using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 GetDirectionToNormalized(this Vector2 from, Vector2 to)
    {
        return (to - from).normalized;
    }
}
