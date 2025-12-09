using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Implementa el algoritmo de recorte Greiner–Hormann para la intersección de polígonos.
    /// Esta implementación se centra en la operación de intersección y devuelve cero o más polígonos
    /// resultantes del recorte del polígono sujeto por el polígono de recorte.
    /// El módulo es defensivo: valida entradas, tolera vértices degenerados y proporciona excepciones informativas.
    /// </summary>
    internal static class CGreinerHormann
    {
        private const float EPSILON = 1e-6f;

        /// <summary>
        /// Recorta el polígono sujeto contra el polígono de recorte y devuelve los polígonos de intersección.
        /// Ambos polígonos deben tener al menos 3 vértices. El método limpiará la entrada e intentará manejar
        /// casos degenerados o limítrofes. El polígono de recorte no necesita ser convexo.
        /// </summary>
        /// <param name="subject">Vértices del polígono sujeto.</param>
        /// <param name="clip">Vértices del polígono de recorte.</param>
        /// <returns>Lista de polígonos (cada uno como lista de PointF) que representan la intersección (puede estar vacía).</returns>
        public static List<List<PointF>> Intersect(IList<PointF> subject, IList<PointF> clip)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (subject.Count < 3) throw new ArgumentException("El polígono sujeto debe tener al menos 3 vértices.", nameof(subject));
            if (clip.Count < 3) throw new ArgumentException("El polígono de recorte debe tener al menos 3 vértices.", nameof(clip));

            var subj = RemoveConsecutiveDuplicates(subject).ToList();
            var clp = RemoveConsecutiveDuplicates(clip).ToList();

            if (subj.Count < 3) throw new ArgumentException("El polígono sujeto es degenerado después de limpiar.", nameof(subject));
            if (clp.Count < 3) throw new ArgumentException("El polígono de recorte es degenerado después de limpiar.", nameof(clip));

            // Pruebas rápidas: si no hay intersecciones, o el sujeto está completamente dentro o fuera
            bool anyInter = HasAnyIntersection(subj, clp);
            if (!anyInter)
            {
                // Si el primer punto del sujeto está dentro del clip, el sujeto entero está dentro
                if (PointInPolygon(subj[0], clp))
                {
                    return new List<List<PointF>> { subj };
                }
                else
                {
                    return new List<List<PointF>>();
                }
            }

            // Construir listas de nodos
            var subjNodes = BuildVertexList(subj);
            var clipNodes = BuildVertexList(clp);

            // Buscar intersecciones y crear registros
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
                        // almacenar registro
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

            // Insertar intersecciones en las listas enlazadas subject y clip ordenadas por el parámetro
            foreach (var grp in intersections.GroupBy(x => x.SubjectEdgeIndex))
            {
                int edge = grp.Key;
                var ordered = grp.OrderBy(x => x.T).ToList();
                var startNode = subjNodes[edge];
                var insertAfter = startNode;
                foreach (var rec in ordered)
                {
                    if (rec.T <= EPSILON)
                    {
                        rec.SubjectNode = startNode;
                        rec.SubjectNode.IsIntersection = true;
                    }
                    else if (rec.T >= 1 - EPSILON)
                    {
                        rec.SubjectNode = subjNodes[(edge + 1) % subjNodes.Count];
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

            foreach (var grp in intersections.GroupBy(x => x.ClipEdgeIndex))
            {
                int edge = grp.Key;
                var ordered = grp.OrderBy(x => x.U).ToList();
                var startNode = clipNodes[edge];
                var insertAfter = startNode;
                foreach (var rec in ordered)
                {
                    if (rec.U <= EPSILON)
                    {
                        rec.ClipNode = startNode;
                        rec.ClipNode.IsIntersection = true;
                    }
                    else if (rec.U >= 1 - EPSILON)
                    {
                        rec.ClipNode = clipNodes[(edge + 1) % clipNodes.Count];
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

            // Enlazar los nodos de intersección correspondientes entre subject y clip
            foreach (var rec in intersections)
            {
                if (rec.SubjectNode == null)
                    rec.SubjectNode = FindNodeAtPoint(subjNodes, rec.Point);
                if (rec.ClipNode == null)
                    rec.ClipNode = FindNodeAtPoint(clipNodes, rec.Point);

                if (rec.SubjectNode == null || rec.ClipNode == null) continue;

                rec.SubjectNode.Neighbor = rec.ClipNode;
                rec.ClipNode.Neighbor = rec.SubjectNode;
                rec.SubjectNode.Alpha = rec.T;
                rec.ClipNode.Alpha = rec.U;
            }

            // Determinar clasificación entrada/salida para intersecciones en subject
            foreach (var rec in intersections)
            {
                var sn = rec.SubjectNode;
                if (sn == null || !sn.IsIntersection) continue;

                // Avanzar ligeramente a lo largo de la arista subject tras la intersección y probar punto-en-polígono
                var edgeIndex = rec.SubjectEdgeIndex;
                var pA = subj[edgeIndex];
                var pB = subj[(edgeIndex + 1) % subj.Count];
                var dir = new PointF(pB.X - pA.X, pB.Y - pA.Y);
                var len = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                if (len < EPSILON) continue;
                dir.X /= len; dir.Y /= len;
                var after = new PointF(rec.Point.X + dir.X * 1e-3f, rec.Point.Y + dir.Y * 1e-3f);
                sn.IsEntry = !PointInPolygon(after, clp); // si el punto tras la intersección está fuera del clip, entonces es entrada
            }

            // Recopilar todos los nodos de intersección del subject
            var allSubInter = GetAllNodes(subjNodes).Where(v => v.IsIntersection).ToList();
            var results = new List<List<PointF>>();

            foreach (var start in allSubInter)
            {
                if (start.Visited) continue;
                var current = start;
                var polygon = new List<PointF>();

                // Para Greiner–Hormann, comenzar en una intersección y seguir el polígono, cambiando en intersecciones
                do
                {
                    if (current == null) break;
                    if (current.Visited) break;

                    polygon.Add(current.Point);
                    current.Visited = true;

                    if (current.IsIntersection)
                    {
                        // cambiar al vecino y continuar la traversía en la lista vecina
                        current = current.Neighbor;
                        if (current == null) break;
                        // mover al siguiente en ese polígono
                        current = current.Next;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
                while (current != null && current != start);

                var cleaned = RemoveDuplicatePoints(polygon);
                if (cleaned.Count >= 3)
                    results.Add(cleaned);
            }

            return results;
        }

        #region Ayudantes y tipos

        private class Vertex
        {
            public PointF Point;
            public Vertex Next;
            public Vertex Prev;
            public Vertex Neighbor;
            public bool IsIntersection;
            public bool Visited;
            public bool IsEntry;
            public float Alpha;

            public Vertex(PointF p) { Point = p; }
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
            foreach (var v in GetAllNodes(nodes))
            {
                if (Math.Abs(v.Point.X - p.X) < EPSILON && Math.Abs(v.Point.Y - p.Y) < EPSILON) return v;
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

        private static bool PointInPolygon(PointF p, IList<PointF> poly)
        {
            bool inside = false;
            int n = poly.Count;
            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                var pi = poly[i];
                var pj = poly[j];
                if (IsPointOnSegment(p, pj, pi)) return true;
                bool intersect = ((pi.Y > p.Y) != (pj.Y > p.Y)) &&
                    (p.X < (pj.X - pi.X) * (p.Y - pi.Y) / (pj.Y - pi.Y + 0.0f) + pi.X);
                if (intersect) inside = !inside;
            }
            return inside;
        }

        private static bool IsPointOnSegment(PointF p, PointF a, PointF b)
        {
            if (p.X < Math.Min(a.X, b.X) - EPSILON || p.X > Math.Max(a.X, b.X) + EPSILON ||
                p.Y < Math.Min(a.Y, b.Y) - EPSILON || p.Y > Math.Max(a.Y, b.Y) + EPSILON)
                return false;
            float cross = (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X);
            return Math.Abs(cross) < EPSILON;
        }

        private static bool SegmentIntersection(PointF p, PointF p2, PointF q, PointF q2, out float t, out float u, out PointF intersection)
        {
            t = 0; u = 0; intersection = PointF.Empty;
            float r_x = p2.X - p.X;
            float r_y = p2.Y - p.Y;
            float s_x = q2.X - q.X;
            float s_y = q2.Y - q.Y;
            float denom = r_x * s_y - r_y * s_x;
            if (Math.Abs(denom) < EPSILON) return false;
            float qp_x = q.X - p.X;
            float qp_y = q.Y - p.Y;
            t = (qp_x * s_y - qp_y * s_x) / denom;
            u = (qp_x * r_y - qp_y * r_x) / denom;
            if (t < -EPSILON || t > 1 + EPSILON || u < -EPSILON || u > 1 + EPSILON) return false;
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
