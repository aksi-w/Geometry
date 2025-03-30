namespace taskIT2
{
    public class PointShape : Shape
    {
        public PointShape(double x, double y) : base(x, y) { }

        public override double GetArea() => 0;

        public override (double xMin, double xMax, double yMin, double yMax) GetBoundingRectangle()
            => (CenterX, CenterX, CenterY, CenterY); 
    }
}