using SolidWorks.Interop.sldworks;
using WindowsFormsApp1.Pekok.Primitive;

namespace WindowsFormsApp1.Pekok.Figure
{
    class Parallelepiped
    {
        public PointD p1, p2, p3;
        public object[] sketch;
        public Parallelepiped()
        {
            p1 = new PointD();
            p2 = new PointD();
            p3 = new PointD();
        }
        public Parallelepiped(PointD p1, PointD p2, PointD p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
        public Parallelepiped(PointD p1, PointD p2, PointD p3, SketchSegment[] sketch) : this(p1, p2, p3)
        {
            this.sketch = sketch;
        }
        public Parallelepiped(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3) : this()
        {
            p1.X = x1;
            p1.Y = y1;
            p1.Z = z1;

            p2.X = x2;
            p2.Y = y2;
            p2.Z = z2;

            p3.X = x3;
            p3.Y = y3;
            p3.Z = z3;
        }
        public Parallelepiped(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3, SketchSegment[] sketch) : this(x1, y1, z1, x2, y2, z2, x3, y3, z3)
        {
            this.sketch = sketch;
        }

        public object[] draw(SketchManager sm)
        {
            sketch = sm.CreateParallelogram(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z, p3.X, p3.Y, p3.Z);

            return sketch;
        }
    }
}