using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    internal class CFloodFillAlgorithm
    {
        // Realiza flood fill (relleno de inundación) a partir de un punto inicial
        // Usa BFS (breadth-first search) para rellenar el área
        public void FloodFill(Bitmap bitmap, int x, int y, Color fillColor)
        {
            if (bitmap == null || x < 0 || x >= bitmap.Width || y < 0 || y >= bitmap.Height)
                return;

            Color originalColor = bitmap.GetPixel(x, y);

            // Si el color original es igual al color de relleno, no hacer nada
            if (originalColor.ToArgb() == fillColor.ToArgb())
                return;

            Queue<(int, int)> queue = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            queue.Enqueue((x, y));
            visited.Add((x, y));

            while (queue.Count > 0)
            {
                var (cx, cy) = queue.Dequeue();

                // Verificar que esté dentro de los límites
                if (cx < 0 || cx >= bitmap.Width || cy < 0 || cy >= bitmap.Height)
                    continue;

                // Si el pixel actual tiene el color original, rellenarlo
                if (bitmap.GetPixel(cx, cy).ToArgb() == originalColor.ToArgb())
                {
                    bitmap.SetPixel(cx, cy, fillColor);

                    // Añadir vecinos a la cola
                    var neighbors = new[] { (cx + 1, cy), (cx - 1, cy), (cx, cy + 1), (cx, cy - 1) };
                    foreach (var (nx, ny) in neighbors)
                    {
                        if (!visited.Contains((nx, ny)))
                        {
                            visited.Add((nx, ny));
                            queue.Enqueue((nx, ny));
                        }
                    }
                }
            }
        }
    }
}
