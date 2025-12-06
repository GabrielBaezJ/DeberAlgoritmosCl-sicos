using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    internal class CBresenhamCircle
    {
        // Genera puntos del círculo usando el algoritmo de Bresenham (entero)
        public List<Point> GenerateCirclePoints(int xc, int yc, int r)
        {
            var points = new List<Point>();
            if (r <= 0) return points;

            int x = 0;
            int y = r;
            int d = 3 - 2 * r;

            AddSymmetricPoints(points, xc, yc, x, y);

            while (x <= y)
            {
                x++;
                if (d <= 0)
                {
                    d = d + 4 * x + 6;
                }
                else
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                AddSymmetricPoints(points, xc, yc, x, y);
            }

            return points;
        }

        private void AddSymmetricPoints(List<Point> points, int xc, int yc, int x, int y)
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
