using System;

namespace taskIT2
{
    public abstract class Shape
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public Shape(double x, double y)
        {
            CenterX = x;
            CenterY = y;
        }

        public abstract double GetArea();
        public abstract (double xMin, double xMax, double yMin, double yMax) GetBoundingRectangle();
    }
}
