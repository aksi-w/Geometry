public abstract class Shape
{
    public double CenterX { get; }
    public double CenterY { get; }

    protected Shape(double x, double y)
    {
        CenterX = x;
        CenterY = y;
    }

    public abstract double GetArea();
    public abstract (double xMin, double xMax, double yMin, double yMax) GetBoundingRectangle();
}
