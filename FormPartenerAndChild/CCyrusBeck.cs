using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Provides the Cyrus-Beck line clipping algorithm for clipping line segments against a convex polygon.
    /// The polygon must be convex and is normalized to counter-clockwise winding if necessary.
    /// All public methods validate inputs and throw ArgumentException for invalid parameters.
    /// </summary>
    internal static class CCyrusBeck
    {
        private const float EPSILON = 1e-6f;

        /// <summary>
        /// Clips a single line segment against a convex polygon using the Cyrus-Beck algorithm.
        /// Returns true if a visible portion exists inside the polygon; clippedP0/clippedP1 contain the clipped segment.
        /// The polygon must contain at least 3 vertices and be convex. The method will attempt to normalize winding.
        /// </summary>
        /// <param name="polygon">Convex polygon vertices (any starting vertex). The method will accept CCW or CW and normalize to CCW.</param>
        /// <param name="p0">Line segment start point.</param>
        /// <param name="p1">Line segment end point.</param>
        /// <param name="clippedP0">Output clipped start point if returned true.</param>
        /// <param name="clippedP1">Output clipped end point if returned true.</param>
        /// <returns>True if the segment (or portion) lies inside the polygon.</returns>
        public static bool ClipSegment(IList<PointF> polygon, PointF p0, PointF p1, out PointF clippedP0, out PointF clippedP1)
        {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            if (polygon.Count < 3) throw new ArgumentException("Polygon must have at least 3 vertices.", nameof(polygon));
            if (Math.Abs(p0.X - p1.X) < EPSILON && Math.Abs(p0.Y - p1.Y) < EPSILON)
                throw new ArgumentException("Segment endpoints must not be identical.", nameof(p0));

            // Work on a copy to avoid mutating caller's list
            var poly = polygon.ToList();

            // Ensure polygon has CCW winding. If not, reverse.
            float area = SignedArea(poly);
            if (Math.Abs(area) < EPSILON)
                throw new ArgumentException("Polygon area is zero or degenerate.", nameof(polygon));

            if (area < 0)
            {
                poly.Reverse();
            }

            if (!IsConvex(poly))
            {
                throw new ArgumentException("Polygon must be convex.", nameof(polygon));
            }

            float tE = 0.0f; // maximum entering t
            float tL = 1.0f; // minimum leaving t

            var d = new PointF(p1.X - p0.X, p1.Y - p0.Y);

            for (int i = 0; i < poly.Count; i++)
            {
                var vi = poly[i];
                var vj = poly[(i + 1) % poly.Count];

                // Edge vector
                var edge = new PointF(vj.X - vi.X, vj.Y - vi.Y);

                // Outward normal for CCW polygon: (edge.Y, -edge.X)
                var n = new PointF(edge.Y, -edge.X);

                // Compute numerator and denominator
                var numerator = Dot(n, new PointF(vi.X - p0.X, vi.Y - p0.Y));
                var denominator = Dot(n, d);

                if (Math.Abs(denominator) < EPSILON)
                {
                    // Line is parallel to this edge
                    if (numerator < 0)
                    {
                        // Outside the polygon
                        clippedP0 = PointF.Empty;
                        clippedP1 = PointF.Empty;
                        return false;
                    }
                    else
                    {
                        // Parallel but inside or on the edge: no constraint from this edge
                        continue;
                    }
                }

                float t = numerator / denominator;

                if (denominator < 0)
                {
                    // Potentially entering
                    if (t > tE) tE = t;
                }
                else
                {
                    // denominator > 0: potentially leaving
                    if (t < tL) tL = t;
                }

                // Early rejection
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

            // Compute clipped points (clamp within [0,1])
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
        /// Clips multiple segments against the convex polygon and returns the list of visible segments.
        /// Degenerate input segments are ignored. Performs argument validation.
        /// </summary>
        /// <param name="polygon">Convex polygon vertices.</param>
        /// <param name="segments">Enumerable of segments (start,end).</param>
        /// <returns>List of clipped segments.</returns>
        public static List<(PointF, PointF)> ClipSegments(IList<PointF> polygon, IEnumerable<(PointF, PointF)> segments)
        {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            if (segments == null) throw new ArgumentNullException(nameof(segments));

            var result = new List<(PointF, PointF)>();

            foreach (var s in segments)
            {
                try
                {
                    // Skip degenerate
                    if (Math.Abs(s.Item1.X - s.Item2.X) < EPSILON && Math.Abs(s.Item1.Y - s.Item2.Y) < EPSILON)
                        continue;

                    if (ClipSegment(polygon, s.Item1, s.Item2, out var a, out var b))
                        result.Add((a, b));
                }
                catch (ArgumentException)
                {
                    // If polygon is invalid or segment invalid, skip this segment but continue processing others.
                    // This prevents the application from crashing on malformed input.
                    continue;
                }
            }

            return result;
        }

        #region Helper methods

        private static float Dot(PointF a, PointF b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        private static float SignedArea(IList<PointF> poly)
        {
            // Compute signed area using shoelace formula
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
            // A polygon is convex if all cross products of consecutive edges have the same sign
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

                if (Math.Abs(cross) < EPSILON) continue; // collinear

                int currentSign = cross > 0 ? 1 : -1;
                if (sign == 0) sign = currentSign;
                else if (sign != currentSign) return false;
            }

            return true;
        }

        #endregion
    }
}
