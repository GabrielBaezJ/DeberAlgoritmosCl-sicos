using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Clase que implementa el algoritmo de Boundary Fill (Relleno de Fronteras)
    /// 
    /// El algoritmo Boundary Fill rellena el interior de una región buscando un color de frontera específico.
    /// A diferencia del Flood Fill que se expande desde un color inicial, Boundary Fill se detiene
    /// al encontrar el color de la frontera, permitiendo rellenar regiones cerradas.
    /// </summary>
    internal class CBoundaryFill
    {
        /// <summary>
        /// Enumeración de variantes del algoritmo
        /// </summary>
        public enum BoundaryFillVariant
        {
            /// <summary>Conectividad de 4 vecinos (arriba, abajo, izquierda, derecha)</summary>
            FourConnected = 4,
            /// <summary>Conectividad de 8 vecinos (incluye diagonales)</summary>
            EightConnected = 8,
            /// <summary>Variante mejorada con optimización de memoria</summary>
            OptimizedEightConnected = 12
        }

        private BoundaryFillVariant currentVariant = BoundaryFillVariant.FourConnected;

        /// <summary>
        /// Obtiene o establece la variante actual del algoritmo
        /// </summary>
        public BoundaryFillVariant CurrentVariant
        {
            get => currentVariant;
            set => currentVariant = value;
        }

        /// <summary>
        /// Realiza un relleno de frontera con conectividad de 4 vecinos (arriba, abajo, izquierda, derecha)
        /// </summary>
        /// <param name="bitmap">Bitmap a rellenar (no puede ser nulo)</param>
        /// <param name="x">Coordenada X del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="y">Coordenada Y del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="fillColor">Color de relleno (no puede ser nulo)</param>
        /// <param name="boundaryColor">Color de la frontera que detiene el relleno (no puede ser nulo)</param>
        /// <returns>Número de píxeles rellenados</returns>
        /// <exception cref="ArgumentNullException">Si bitmap, fillColor o boundaryColor son nulos</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si las coordenadas están fuera de los límites</exception>
        public int BoundaryFillFourConnected(Bitmap bitmap, int x, int y, Color fillColor, Color boundaryColor)
        {
            ValidateInput(bitmap, x, y, fillColor, boundaryColor);

            int pixelsFilled = 0;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            queue.Enqueue((x, y));
            visited.Add((x, y));

            while (queue.Count > 0)
            {
                var (cx, cy) = queue.Dequeue();

                try
                {
                    Color currentColor = bitmap.GetPixel(cx, cy);

                    // Si el pixel actual es la frontera o ya fue rellenado, continuar
                    if (currentColor.ToArgb() == boundaryColor.ToArgb() || currentColor.ToArgb() == fillColor.ToArgb())
                        continue;

                    // Rellenar el pixel actual
                    bitmap.SetPixel(cx, cy, fillColor);
                    pixelsFilled++;

                    // Añadir los 4 vecinos (arriba, abajo, izquierda, derecha)
                    var neighbors = new[] 
                    { 
                        (cx + 1, cy),      // derecha
                        (cx - 1, cy),      // izquierda
                        (cx, cy + 1),      // abajo
                        (cx, cy - 1)       // arriba
                    };

                    foreach (var (nx, ny) in neighbors)
                    {
                        if (IsValidCoordinate(bitmap, nx, ny) && !visited.Contains((nx, ny)))
                        {
                            visited.Add((nx, ny));
                            queue.Enqueue((nx, ny));
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al rellenar píxel ({cx}, {cy}): {ex.Message}");
                    continue;
                }
            }

            return pixelsFilled;
        }

        /// <summary>
        /// Realiza un relleno de frontera con conectividad de 8 vecinos (incluye diagonales)
        /// </summary>
        /// <param name="bitmap">Bitmap a rellenar (no puede ser nulo)</param>
        /// <param name="x">Coordenada X del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="y">Coordenada Y del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="fillColor">Color de relleno (no puede ser nulo)</param>
        /// <param name="boundaryColor">Color de la frontera que detiene el relleno (no puede ser nulo)</param>
        /// <returns>Número de píxeles rellenados</returns>
        /// <exception cref="ArgumentNullException">Si bitmap, fillColor o boundaryColor son nulos</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si las coordenadas están fuera de los límites</exception>
        public int BoundaryFillEightConnected(Bitmap bitmap, int x, int y, Color fillColor, Color boundaryColor)
        {
            ValidateInput(bitmap, x, y, fillColor, boundaryColor);

            int pixelsFilled = 0;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            queue.Enqueue((x, y));
            visited.Add((x, y));

            while (queue.Count > 0)
            {
                var (cx, cy) = queue.Dequeue();

                try
                {
                    Color currentColor = bitmap.GetPixel(cx, cy);

                    // Si el pixel actual es la frontera o ya fue rellenado, continuar
                    if (currentColor.ToArgb() == boundaryColor.ToArgb() || currentColor.ToArgb() == fillColor.ToArgb())
                        continue;

                    // Rellenar el pixel actual
                    bitmap.SetPixel(cx, cy, fillColor);
                    pixelsFilled++;

                    // Añadir los 8 vecinos (incluyendo diagonales)
                    var neighbors = new[] 
                    { 
                        (cx + 1, cy),      // derecha
                        (cx - 1, cy),      // izquierda
                        (cx, cy + 1),      // abajo
                        (cx, cy - 1),      // arriba
                        (cx + 1, cy + 1),  // diagonal inferior-derecha
                        (cx - 1, cy - 1),  // diagonal superior-izquierda
                        (cx + 1, cy - 1),  // diagonal superior-derecha
                        (cx - 1, cy + 1)   // diagonal inferior-izquierda
                    };

                    foreach (var (nx, ny) in neighbors)
                    {
                        if (IsValidCoordinate(bitmap, nx, ny) && !visited.Contains((nx, ny)))
                        {
                            visited.Add((nx, ny));
                            queue.Enqueue((nx, ny));
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al rellenar píxel ({cx}, {cy}): {ex.Message}");
                    continue;
                }
            }

            return pixelsFilled;
        }

        /// <summary>
        /// Realiza un relleno de frontera optimizado con conectividad de 8 vecinos
        /// Utiliza una cola de escaneo de líneas para mejor rendimiento en imágenes grandes
        /// </summary>
        /// <param name="bitmap">Bitmap a rellenar (no puede ser nulo)</param>
        /// <param name="x">Coordenada X del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="y">Coordenada Y del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="fillColor">Color de relleno (no puede ser nulo)</param>
        /// <param name="boundaryColor">Color de la frontera que detiene el relleno (no puede ser nulo)</param>
        /// <returns>Número de píxeles rellenados</returns>
        /// <exception cref="ArgumentNullException">Si bitmap, fillColor o boundaryColor son nulos</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si las coordenadas están fuera de los límites</exception>
        public int BoundaryFillOptimized(Bitmap bitmap, int x, int y, Color fillColor, Color boundaryColor)
        {
            ValidateInput(bitmap, x, y, fillColor, boundaryColor);

            int pixelsFilled = 0;
            Queue<(int, int)> scanLineQueue = new Queue<(int, int)>();
            bool[] visited = new bool[bitmap.Width * bitmap.Height];

            scanLineQueue.Enqueue((x, y));
            int index = y * bitmap.Width + x;
            visited[index] = true;

            while (scanLineQueue.Count > 0)
            {
                var (cx, cy) = scanLineQueue.Dequeue();

                try
                {
                    // Rellenar línea desde el punto inicial hacia la izquierda
                    int fillX = cx;
                    while (fillX >= 0 && bitmap.GetPixel(fillX, cy).ToArgb() != boundaryColor.ToArgb() 
                           && bitmap.GetPixel(fillX, cy).ToArgb() != fillColor.ToArgb())
                    {
                        bitmap.SetPixel(fillX, cy, fillColor);
                        pixelsFilled++;
                        fillX--;
                    }

                    // Rellenar línea desde el punto inicial hacia la derecha
                    fillX = cx + 1;
                    while (fillX < bitmap.Width && bitmap.GetPixel(fillX, cy).ToArgb() != boundaryColor.ToArgb() 
                           && bitmap.GetPixel(fillX, cy).ToArgb() != fillColor.ToArgb())
                    {
                        bitmap.SetPixel(fillX, cy, fillColor);
                        pixelsFilled++;
                        fillX++;
                    }

                    // Procesar filas adyacentes
                    if (cy + 1 < bitmap.Height)
                    {
                        for (int px = Math.Max(0, cx - 1); px < Math.Min(bitmap.Width, cx + 2); px++)
                        {
                            int pidx = (cy + 1) * bitmap.Width + px;
                            if (!visited[pidx])
                            {
                                visited[pidx] = true;
                                scanLineQueue.Enqueue((px, cy + 1));
                            }
                        }
                    }

                    if (cy - 1 >= 0)
                    {
                        for (int px = Math.Max(0, cx - 1); px < Math.Min(bitmap.Width, cx + 2); px++)
                        {
                            int pidx = (cy - 1) * bitmap.Width + px;
                            if (!visited[pidx])
                            {
                                visited[pidx] = true;
                                scanLineQueue.Enqueue((px, cy - 1));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al rellenar escaneo de línea en ({cx}, {cy}): {ex.Message}");
                    continue;
                }
            }

            return pixelsFilled;
        }

        /// <summary>
        /// Realiza un relleno de frontera automático usando la variante configurada actualmente
        /// </summary>
        /// <param name="bitmap">Bitmap a rellenar (no puede ser nulo)</param>
        /// <param name="x">Coordenada X del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="y">Coordenada Y del punto inicial (debe estar dentro del bitmap)</param>
        /// <param name="fillColor">Color de relleno (no puede ser nulo)</param>
        /// <param name="boundaryColor">Color de la frontera que detiene el relleno (no puede ser nulo)</param>
        /// <returns>Número de píxeles rellenados</returns>
        public int BoundaryFill(Bitmap bitmap, int x, int y, Color fillColor, Color boundaryColor)
        {
            switch (currentVariant)
            {
                case BoundaryFillVariant.FourConnected:
                    return BoundaryFillFourConnected(bitmap, x, y, fillColor, boundaryColor);
                case BoundaryFillVariant.EightConnected:
                    return BoundaryFillEightConnected(bitmap, x, y, fillColor, boundaryColor);
                case BoundaryFillVariant.OptimizedEightConnected:
                    return BoundaryFillOptimized(bitmap, x, y, fillColor, boundaryColor);
                default:
                    throw new InvalidOperationException("Variante no soportada");
            }
        }

        /// <summary>
        /// Valida que las coordenadas estén dentro de los límites del bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap a validar</param>
        /// <param name="x">Coordenada X a validar</param>
        /// <param name="y">Coordenada Y a validar</param>
        /// <returns>True si las coordenadas son válidas</returns>
        private bool IsValidCoordinate(Bitmap bitmap, int x, int y)
        {
            return x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height;
        }

        /// <summary>
        /// Valida que los parámetros de entrada sean válidos
        /// </summary>
        /// <param name="bitmap">Bitmap a validar</param>
        /// <param name="x">Coordenada X a validar</param>
        /// <param name="y">Coordenada Y a validar</param>
        /// <param name="fillColor">Color de relleno a validar</param>
        /// <param name="boundaryColor">Color de frontera a validar</param>
        /// <exception cref="ArgumentNullException">Si los parámetros no nulos son nulos</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si las coordenadas están fuera de rango</exception>
        private void ValidateInput(Bitmap bitmap, int x, int y, Color fillColor, Color boundaryColor)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap), "El bitmap no puede ser nulo");

            if (x < 0 || x >= bitmap.Width || y < 0 || y >= bitmap.Height)
                throw new ArgumentOutOfRangeException($"Las coordenadas ({x}, {y}) están fuera de los límites del bitmap (0-{bitmap.Width-1}, 0-{bitmap.Height-1})");

            // Los colores nunca son nulos en C#, pero podemos validar otros aspectos
        }
    }
}
