using NUnit.Framework;
using UnityEngine;

public class AreaTests
{
    private IArea area;

    [SetUp]
    public void Setup()
    {
        area = new SquareArea(2, Vector2.zero);
    }

    [Test]
    public void Point_On_Edge_Is_In_Area()
    {
        Vector2[] points = new Vector2[] 
        { 
            new(1, 1), new(-1, -1), new(1, 0.5f), new(-0.5f, -1) 
        };

        foreach (var point in points)
        {
            Assert.IsTrue(area.IsPointInside(point));
        }
    }

    [Test]
    public void Point_Inside_Is_In_Area()
    {
        Vector2[] points = new Vector2[]
        {
            new(0.5f, 0.5f), new(0, 0), new(-0.5f, -0.5f)
        };

        foreach (var point in points)
        {
            Assert.IsTrue(area.IsPointInside(point));
        }
    }

    [Test]
    public void Point_Outside_Not_In_Area()
    {
        Vector2[] points = new Vector2[]
        {
            new(2, 2), new(-2, -2), new(10, 999), new(999, -10), new(1.001f, 1.001f), new(-1.001f, -1.001f)
        };

        foreach (var point in points)
        {
            Assert.IsFalse(area.IsPointInside(point));
        }
    }
}
