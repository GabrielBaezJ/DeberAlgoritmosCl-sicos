using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FormPartenerAndChild
{
    internal class CCircle
    {
        public List<Point> points = new List<Point>();

        public void CircleMidPoint(int xc, int yc, int r)
        {
            int x, y, p;
            x = 0;
            y = r;
            p = 1 - r;

            PlotPoint(xc, yc, x, y);

            while (x < y)
            {
                x = x + 1;
                if (p < 0)
                {
                    p = p + 2 * x + 3;
                }
                else
                {
                    y = y - 1;
                    p = p + 2 * (x - y) + 5;
                }
                PlotPoint(xc, yc, x, y);
            }
        }

        void PlotPoint(int xc, int yc, int x, int y)
        {
            points.Add(new Point(xc + x, yc + y));
            points.Add(new Point(xc - x, yc + y));
            points.Add(new Point(xc + x, yc - y));
            points.Add(new Point(xc - x, yc - y));
            points.Add(new Point(xc + y, yc + x));
            points.Add(new Point(xc - y, yc + x));
            points.Add(new Point(xc + y, yc - x));
            points.Add(new Point(xc - y, yc - x));
        }
    }
}
