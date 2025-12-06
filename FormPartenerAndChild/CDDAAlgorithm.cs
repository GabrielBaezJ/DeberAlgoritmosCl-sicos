using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    internal class CDDAAlgorithm
    {
        // Genera los puntos de una línea usando el algoritmo DDA
        public List<Point> GenerateLinePoints(int x0, int y0, int x1, int y1)
        {
            var points = new List<Point>();

            int dx = x1 - x0;
            int dy = y1 - y0;

            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            if (steps == 0)
            {
                points.Add(new Point(x0, y0));
                return points;
            }

            double xInc = dx / (double)steps;
            double yInc = dy / (double)steps;

            double x = x0;
            double y = y0;

            for (int i = 0; i <= steps; i++)
            {
                points.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
                x += xInc;
                y += yInc;
            }

            return points;
        }
    }
}
