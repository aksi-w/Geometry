using System; 

namespace taskIT2
{
    public class LineShape : Shape
    {
        public double EndX { get; }
        public double EndY { get; }

        public LineShape(double x, double y, double endX, double endY) : base(x, y)
        {
            EndX = endX;
            EndY = endY;
        }

        public override double GetArea() => 0;

        public override (double xMin, double xMax, double yMin, double yMax) GetBoundingRectangle()
        {
            return (
                Math.Min(CenterX, EndX),
                Math.Max(CenterX, EndX),
                Math.Min(CenterY, EndY),
                Math.Max(CenterY, EndY)
            );
        }
    }
}