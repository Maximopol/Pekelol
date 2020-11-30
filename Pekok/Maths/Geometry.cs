using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Pekok.Primitive;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1.Pekok.Maths
{
    public class Geometry
    {
        public static double getDistanceBetweenTwoPoints(PointD first, PointD second)
        {
            return getDistanceBetweenTwoPoints(first.X, first.Y, first.Z, second.X, second.Y, second.Z);
        }
        public static double getDistanceBetweenTwoPoints(double firstX, double firstY, double secondX, double secondY)
        {
            return getDistanceBetweenTwoPoints(firstX, firstY, 0, secondX, secondY, 0);
        }
        public static double getDistanceBetweenTwoPoints(double firstX, double firstY, double firstZ, double secondX, double secondY, double secondZ)
        {
            return Math.Sqrt(
                Math.Pow(secondX - firstX, 2) +
                 Math.Pow(secondY - firstY, 2) +
                 Math.Pow(secondZ - firstZ, 2)
                );
        }

        public static double getRotationAngleRelativeToY(PointD center, PointD vertex)
        {
            double add = 0;
        
            double a =(vertex.Y - center.Y),
                 b = (vertex.X - center.X),
                 c = getDistanceBetweenTwoPoints(center, vertex);
            if (vertex.X < center.X)
            {
                c = -c;
                add = 180;
            }
            return Math.Acos(a / c) * 180 / Math.PI+ add;
        }
    }
}
