using System;

namespace taskIT2
{
    public class EllipseShape : Shape
    {
        public double RadiusX { get; }
        public double RadiusY { get; }

        public EllipseShape(double x, double y, double radiusX, double radiusY) : base(x, y)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        public override double GetArea() => Math.PI * RadiusX * RadiusY;

        public override (double xMin, double xMax, double yMin, double yMax) GetBoundingRectangle()
        {
            return (CenterX - RadiusX, CenterX + RadiusX, CenterY - RadiusY, CenterY + RadiusY);
        }
    }
}