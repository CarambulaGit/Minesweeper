using System.Collections.Generic;
using CodeBase.Infrastructure.Logic;
using NUnit.Framework;

public class TestPoint {
    private static readonly Point[] Points = {new(0, 0), new(3, 3), new(1, 1), new(4, 2)};

    [Test]
    public void TestFindingNeighbors([ValueSource(nameof(Points))] Point point) {
        var result = point.FindNeighbors();
        Assert.IsTrue(result.Count == 8);
        var correctResult = new List<Point> {
            new(point.Y - 1, point.X - 1),
            new(point.Y, point.X - 1),
            new(point.Y + 1, point.X - 1),
            new(point.Y - 1, point.X + 1),
            new(point.Y, point.X + 1),
            new(point.Y + 1, point.X + 1),
            new(point.Y - 1, point.X),
            new(point.Y + 1, point.X),
        };
        Assert.IsTrue(correctResult.HasSameContent(result));
    }
}