using NUnit.Framework;
using UnityEngine;

public class ExtensionsTests
{
    [Test]
    public void Vector2_Direction_From_Center_To_Right_Is_Right()
    {
        var from = Vector2.zero;
        var to = Vector2.right;
        var direction = from.GetDirectionTo(to);

        Assert.AreEqual(Vector2.right, direction);
    }

    [Test]
    public void Vector2_Direction_Always_Normalized()
    {
        var from = Vector2.zero;
        var to = Vector2.right * 1000;
        var direction = from.GetDirectionTo(to);

        Assert.AreEqual(Vector2.right, direction);
    }
}
