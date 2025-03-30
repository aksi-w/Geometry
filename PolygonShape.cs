using System;
using System.Linq; 
namespace taskIT2
{
    public class PolygonShape : Shape
{
    public (double X, double Y)[] Points { get; }

    public PolygonShape(double x, double y, (double X, double Y)[] points) : base(x, y)
    {
        Points = points;
    }

    public override double GetArea()
    {
        double area = 0;
        for (int i = 0; i < Points.Length; i++)
        {
            var p1 = Points[i];
            var p2 = Points[(i + 1) % Points.Length];
            area += (p1.X * p2.Y) - (p2.X * p1.Y);
        }
        return Math.Abs(area) / 2;
    }

    public override (double xMin, double xMax, double yMin, double yMax) GetBoundingRectangle()
        => (Points.Min(p => p.X), Points.Max(p => p.X),
           Points.Min(p => p.Y), Points.Max(p => p.Y));
}
}