using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Clase de utilidad que implementa el algoritmo de recorte de líneas Liang-Barsky.
    /// Los métodos son estáticos y devuelven si la línea fue aceptada y los puntos recortados cuando procede.
    /// Todos los métodos públicos validan las entradas y lanzan ArgumentException para parámetros inválidos.
    /// </summary>
    internal static class CLiangBarsky
    {
        /// <summary>
        /// Intenta recortar la línea desde p0 hasta p1 contra el rectángulo de recorte alineado a los ejes.
        /// Devuelve true si existe un segmento visible dentro del rectángulo; clippedP0/clippedP1 contienen el segmento recortado.
        /// Si la línea está completamente fuera, devuelve false y los puntos recortados no están definidos.
        /// </summary>
        /// <param name="clipRect">Rectángulo de recorte (must tener ancho y alto positivos).</param>
        /// <param name="p0">Punto inicial de la línea.</param>
        /// <param name="p1">Punto final de la línea.</param>
        /// <param name="clippedP0">Salida: inicio del segmento recortado.</param>
        /// <param name="clippedP1">Salida: fin del segmento recortado.</param>
        /// <returns>True si existe una porción visible (posiblemente degenerada) dentro del rectángulo.</returns>
        public static bool LiangBarskyClip(RectangleF clipRect, PointF p0, PointF p1, out PointF clippedP0, out PointF clippedP1)
        {
            // Validar rectángulo de recorte
            if (clipRect.Width <= 0 || clipRect.Height <= 0)
            {
                throw new ArgumentException("El rectángulo de recorte debe tener ancho y alto positivos.", nameof(clipRect));
            }

            // Inicialización
            float t0 = 0.0f; // parámetro de entrada
            float t1 = 1.0f; // parámetro de salida
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;

            // Función interna para procesar cada borde
            bool ClipTest(float p, float q, ref float tE, ref float tL)
            {
                // p: componente de dirección, q: distancia relativa al límite
                if (Math.Abs(p) < 1e-6f)
                {
                    // La línea es paralela a este borde de recorte
                    if (q < 0)
                        return false; // paralela y fuera
                    else
                        return true; // paralela y dentro
                }

                float r = q / p;
                if (p < 0)
                {
                    // punto potencial de entrada
                    if (r > tL) return false; // fuera
                    if (r > tE) tE = r;
                }
                else if (p > 0)
                {
                    // punto potencial de salida
                    if (r < tE) return false; // fuera
                    if (r < tL) tL = r;
                }
                return true;
            }

            // Borde izquierdo: x >= clipRect.Left
            if (!ClipTest(-dx, p0.X - clipRect.Left, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Borde derecho: x <= clipRect.Right
            if (!ClipTest(dx, clipRect.Right - p0.X, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Borde superior (pantalla): y >= clipRect.Top (nota: coordenadas de pantalla suelen tener y hacia abajo)
            if (!ClipTest(-dy, p0.Y - clipRect.Top, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Borde inferior: y <= clipRect.Bottom
            if (!ClipTest(dy, clipRect.Bottom - p0.Y, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            if (t1 < t0)
            {
                // No hay porción visible
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Calcular puntos recortados
            clippedP0 = new PointF(p0.X + t0 * dx, p0.Y + t0 * dy);
            clippedP1 = new PointF(p0.X + t1 * dx, p0.Y + t1 * dy);
            return true;
        }

        /// <summary>
        /// Recorta una lista de segmentos de línea contra el rectángulo de recorte y devuelve la lista de segmentos recortados.
        /// Líneas inválidas (longitud cero) son ignoradas. El método realiza validación de argumentos.
        /// </summary>
        /// <param name="clipRect">Rectángulo de recorte.</param>
        /// <param name="segments">Colección de segmentos (inicio, fin).</param>
        /// <returns>Lista de segmentos recortados que se encuentran (al menos parcialmente) dentro del rectángulo.</returns>
        public static List<(PointF, PointF)> ClipSegments(RectangleF clipRect, IEnumerable<(PointF, PointF)> segments)
        {
            if (segments == null) throw new ArgumentNullException(nameof(segments));
            if (clipRect.Width <= 0 || clipRect.Height <= 0)
                throw new ArgumentException("El rectángulo de recorte debe tener ancho y alto positivos.", nameof(clipRect));

            var result = new List<(PointF, PointF)>();

            foreach (var seg in segments)
            {
                // Omitir segmentos degenerados
                if (Math.Abs(seg.Item1.X - seg.Item2.X) < 1e-6f && Math.Abs(seg.Item1.Y - seg.Item2.Y) < 1e-6f)
                    continue;

                if (LiangBarskyClip(clipRect, seg.Item1, seg.Item2, out var a, out var b))
                {
                    result.Add((a, b));
                }
            }

            return result;
        }
    }
}
