using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Proporciona el algoritmo de recorte de segmentos Cyrus-Beck para recortar contra un polígono convexo.
    /// El polígono debe ser convexo y se normaliza a sentido antihorario si es necesario.
    /// Todos los métodos públicos validan las entradas y lanzan ArgumentException para parámetros inválidos.
    /// </summary>
    internal static class CCyrusBeck
    {
        private const float EPSILON = 1e-6f;

        /// <summary>
        /// Recorta un segmento contra un polígono convexo usando el algoritmo Cyrus-Beck.
        /// Devuelve true si existe una porción visible dentro del polígono; clippedP0/clippedP1 contienen el segmento recortado.
        /// El polígono debe tener al menos 3 vértices y ser convexo. El método intentará normalizar el sentido si es necesario.
        /// </summary>
        /// <param name="polygon">Vértices del polígono convexo (se acepta CW o CCW; se normaliza a CCW).</param>
        /// <param name="p0">Punto inicial del segmento.</param>
        /// <param name="p1">Punto final del segmento.</param>
        /// <param name="clippedP0">Salida: inicio del segmento recortado si retorna true.</param>
        /// <param name="clippedP1">Salida: fin del segmento recortado si retorna true.</param>
        /// <returns>True si el segmento (o su porción) yace dentro del polígono.</returns>
        public static bool ClipSegment(IList<PointF> polygon, PointF p0, PointF p1, out PointF clippedP0, out PointF clippedP1)
        {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            if (polygon.Count < 3) throw new ArgumentException("El polígono debe tener al menos 3 vértices.", nameof(polygon));
            if (Math.Abs(p0.X - p1.X) < EPSILON && Math.Abs(p0.Y - p1.Y) < EPSILON)
                throw new ArgumentException("Los extremos del segmento no deben ser idénticos.", nameof(p0));

            // Trabajar sobre una copia para no mutar la lista del llamador
            var poly = polygon.ToList();

            // Asegurar sentido antihorario (CCW). Si no es así, invertir.
            float area = SignedArea(poly);
            if (Math.Abs(area) < EPSILON)
                throw new ArgumentException("El área del polígono es nula o degenerada.", nameof(polygon));

            if (area < 0)
            {
                poly.Reverse();
            }

            if (!IsConvex(poly))
            {
                throw new ArgumentException("El polígono debe ser convexo.", nameof(polygon));
            }

            float tE = 0.0f; // parámetro máximo de entrada
            float tL = 1.0f; // parámetro mínimo de salida

            var d = new PointF(p1.X - p0.X, p1.Y - p0.Y);

            for (int i = 0; i < poly.Count; i++)
            {
                var vi = poly[i];
                var vj = poly[(i + 1) % poly.Count];

                // Vector arista
                var edge = new PointF(vj.X - vi.X, vj.Y - vi.Y);

                // Normal hacia el exterior para polígono CCW: (edge.Y, -edge.X)
                var n = new PointF(edge.Y, -edge.X);

                // Calcular numerador y denominador
                var numerator = Dot(n, new PointF(vi.X - p0.X, vi.Y - p0.Y));
                var denominator = Dot(n, d);

                if (Math.Abs(denominator) < EPSILON)
                {
                    // Línea paralela a esta arista
                    if (numerator < 0)
                    {
                        // Fuera del polígono
                        clippedP0 = PointF.Empty;
                        clippedP1 = PointF.Empty;
                        return false;
                    }
                    else
                    {
                        // Paralela pero dentro o en el borde: sin restricción
                        continue;
                    }
                }

                float t = numerator / denominator;

                if (denominator < 0)
                {
                    // Potencialmente entrando
                    if (t > tE) tE = t;
                }
                else
                {
                    // denominator > 0: potencialmente saliendo
                    if (t < tL) tL = t;
                }

                // Rechazo temprano
                if (tE - tL > EPSILON)
                {
                    clippedP0 = PointF.Empty;
                    clippedP1 = PointF.Empty;
                    return false;
                }
            }

            if (tE > tL + EPSILON)
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Calcular puntos recortados (clamp en [0,1])
            float ct0 = Math.Max(0.0f, tE);
            float ct1 = Math.Min(1.0f, tL);

            if (ct0 > ct1 + EPSILON)
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            clippedP0 = new PointF(p0.X + ct0 * d.X, p0.Y + ct0 * d.Y);
            clippedP1 = new PointF(p0.X + ct1 * d.X, p0.Y + ct1 * d.Y);
            return true;
        }

        /// <summary>
        /// Recorta múltiples segmentos contra el polígono convexo y devuelve la lista de segmentos visibles.
        /// Se ignoran segmentos degenerados. Realiza validación de argumentos.
        /// </summary>
        /// <param name="polygon">Vértices del polígono convexo.</param>
        /// <param name="segments">Colección de segmentos (inicio, fin).</param>
        /// <returns>Lista de segmentos recortados.</returns>
        public static List<(PointF, PointF)> ClipSegments(IList<PointF> polygon, IEnumerable<(PointF, PointF)> segments)
        {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            if (segments == null) throw new ArgumentNullException(nameof(segments));

            var result = new List<(PointF, PointF)>();

            foreach (var s in segments)
            {
                try
                {
                    // Omitir degenerados
                    if (Math.Abs(s.Item1.X - s.Item2.X) < EPSILON && Math.Abs(s.Item1.Y - s.Item2.Y) < EPSILON)
                        continue;

                    if (ClipSegment(polygon, s.Item1, s.Item2, out var a, out var b))
                        result.Add((a, b));
                }
                catch (ArgumentException)
                {
                    // Si el polígono o segmento es inválido, ignorar y continuar con los demás
                    continue;
                }
            }

            return result;
        }

        #region Métodos auxiliares

        private static float Dot(PointF a, PointF b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        private static float SignedArea(IList<PointF> poly)
        {
            // Calcular área firmada usando la fórmula del zapatero
            float area = 0;
            for (int i = 0; i < poly.Count; i++)
            {
                var p0 = poly[i];
                var p1 = poly[(i + 1) % poly.Count];
                area += (p0.X * p1.Y - p1.X * p0.Y);
            }
            return area / 2f;
        }

        private static bool IsConvex(IList<PointF> poly)
        {
            // Un polígono es convexo si todos los productos cruz de aristas consecutivas tienen el mismo signo
            int n = poly.Count;
            if (n < 3) return false;

            int sign = 0;
            for (int i = 0; i < n; i++)
            {
                var p0 = poly[i];
                var p1 = poly[(i + 1) % n];
                var p2 = poly[(i + 2) % n];

                var dx1 = p1.X - p0.X;
                var dy1 = p1.Y - p0.Y;
                var dx2 = p2.X - p1.X;
                var dy2 = p2.Y - p1.Y;

                float cross = dx1 * dy2 - dy1 * dx2;

                if (Math.Abs(cross) < EPSILON) continue; // colineal

                int currentSign = cross > 0 ? 1 : -1;
                if (sign == 0) sign = currentSign;
                else if (sign != currentSign) return false;
            }

            return true;
        }

        #endregion
    }
}
