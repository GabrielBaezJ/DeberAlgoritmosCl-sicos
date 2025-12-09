using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Implements the Sutherland–Hodgman polygon clipping algorithm.
    /// Clips a subject polygon against a (preferably convex) clipping polygon.
    /// The implementation validates inputs and will throw informative exceptions for invalid data.
    /// </summary>
    internal static class CSutherlandHodgman
    {
        private const float EPSILON = 1e-6f;

        /// <summary>
        /// Clips the subject polygon against the clip polygon using the Sutherland–Hodgman algorithm.
        /// Both polygons must contain at least 3 vertices. The clip polygon should be convex for correct results.
        /// Returns a new polygon representing the clipped result (may be empty if no overlap).
        /// </summary>
        /// <param name="subject">Subject polygon vertices (may be concave).</param>
        /// <param name="clip">Clip polygon vertices (should be convex).</param>
        /// <returns>Clipped polygon vertices (possibly empty).</returns>
        public static List<PointF> ClipPolygon(IList<PointF> subject, IList<PointF> clip)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            if (clip == null) throw new ArgumentNullException(nameof(clip));
            if (subject.Count < 3) throw new ArgumentException("Subject polygon must have at least 3 vertices.", nameof(subject));
            if (clip.Count < 3) throw new ArgumentException("Clip polygon must have at least 3 vertices.", nameof(clip));

            // Prefer clipping polygon to be convex, warn if not (throwing to enforce stricter behavior)
            if (!IsConvex(clip))
                throw new ArgumentException("Clip polygon must be convex for reliable results.", nameof(clip));

            // Work on copies
            var outputList = subject.ToList();

            // Iterate over clip edges
            for (int i = 0; i < clip.Count; i++)
            {
                var inputList = outputList.ToList();
                outputList.Clear();

                var A = clip[i];
                var B = clip[(i + 1) % clip.Count];

                if (inputList.Count == 0) break; // nothing to clip

                PointF S = inputList[inputList.Count - 1]; // last vertex

                foreach (var E in inputList)
                {
                    bool Ein = IsInside(A, B, E);
                    bool Sin = IsInside(A, B, S);

                    if (Sin && Ein)
                    {
                        // both inside
                        outputList.Add(E);
                    }
                    else if (Sin && !Ein)
                    {
                        // leaving -- add intersection
                        if (TryIntersect(S, E, A, B, out var ip))
                            outputList.Add(ip);
                    }
                    else if (!Sin && Ein)
                    {
                        // entering -- add intersection then E
                        if (TryIntersect(S, E, A, B, out var ip))
                            outputList.Add(ip);
                        outputList.Add(E);
                    }
                    // else both outside -> do nothing

                    S = E;
                }
            }

            // Remove nearly-duplicate consecutive points
            outputList = RemoveDuplicatePoints(outputList);

            return outputList;
        }

        #region Geometry helpers

        // Determines if point P is inside the half-plane defined by edge AB (assuming clip polygon is CCW)
        private static bool IsInside(PointF A, PointF B, PointF P)
        {
            // For clip edge AB, the inside is to the left of the directed edge (A->B) for CCW polygon
            var cross = (B.X - A.X) * (P.Y - A.Y) - (B.Y - A.Y) * (P.X - A.X);
            return cross >= -EPSILON; // allow points on edge
        }

        // Attempts to compute intersection between segment S->E and the infinite line AB.
        // Returns false if segments are parallel and no intersection.
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
                // Parallel or collinear
                return false;
            }

            // Solve for parameters: S + t*(E-S) intersects A + u*(B-A)
            float t = ((A.X - S.X) * dyAB - (A.Y - S.Y) * dxAB) / denom;

            intersection = new PointF(S.X + t * dxSE, S.Y + t * dySE);
            return true;
        }

        // Check polygon convexity (simple O(n) test)
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

            // Close check: if first and last duplicate, remove last
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
