using SolidWorks.Interop.sldworks;

namespace WindowsFormsApp1.Pekok.Primitive
{
    public class Line
    {
        public PointD start;
        public PointD end;
        public SketchSegment sketch;
        public PointD draw(SketchManager sm)
        {
            sketch = sm.CreateLine(start.X, start.Y, start.Z, end.X, end.Y, end.Z);
            if (sketch != null) sketch.Select(false);
            return end;
        }
        public Line() { }
        public Line(PointD start, PointD end)
        {
            this.start = start;
            this.end = end;
        }
        public Line(PointD start, PointD end, SketchSegment sketch) : this(start, end)
        {
            this.sketch = sketch;
        }
        public Line(double x, double y, double z, double x1, double y1, double z1)
        {
            start.X = x;
            start.Y = y;
            start.Z = z;

            end.X = x1;
            end.Y = y1;
            end.Z = z1;
        }
        public Line(double x, double y, double z, double x1, double y1, double z1, SketchSegment sketch) : this(x, y, z, x1, y1, z1)
        {
            this.sketch = sketch;
        }
    }
}
