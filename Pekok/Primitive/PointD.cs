using SolidWorks.Interop.sldworks;

namespace WindowsFormsApp1.Pekok.Primitive
{
    public class PointD
    {
        public SketchPoint sketch;
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public PointD() { }
        public PointD(double x, double y, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public PointD draw(SketchManager sm)
        {
            sketch = sm.CreatePoint(X, Y, Z);
            sketch.Select(false);
            return this;
        }
    }
}
