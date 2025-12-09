using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Utility class that implements the Liang-Barsky line clipping algorithm.
    /// The methods are static and return whether the line was accepted and the clipped points when applicable.
    /// All public methods validate inputs and throw ArgumentException for invalid parameters.
    /// </summary>
    internal static class CLiangBarsky
    {
        /// <summary>
        /// Attempts to clip the line from p0 to p1 against the axis-aligned clipping rectangle.
        /// Returns true if a visible segment exists inside the rectangle; clippedP0/clippedP1 are set to the clipped segment.
        /// If the line lies completely outside, returns false and clipped points are undefined.
        /// </summary>
        /// <param name="clipRect">Clipping rectangle (must have positive Width and Height).</param>
        /// <param name="p0">Line start point.</param>
        /// <param name="p1">Line end point.</param>
        /// <param name="clippedP0">Clipped segment start (output).</param>
        /// <param name="clippedP1">Clipped segment end (output).</param>
        /// <returns>True if there is a visible (possibly degenerate) segment inside the rectangle.</returns>
        public static bool LiangBarskyClip(RectangleF clipRect, PointF p0, PointF p1, out PointF clippedP0, out PointF clippedP1)
        {
            // Validate clipping rectangle
            if (clipRect.Width <= 0 || clipRect.Height <= 0)
            {
                throw new ArgumentException("Clipping rectangle must have positive width and height.", nameof(clipRect));
            }

            // Initialization
            float t0 = 0.0f; // entering parameter
            float t1 = 1.0f; // leaving parameter
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;

            // Define an inner function to process each edge
            bool ClipTest(float p, float q, ref float tE, ref float tL)
            {
                // p: direction component, q: relative distance to boundary
                if (Math.Abs(p) < 1e-6f)
                {
                    // Line is parallel to this clipping edge
                    if (q < 0)
                        return false; // parallel and outside
                    else
                        return true; // parallel and inside
                }

                float r = q / p;
                if (p < 0)
                {
                    // potential entering point
                    if (r > tL) return false; // outside
                    if (r > tE) tE = r;
                }
                else if (p > 0)
                {
                    // potential leaving point
                    if (r < tE) return false; // outside
                    if (r < tL) tL = r;
                }
                return true;
            }

            // Left edge: x >= clipRect.Left
            if (!ClipTest(-dx, p0.X - clipRect.Left, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Right edge: x <= clipRect.Right
            if (!ClipTest(dx, clipRect.Right - p0.X, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Bottom edge: y >= clipRect.Top (note: typical screen coords have y downwards)
            if (!ClipTest(-dy, p0.Y - clipRect.Top, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Top edge: y <= clipRect.Bottom
            if (!ClipTest(dy, clipRect.Bottom - p0.Y, ref t0, ref t1))
            {
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            if (t1 < t0)
            {
                // No visible portion
                clippedP0 = PointF.Empty;
                clippedP1 = PointF.Empty;
                return false;
            }

            // Compute clipped points
            clippedP0 = new PointF(p0.X + t0 * dx, p0.Y + t0 * dy);
            clippedP1 = new PointF(p0.X + t1 * dx, p0.Y + t1 * dy);
            return true;
        }

        /// <summary>
        /// Clips a list of line segments against the clipping rectangle and returns a list of clipped segments.
        /// Invalid lines (zero-length) are ignored. The method performs argument validation.
        /// </summary>
        /// <param name="clipRect">Clipping rectangle.</param>
        /// <param name="segments">Enumerable of line segments (start,end).</param>
        /// <returns>List of clipped segments that lie (at least partially) inside the clipping rectangle.</returns>
        public static List<(PointF, PointF)> ClipSegments(RectangleF clipRect, IEnumerable<(PointF, PointF)> segments)
        {
            if (segments == null) throw new ArgumentNullException(nameof(segments));
            if (clipRect.Width <= 0 || clipRect.Height <= 0)
                throw new ArgumentException("Clipping rectangle must have positive width and height.", nameof(clipRect));

            var result = new List<(PointF, PointF)>();

            foreach (var seg in segments)
            {
                // Skip degenerate segments
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
