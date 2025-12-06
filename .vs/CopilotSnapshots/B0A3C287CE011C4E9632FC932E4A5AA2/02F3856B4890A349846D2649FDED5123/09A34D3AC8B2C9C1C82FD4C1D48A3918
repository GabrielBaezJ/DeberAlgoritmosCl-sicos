using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    internal class CMidPointAlgorithm
    {
        // Genera puntos de una línea usando el algoritmo del punto medio (integer variant)
        public List<Point> GenerateLinePoints(int x0, int y0, int x1, int y1)
        {
            var points = new List<Point>();

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;

            // Determinar si se intercambian los ejes
            bool swap = false;
            if (dy > dx)
            {
                // swap dx, dy
                var tmp = dx; dx = dy; dy = tmp;
                swap = true;
            }

            int d = 2 * dy - dx;
            int x = 0;
            int y = 0;

            int xi = x0;
            int yi = y0;

            for (int i = 0; i <= dx; i++)
            {
                points.Add(new Point(xi, yi));

                if (d >= 0)
                {
                    if (swap)
                        xi += sx;
                    else
                        yi += sy;
                    d -= 2 * dx;
                }

                if (d < 0)
                {
                    if (swap)
                        yi += sy;
                    else
                        xi += sx;
                }

                d += 2 * dy;
            }

            return points;
        }
    }
}
