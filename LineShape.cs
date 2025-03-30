namespace taskIT2
{
    public class LineShape : Shape
    {
        public double EndX { get; set; }
        public double EndY { get; set; }

        public LineShape(double x, double y, double endX, double endY) : base(x, y)
        {
            EndX = endX;
            EndY = endY;
        }

        public override double GetArea() => 0;

        public override (double xMin, double xMax, double yMin, double yMax) GetBoundingRectangle()
        {
            return (System.Math.Min(CenterX, EndX), System.Math.Max(CenterX, EndX),
                    System.Math.Min(CenterY, EndY), System.Math.Max(CenterY, EndY));
        }
    }
}
