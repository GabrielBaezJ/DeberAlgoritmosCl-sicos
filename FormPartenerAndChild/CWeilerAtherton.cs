using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Implementa el algoritmo de recorte Weiler–Atherton.
    /// Recorta un polígono sujeto contra un polígono de recorte y devuelve cero o más polígonos recortados.
    /// El algoritmo inserta vértices de intersección en ambos polígonos y los recorre para construir los polígonos resultado.
    /// Los métodos públicos validan las entradas y lanzan ArgumentException para datos inválidos.
    /// </summary>
    internal static class CWeilerAtherton
    {
        private const float EPSILON = 1e-6f;

        /// <summary>
        /// Recorta el polígono sujeto contra el polígono de recorte usando Weiler–Atherton.
        /// Devuelve una lista de polígonos recortados (puede estar vacía si no hay intersección).
        /// </summary>
        /// <param name="subject">Vértices del polígono sujeto (puede ser cóncavo).</param>
        /// <param name="clip">Vértices del polígono de recorte (puede ser cóncavo).</param>
        /// <returns>Lista de polígonos recortados (cada uno como lista de vértices).</returns>
        public static List<List<PointF>> ClipPolygon(IList<PointF> subject, IList<PointF> clip)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (subject.Count < 3) throw new ArgumentException("El polígono sujeto debe tener al menos 3 vértices.", nameof(subject));
            if (clip.Count < 3) throw new ArgumentException("El polígono de recorte debe tener al menos 3 vértices.", nameof(clip));

            // Trabajar sobre copias limpiando puntos consecutivos duplicados
            var subj = RemoveConsecutiveDuplicates(subject).ToList();
            var clp = RemoveConsecutiveDuplicates(clip).ToList();

            if (subj.Count < 3) throw new ArgumentException("El polígono sujeto es degenerado después de limpiar.", nameof(subject));
            if (clp.Count < 3) throw new ArgumentException("El polígono de recorte es degenerado después de limpiar.", nameof(clip));

            // Si no hay intersecciones: el sujeto está completamente dentro o fuera
            if (!HasAnyIntersection(subj, clp))
            {
                if (PointInPolygon(subj[0], clp))
                {
                    // sujeto totalmente dentro del clip
                    return new List<List<PointF>> { subj };
                }
                else
                {
                    // sujeto totalmente fuera
                    return new List<List<PointF>>();
                }
            }

            // Construir listas enlazadas circulares de vértices para subject y clip
            var subjNodes = BuildVertexList(subj);
            var clipNodes = BuildVertexList(clp);

            // Encontrar intersecciones e insertar nodos
            var intersections = new List<IntersectionRecord>();

            for (int i = 0; i < subj.Count; i++)
            {
                var p1 = subj[i];
                var p2 = subj[(i + 1) % subj.Count];

                for (int j = 0; j < clp.Count; j++)
                {
                    var q1 = clp[j];
                    var q2 = clp[(j + 1) % clp.Count];

                    if (SegmentIntersection(p1, p2, q1, q2, out float t, out float u, out PointF ip))
                    {
                        // Crear registros de intersección; los nodos reales se crearán/insertarán posteriormente
                        intersections.Add(new IntersectionRecord
                        {
                            SubjectEdgeIndex = i,
                            ClipEdgeIndex = j,
                            T = t,
                            U = u,
                            Point = ip
                        });
                    }
                }
            }

            // Insertar nodos de intersección en la lista de subject por cada arista
            foreach (var grp in intersections.GroupBy(x => x.SubjectEdgeIndex))
            {
                int edgeIndex = grp.Key;
                var list = grp.OrderBy(x => x.T).ToList();

                // encontrar el nodo correspondiente al vértice de inicio de la arista
                var startNode = subjNodes[edgeIndex];
                var insertAfter = startNode;

                foreach (var rec in list)
                {
                    // si la intersección está en el inicio o fin, enlazar con el vértice existente
                    if (rec.T <= EPSILON)
                    {
                        rec.SubjectNode = startNode;
                        rec.SubjectNode.IsIntersection = true;
                    }
                    else if (rec.T >= 1 - EPSILON)
                    {
                        var endNode = subjNodes[(edgeIndex + 1) % subjNodes.Count];
                        rec.SubjectNode = endNode;
                        rec.SubjectNode.IsIntersection = true;
                    }
                    else
                    {
                        var newNode = new Vertex(rec.Point) { IsIntersection = true };
                        InsertAfter(insertAfter, newNode);
                        rec.SubjectNode = newNode;
                        insertAfter = newNode;
                    }
                }
            }

            // Insertar nodos de intersección en la lista de clip por cada arista
            foreach (var grp in intersections.GroupBy(x => x.ClipEdgeIndex))
            {
                int edgeIndex = grp.Key;
                var list = grp.OrderBy(x => x.U).ToList();

                var startNode = clipNodes[edgeIndex];
                var insertAfter = startNode;

                foreach (var rec in list)
                {
                    if (rec.U <= EPSILON)
                    {
                        rec.ClipNode = startNode;
                        rec.ClipNode.IsIntersection = true;
                    }
                    else if (rec.U >= 1 - EPSILON)
                    {
                        var endNode = clipNodes[(edgeIndex + 1) % clipNodes.Count];
                        rec.ClipNode = endNode;
                        rec.ClipNode.IsIntersection = true;
                    }
                    else
                    {
                        var newNode = new Vertex(rec.Point) { IsIntersection = true };
                        InsertAfter(insertAfter, newNode);
                        rec.ClipNode = newNode;
                        insertAfter = newNode;
                    }
                }
            }

            // Asegurar que cada registro de intersección tenga SubjectNode y ClipNode y enlazarlos
            foreach (var rec in intersections)
            {
                if (rec.SubjectNode == null || rec.ClipNode == null)
                {
                    // Intentar encontrar nodos en las mismas coordenadas
                    rec.SubjectNode = rec.SubjectNode ?? FindNodeAtPoint(subjNodes, rec.Point);
                    rec.ClipNode = rec.ClipNode ?? FindNodeAtPoint(clipNodes, rec.Point);
                }

                if (rec.SubjectNode == null || rec.ClipNode == null)
                {
                    // Omitir intersección malformada
                    continue;
                }

                // Enlazar vecinos
                rec.SubjectNode.Neighbor = rec.ClipNode;
                rec.ClipNode.Neighbor = rec.SubjectNode;

                // Almacenar índice de arista para clasificación
                rec.SubjectNode.EdgeIndex = rec.SubjectEdgeIndex;
                rec.SubjectNode.Alpha = rec.T;
                rec.ClipNode.EdgeIndex = rec.ClipEdgeIndex;
                rec.ClipNode.Alpha = rec.U;
            }

            // Clasificar intersecciones en subject como entrada o salida
            foreach (var rec in intersections)
            {
                var sNode = rec.SubjectNode;
                if (sNode == null) continue;
                if (!sNode.IsIntersection) continue;

                // Dirección de la arista subject
                var sIdx = rec.SubjectEdgeIndex;
                var p1 = subj[sIdx];
                var p2 = subj[(sIdx + 1) % subj.Count];
                var dir = new PointF(p2.X - p1.X, p2.Y - p1.Y);
                var len = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                if (len < EPSILON) continue;
                dir.X /= len; dir.Y /= len;

                // Punto ligeramente después de la intersección en la dirección de subject
                var after = new PointF(rec.Point.X + dir.X * 1e-3f, rec.Point.Y + dir.Y * 1e-3f);
                sNode.IsEntry = PointInPolygon(after, clp);
            }

            // Recopilar todas las intersecciones en subject
            var allSubjectIntersections = GetAllNodes(subjNodes).Where(v => v.IsIntersection).ToList();

            var results = new List<List<PointF>>();

            // Si no hay nodos de intersección tras la inserción (poco probable), devolver vacío
            if (allSubjectIntersections.Count == 0)
            {
                return results;
            }

            // Recorrer para construir polígonos recortados
            foreach (var start in allSubjectIntersections)
            {
                if (start.Visited) continue;

                // Preferir comenzar en una intersección de entrada
                Vertex s = start;
                if (!s.IsEntry)
                {
                    // intentar encontrar una intersección de entrada no visitada
                    var unvisitedEntry = allSubjectIntersections.FirstOrDefault(x => !x.Visited && x.IsEntry);
                    if (unvisitedEntry != null) s = unvisitedEntry;
                }

                if (s.Visited) continue;

                var poly = new List<PointF>();
                var current = s;
                bool onSubject = true;

                while (true)
                {
                    if (current == null) break;
                    if (current.Visited && current == s) break;

                    if (onSubject)
                    {
                        // recorrer subject hacia adelante hasta la próxima intersección
                        do
                        {
                            poly.Add(current.Point);
                            current = current.Next;
                        }
                        while (current != null && !current.IsIntersection);

                        if (current == null) break;

                        // current es una intersección
                        if (current.Visited) break;

                        current.Visited = true;
                        if (current.Neighbor != null) current.Neighbor.Visited = true;

                        // mover al nodo correspondiente en el polígono clip
                        current = current.Neighbor;
                        onSubject = false;
                    }
                    else
                    {
                        // recorrer clip hacia adelante hasta la próxima intersección
                        do
                        {
                            poly.Add(current.Point);
                            current = current.Next;
                        }
                        while (current != null && !current.IsIntersection);

                        if (current == null) break;

                        if (current.Visited) break;

                        current.Visited = true;
                        if (current.Neighbor != null) current.Neighbor.Visited = true;

                        current = current.Neighbor;
                        onSubject = true;
                    }

                    if (current == s) break;
                }

                var cleaned = RemoveDuplicatePoints(poly);
                if (cleaned.Count >= 3)
                {
                    results.Add(cleaned);
                }
            }

            return results;
        }

        #region Tipos y métodos auxiliares

        private class Vertex
        {
            public PointF Point;
            public Vertex Next;
            public Vertex Prev;
            public Vertex Neighbor;
            public bool IsIntersection;
            public bool Visited;
            public bool IsEntry; // verdadero para nodos de intersección que representan entrada
            public int EdgeIndex = -1;
            public float Alpha = 0f;

            public Vertex(PointF p)
            {
                Point = p;
            }

            public override string ToString() => Point.ToString();
        }

        private class IntersectionRecord
        {
            public int SubjectEdgeIndex;
            public int ClipEdgeIndex;
            public float T;
            public float U;
            public PointF Point;
            public Vertex SubjectNode;
            public Vertex ClipNode;
        }

        private static List<PointF> RemoveConsecutiveDuplicates(IList<PointF> poly)
        {
            var res = new List<PointF>();
            for (int i = 0; i < poly.Count; i++)
            {
                var p = poly[i];
                if (res.Count == 0) res.Add(p);
                else
                {
                    var last = res[res.Count - 1];
                    if (Math.Abs(last.X - p.X) > EPSILON || Math.Abs(last.Y - p.Y) > EPSILON)
                        res.Add(p);
                }
            }

            // comprobar duplicado de cierre
            if (res.Count > 1)
            {
                var first = res[0];
                var last = res[res.Count - 1];
                if (Math.Abs(first.X - last.X) < EPSILON && Math.Abs(first.Y - last.Y) < EPSILON)
                    res.RemoveAt(res.Count - 1);
            }

            return res;
        }

        private static bool HasAnyIntersection(IList<PointF> subj, IList<PointF> clp)
        {
            for (int i = 0; i < subj.Count; i++)
            {
                var p1 = subj[i];
                var p2 = subj[(i + 1) % subj.Count];
                for (int j = 0; j < clp.Count; j++)
                {
                    var q1 = clp[j];
                    var q2 = clp[(j + 1) % clp.Count];
                    if (SegmentIntersection(p1, p2, q1, q2, out _, out _, out _)) return true;
                }
            }
            return false;
        }

        private static List<Vertex> BuildVertexList(IList<PointF> poly)
        {
            var nodes = new List<Vertex>();
            for (int i = 0; i < poly.Count; i++) nodes.Add(new Vertex(poly[i]));

            int n = nodes.Count;
            for (int i = 0; i < n; i++)
            {
                nodes[i].Next = nodes[(i + 1) % n];
                nodes[i].Prev = nodes[(i - 1 + n) % n];
            }

            return nodes;
        }

        private static void InsertAfter(Vertex pos, Vertex node)
        {
            node.Next = pos.Next;
            node.Prev = pos;
            pos.Next.Prev = node;
            pos.Next = node;
        }

        private static Vertex FindNodeAtPoint(List<Vertex> nodes, PointF p)
        {
            foreach (var node in GetAllNodes(nodes))
            {
                if (Math.Abs(node.Point.X - p.X) < EPSILON && Math.Abs(node.Point.Y - p.Y) < EPSILON)
                    return node;
            }
            return null;
        }

        private static IEnumerable<Vertex> GetAllNodes(List<Vertex> startNodes)
        {
            var seen = new HashSet<Vertex>();
            foreach (var start in startNodes)
            {
                var cur = start;
                do
                {
                    if (cur == null) break;
                    if (seen.Contains(cur)) break;
                    seen.Add(cur);
                    yield return cur;
                    cur = cur.Next;
                }
                while (cur != start);
            }
        }

        // Punto-en-polígono por casting de rayos (par-impar) con detección en arista
        private static bool PointInPolygon(PointF p, IList<PointF> poly)
        {
            bool inside = false;
            int n = poly.Count;
            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                var pi = poly[i];
                var pj = poly[j];

                // Comprobación en arista
                if (IsPointOnSegment(p, pj, pi)) return true;

                bool intersect = ((pi.Y > p.Y) != (pj.Y > p.Y)) &&
                    (p.X < (pj.X - pi.X) * (p.Y - pi.Y) / (pj.Y - pi.Y + 0.0f) + pi.X);
                if (intersect) inside = !inside;
            }
            return inside;
        }

        private static bool IsPointOnSegment(PointF p, PointF a, PointF b)
        {
            // caja contenedora
            if (p.X < Math.Min(a.X, b.X) - EPSILON || p.X > Math.Max(a.X, b.X) + EPSILON ||
                p.Y < Math.Min(a.Y, b.Y) - EPSILON || p.Y > Math.Max(a.Y, b.Y) + EPSILON)
                return false;

            // producto cruzado
            float cross = (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X);
            return Math.Abs(cross) < EPSILON;
        }

        // Devuelve true si los segmentos p->p2 y q->q2 se intersectan. t es parámetro en p->p2, u en q->q2
        private static bool SegmentIntersection(PointF p, PointF p2, PointF q, PointF q2, out float t, out float u, out PointF intersection)
        {
            t = 0; u = 0; intersection = PointF.Empty;

            float r_x = p2.X - p.X;
            float r_y = p2.Y - p.Y;
            float s_x = q2.X - q.X;
            float s_y = q2.Y - q.Y;

            float denom = r_x * s_y - r_y * s_x;

            if (Math.Abs(denom) < EPSILON)
            {
                // paralelos
                return false;
            }

            float qp_x = q.X - p.X;
            float qp_y = q.Y - p.Y;

            t = (qp_x * s_y - qp_y * s_x) / denom;
            u = (qp_x * r_y - qp_y * r_x) / denom;

            if (t < -EPSILON || t > 1 + EPSILON || u < -EPSILON || u > 1 + EPSILON)
                return false;

            intersection = new PointF(p.X + t * r_x, p.Y + t * r_y);
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
