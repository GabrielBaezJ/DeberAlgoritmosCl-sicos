using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    internal class CParametricCircleAlgorithm
    {
        // Genera los puntos del círculo usando la forma paramétrica: x = xc + r*cos(theta), y = yc + r*sin(theta)
        // Ahora acepta ángulo de inicio y ángulo de barrido en grados para dibujar arcos.
        public List<Point> GenerateCirclePoints(int xc, int yc, int r, double startAngleDeg = 0, double sweepAngleDeg = 360)
        {
            var points = new List<Point>();
            if (r <= 0) return points;

            // Normalizar sweep
            if (Math.Abs(sweepAngleDeg) < 1e-6) return points;

            double sweep = sweepAngleDeg;
            // Si el barrido es mayor que 360, limitar a 360
            if (Math.Abs(sweep) >= 360.0)
            {
                sweep = 360.0;
            }

            // Convertir a radianes
            double startRad = startAngleDeg * Math.PI / 180.0;
            double sweepRad = sweep * Math.PI / 180.0;

            // Determinar número de muestras proporcional al arco
            double circumference = 2 * Math.PI * r;
            double arcLength = Math.Abs(sweep) / 360.0 * circumference;
            int n = Math.Max(12, (int)Math.Ceiling(arcLength * 1.2));

            for (int i = 0; i <= n; i++)
            {
                double theta = startRad + sweepRad * i / n;
                int x = (int)Math.Round(xc + r * Math.Cos(theta));
                int y = (int)Math.Round(yc + r * Math.Sin(theta));
                points.Add(new Point(x, y));
            }

            return points;
        }
    }
}
