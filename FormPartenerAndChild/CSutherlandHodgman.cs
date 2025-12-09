using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Implementa el algoritmo de recorte de polígonos Sutherland–Hodgman.
    /// Recorta un polígono sujeto contra un polígono de recorte (preferiblemente convexo).
    /// La implementación valida entradas y lanzará excepciones informativas para datos inválidos.
    /// </summary>
    internal static class CSutherlandHodgman
    {
        private const float EPSILON = 1e-6f;

        /// <summary>
        /// Recorta el polígono sujeto contra el polígono de recorte usando el algoritmo Sutherland–Hodgman.
        /// Ambos polígonos deben contener al menos 3 vértices. El polígono de recorte debería ser convexo
        /// para obtener resultados correctos. Devuelve un nuevo polígono que representa el resultado del recorte
        /// (puede estar vacío si no hay solapamiento).
        /// </summary>
        /// <param name="subject">Vértices del polígono sujeto (puede ser cóncavo).</param>
        /// <param name="clip">Vértices del polígono de recorte (debería ser convexo).</param>
        /// <returns>Vértices del polígono recortado (posiblemente vacío).</returns>
        public static List<PointF> ClipPolygon(IList<PointF> subject, IList<PointF> clip)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (subject.Count < 3) throw new ArgumentException("El polígono sujeto debe tener al menos 3 vértices.", nameof(subject));
            if (clip.Count < 3) throw new ArgumentException("El polígono de recorte debe tener al menos 3 vértices.", nameof(clip));

            // Preferimos que el polígono de recorte sea convexo; si no lo es, lanzamos excepción para comportamiento estricto
            if (!IsConvex(clip))
                throw new ArgumentException("El polígono de recorte debe ser convexo para resultados fiables.", nameof(clip));

            // Trabajar sobre copias
            var outputList = subject.ToList();

            // Iterar sobre las aristas del polígono de recorte
            for (int i = 0; i < clip.Count; i++)
            {
                var inputList = outputList.ToList();
                outputList.Clear();

                var A = clip[i];
                var B = clip[(i + 1) % clip.Count];

                if (inputList.Count == 0) break; // nada que recortar

                PointF S = inputList[inputList.Count - 1]; // último vértice

                foreach (var E in inputList)
                {
                    bool Ein = IsInside(A, B, E);
                    bool Sin = IsInside(A, B, S);

                    if (Sin && Ein)
                    {
                        // ambos dentro
                        outputList.Add(E);
                    }
                    else if (Sin && !Ein)
                    {
                        // saliendo -- agregar intersección
                        if (TryIntersect(S, E, A, B, out var ip))
                            outputList.Add(ip);
                    }
                    else if (!Sin && Ein)
                    {
                        // entrando -- agregar intersección luego E
                        if (TryIntersect(S, E, A, B, out var ip))
                            outputList.Add(ip);
                        outputList.Add(E);
                    }
                    // si ambos fuera -> no hacer nada

                    S = E;
                }
            }

            // Eliminar puntos consecutivos casi duplicados
            outputList = RemoveDuplicatePoints(outputList);

            return outputList;
        }

        #region Ayudantes geométricos

        // Determina si el punto P está dentro del semiplano definido por la arista AB (suponiendo polígono clip CCW)
        private static bool IsInside(PointF A, PointF B, PointF P)
        {
            // Para la arista AB, el interior está a la izquierda de la arista dirigida (A->B) para polígono CCW
            var cross = (B.X - A.X) * (P.Y - A.Y) - (B.Y - A.Y) * (P.X - A.X);
            return cross >= -EPSILON; // permitir puntos sobre la arista
        }

        // Intenta calcular la intersección entre el segmento S->E y la recta infinita AB.
        // Devuelve false si son paralelos y no hay intersección útil.
        private static bool TryIntersect(PointF S, PointF E, PointF A, PointF B, out PointF intersection)
        {
            intersection = PointF.Empty;

            var dxSE = E.X - S.X;
            var dySE = E.Y - S.Y;

            var dxAB = B.X - A.X;
            var dyAB = B.Y - A.Y;

            float denom = dxSE * dyAB - dySE * dxAB;

            if (Math.Abs(denom) < EPSILON)
            {
                // Paralelo o colineal
                return false;
            }

            // Resolver parámetros: S + t*(E-S) intersecta A + u*(B-A)
            float t = ((A.X - S.X) * dyAB - (A.Y - S.Y) * dxAB) / denom;

            intersection = new PointF(S.X + t * dxSE, S.Y + t * dySE);
            return true;
        }

        // Comprobar convexidad de un polígono (test O(n) simple)
        private static bool IsConvex(IList<PointF> poly)
        {
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
                if (Math.Abs(cross) < EPSILON) continue;

                int currentSign = cross > 0 ? 1 : -1;
                if (sign == 0) sign = currentSign;
                else if (sign != currentSign) return false;
            }

            return true;
        }

        private static List<PointF> RemoveDuplicatePoints(List<PointF> poly)
        {
            var res = new List<PointF>();
            foreach (var p in poly)
            {
                if (res.Count == 0) res.Add(p);
                else
                {
                    var last = res[res.Count - 1];
                    if (Math.Abs(last.X - p.X) > EPSILON || Math.Abs(last.Y - p.Y) > EPSILON)
                        res.Add(p);
                }
            }

            // Comprobación de cierre: si primer y último son iguales, quitar el último
            if (res.Count > 1)
            {
                var first = res[0];
                var last = res[res.Count - 1];
                if (Math.Abs(first.X - last.X) < EPSILON && Math.Abs(first.Y - last.Y) < EPSILON)
                    res.RemoveAt(res.Count - 1);
            }

            return res;
        }

        #endregion
    }
}
