using SolidWorks.Interop.sldworks;
using WindowsFormsApp1.Pekok.Primitive;

namespace WindowsFormsApp1.Pekok.Figure
{
    public class Pentagon
    {
        public PointD vertex;
        public PointD center;
        public const int COUNT = 5;
        public object[] sketch;

        public Pentagon()
        {
            vertex = new PointD();
            center = new PointD();
        }
        public Pentagon(PointD center, PointD vertex)
        {
            this.vertex = vertex;
            this.center = center;
        }
        public Pentagon(PointD center, PointD vertex, SketchSegment[] sketch) : this(center, vertex)
        {
            this.sketch = sketch;
        }
        public Pentagon(double cx, double cy, double cz, double vx, double vy, double vz) : this()
        {
            vertex.X = vx;
            vertex.Y = vy;
            vertex.Z = vz;

            center.X = cx;
            center.Y = cy;
            center.Z = cz;
        }
        public Pentagon(double cx, double cy, double cz, double vx, double vy, double vz, SketchSegment[] sketch) : this(cx, cy, cz, vx, vy, vz)
        {
            this.sketch = sketch;
        }


        public object[] draw(SketchManager sm)
        {
            sketch = sm.CreatePolygon(center.X, center.Y, center.Z, vertex.X, vertex.Y, vertex.Z, COUNT, false);
            return sketch;
        }
    }
}