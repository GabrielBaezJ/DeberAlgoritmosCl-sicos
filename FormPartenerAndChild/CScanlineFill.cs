using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormPartenerAndChild
{
    /// <summary>
    /// CScanlineFill
    /// Proporciona una implementación de relleno de polígonos por scanline con soporte para
    /// las reglas de relleno Par-Impar (Even-Odd) y Número de vueltas (NonZero).
    /// La implementación es modular para que pueda ser reemplazada o extendida fácilmente.
    /// </summary>
    internal class CScanlineFill
    {
        public enum FillRule
        {
            EvenOdd,
            NonZero
        }

        /// <summary>
        /// Rellena un polígono en el bitmap proporcionado usando el algoritmo de scanline.
        /// El polígono se proporciona como una lista de Points (orden de vértices: cualquiera).
        /// El método es resiliente a entradas inválidas y lanza ArgumentException
        /// para polígonos inválidos.
        /// Devuelve una lista de segmentos rellenados (y, xInicio, xFin) para visualización opcional en la UI.
        /// </summary>
        public List<(int y, int xStart, int xEnd)> ScanlineFill(Bitmap bmp, List<Point> polygon, Color fillColor, FillRule rule = FillRule.EvenOdd)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            if (polygon.Count < 3) throw new ArgumentException("El polígono debe tener al menos 3 vértices.", nameof(polygon));

            int width = bmp.Width;
            int height = bmp.Height;

            // Construir tabla de aristas: para cada arista, calcular ymin, ymax, x en ymin e inversa de la pendiente
            var edges = new List<Edge>();
            for (int i = 0; i < polygon.Count; i++)
            {
                Point p1 = polygon[i];
                Point p2 = polygon[(i + 1) % polygon.Count];

                // Ignorar aristas horizontales para propósitos de intersección
                if (p1.Y == p2.Y) continue;

                // Asegurar que ymin < ymax
                if (p1.Y < p2.Y)
                    edges.Add(new Edge(p1.X, p1.Y, p2.X, p2.Y));
                else
                    edges.Add(new Edge(p2.X, p2.Y, p1.X, p1.Y));
            }

            if (edges.Count == 0) throw new ArgumentException("El polígono no tiene aristas válidas.", nameof(polygon));

            int yMin = edges.Min(e => e.Ymin);
            int yMax = edges.Max(e => e.Ymax);

            // Limitar al bitmap
            yMin = Math.Max(0, yMin);
            yMax = Math.Min(height - 1, yMax);

            var filledSegments = new List<(int y, int xStart, int xEnd)>();

            // Tabla de Aristas Activas (AET)
            var AET = new List<ActiveEdge>();

            // Para cada línea de escaneo
            for (int y = yMin; y <= yMax; y++)
            {
                // Añadir aristas donde ymin == y
                foreach (var edge in edges.Where(e => e.Ymin == y))
                {
                    AET.Add(new ActiveEdge(edge));
                }

                // Eliminar aristas donde ymax == y
                AET.RemoveAll(ae => ae.Ymax <= y);

                // Actualizar x para aristas activas y ordenar por x actual
                foreach (var ae in AET)
                {
                    // x actual ya se mantiene
                }

                AET.Sort((a, b) => a.CurrentX.CompareTo(b.CurrentX));

                // Construir lista de intersecciones
                var intersections = AET.Select(a => a.CurrentX).ToList();

                // Emparejar intersecciones y rellenar entre pares según la regla
                if (intersections.Count >= 2)
                {
                    if (rule == FillRule.EvenOdd)
                    {
                        for (int i = 0; i + 1 < intersections.Count; i += 2)
                        {
                            int xStart = (int)Math.Ceiling(intersections[i]);
                            int xEnd = (int)Math.Floor(intersections[i + 1]);
                            if (xEnd >= xStart)
                            {
                                // Limitar al bitmap
                                int xs = Math.Max(0, xStart);
                                int xe = Math.Min(width - 1, xEnd);
                                RellenarLineaEscaneo(bmp, y, xs, xe, fillColor);
                                filledSegments.Add((y, xs, xe));
                            }
                        }
                    }
                    else if (rule == FillRule.NonZero)
                    {
                        // Implementación de número de vueltas (NonZero): recorrer aristas y mantener número de vueltas
                        // Construir lista de contribuciones de aristas ordenadas por x
                        var edgeContribs = new List<(double x, int contrib)>();
                        foreach (var ae in AET)
                        {
                            edgeContribs.Add((ae.CurrentX, ae.Winding));
                        }
                        edgeContribs = edgeContribs.OrderBy(ec => ec.x).ToList();

                        int winding = 0;
                        double? segStart = null;

                        for (int i = 0; i < edgeContribs.Count; i++)
                        {
                            winding += edgeContribs[i].contrib;
                            double nextX = (i + 1 < edgeContribs.Count) ? edgeContribs[i + 1].x : edgeContribs[i].x;

                            if (winding != 0)
                            {
                                // rellenar entre edgeContribs[i].x y nextX
                                int xs = (int)Math.Ceiling(edgeContribs[i].x);
                                int xe = (int)Math.Floor(nextX);
                                if (xe >= xs)
                                {
                                    int xs2 = Math.Max(0, xs);
                                    int xe2 = Math.Min(width - 1, xe);
                                    RellenarLineaEscaneo(bmp, y, xs2, xe2, fillColor);
                                    filledSegments.Add((y, xs2, xe2));
                                }
                            }
                        }
                    }
                }

                // Incrementar x actual para aristas de AET
                foreach (var ae in AET)
                {
                    ae.IncrementarX();
                }
            }

            return filledSegments;
        }

        /// <summary>
        /// Rellena píxeles en una única línea de escaneo y desde xInicio hasta xFin inclusive con el color de relleno.
        /// Usa SetPixel; el código que llama debe asegurar que las coordenadas estén dentro de los límites del bitmap.
        /// </summary>
        private void RellenarLineaEscaneo(Bitmap bmp, int y, int xStart, int xEnd, Color fillColor)
        {
            if (y < 0 || y >= bmp.Height) return;
            xStart = Math.Max(0, xStart);
            xEnd = Math.Min(bmp.Width - 1, xEnd);
            for (int x = xStart; x <= xEnd; x++)
            {
                try
                {
                    bmp.SetPixel(x, y, fillColor);
                }
                catch
                {
                    // ignorar errores de establecimiento de píxeles para mantener la aplicación estable
                }
            }
        }

        /// <summary>
        /// Almacenamiento de arista usado para construir la Tabla de Aristas Activas (AET).
        /// </summary>
        private class Edge
        {
            public int X1, Y1, X2, Y2;
            public int Ymin => Math.Min(Y1, Y2);
            public int Ymax => Math.Max(Y1, Y2);
            public Edge(int x1, int y1, int x2, int y2)
            {
                X1 = x1; Y1 = y1; X2 = x2; Y2 = y2;
            }
        }

        /// <summary>
        /// Arista activa con x actual (double) y delta por línea de escaneo.
        /// El número de vueltas es +1 para aristas hacia arriba y -1 para aristas hacia abajo, usado por la regla NonZero.
        /// </summary>
        private class ActiveEdge
        {
            public double CurrentX;
            public int Ymax;
            public double InvSlope; // dx/dy
            public int Winding;

            public ActiveEdge(Edge e)
            {
                CurrentX = e.X1;
                Ymax = e.Ymax;
                // calcular pendiente inversa: dx / dy donde dy != 0
                double dy = (double)(e.Y2 - e.Y1);
                double dx = (double)(e.X2 - e.X1);
                InvSlope = dx / dy;
                Winding = (e.Y2 > e.Y1) ? 1 : -1; // arista hacia arriba contribuye +1
            }

            public void IncrementarX()
            {
                CurrentX += InvSlope;
            }
        }
    }
}
