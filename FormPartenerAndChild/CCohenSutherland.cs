using System;
using System.Drawing;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Implementación del algoritmo de recorte de líneas Cohen-Sutherland.
    /// Permite recortar una línea contra un rectángulo de recorte (clipping window).
    /// La implementación es modular y puede reemplazarse por otra que implemente la misma firma.
    /// </summary>
    internal class CCohenSutherland
    {
        // Bits para el outcode
        private const int INSIDE = 0; // 0000
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

        /// <summary>
        /// Intenta recortar la línea (p1-p2) usando la ventana de recorte <paramref name="clipRect"/>.
        /// Si la línea (o parte de ella) queda dentro de la ventana, se actualizan <paramref name="p1"/> y <paramref name="p2"/>
        /// con los extremos de la porción recortada y el método devuelve true.
        /// Si la línea está totalmente fuera, devuelve false y los puntos no se modifican.
        /// </summary>
        /// <param name="clipRect">Rectángulo de recorte (debe tener ancho y alto positivos).</param>
        /// <param name="p1">Punto 1 de la línea (se puede actualizar con el extremo recortado)</param>
        /// <param name="p2">Punto 2 de la línea (se puede actualizar con el extremo recortado)</param>
        /// <returns>True si hay una porción visible después del recorte; false en caso contrario.</returns>
        /// <exception cref="ArgumentException">Si el rectángulo de recorte tiene tamaño no positivo.</exception>
        public bool ClipLine(Rectangle clipRect, ref Point p1, ref Point p2)
        {
            if (clipRect.Width <= 0 || clipRect.Height <= 0)
                throw new ArgumentException("El rectángulo de recorte debe tener ancho y alto positivos.", nameof(clipRect));

            // Usamos dobles para evitar pérdida de precisión durante intersecciones.
            double x1 = p1.X;
            double y1 = p1.Y;
            double x2 = p2.X;
            double y2 = p2.Y;

            int outcode1 = ComputeOutCode((int)Math.Round(x1), (int)Math.Round(y1), clipRect);
            int outcode2 = ComputeOutCode((int)Math.Round(x2), (int)Math.Round(y2), clipRect);

            bool accept = false;
            int safety = 0; // prevención contra bucles infinitos

            while (true)
            {
                // Caso trivial: ambos dentro
                if ((outcode1 | outcode2) == 0)
                {
                    accept = true;
                    break;
                }
                // Caso trivial de rechazo: comparten fuera la misma región
                else if ((outcode1 & outcode2) != 0)
                {
                    break;
                }
                else
                {
                    // Seleccionar un punto que esté fuera
                    int outcodeOut = (outcode1 != 0) ? outcode1 : outcode2;

                    double x = 0.0, y = 0.0;

                    // Evitamos divisiones por cero comprobando diferencias
                    double dx = x2 - x1;
                    double dy = y2 - y1;

                    try
                    {
                        if ((outcodeOut & TOP) != 0)
                        {
                            // Intersección con la frontera superior (y = Top)
                            if (Math.Abs(dy) < 1e-9)
                            {
                                // Línea casi horizontal; tomar x aproximado
                                x = x1;
                            }
                            else
                            {
                                x = x1 + dx * (clipRect.Top - y1) / dy;
                            }
                            y = clipRect.Top;
                        }
                        else if ((outcodeOut & BOTTOM) != 0)
                        {
                            // Intersección con la frontera inferior (y = Bottom)
                            if (Math.Abs(dy) < 1e-9)
                            {
                                x = x1;
                            }
                            else
                            {
                                x = x1 + dx * (clipRect.Bottom - y1) / dy;
                            }
                            y = clipRect.Bottom;
                        }
                        else if ((outcodeOut & RIGHT) != 0)
                        {
                            // Intersección con la frontera derecha (x = Right)
                            if (Math.Abs(dx) < 1e-9)
                            {
                                y = y1;
                            }
                            else
                            {
                                y = y1 + dy * (clipRect.Right - x1) / dx;
                            }
                            x = clipRect.Right;
                        }
                        else if ((outcodeOut & LEFT) != 0)
                        {
                            // Intersección con la frontera izquierda (x = Left)
                            if (Math.Abs(dx) < 1e-9)
                            {
                                y = y1;
                            }
                            else
                            {
                                y = y1 + dy * (clipRect.Left - x1) / dx;
                            }
                            x = clipRect.Left;
                        }
                    }
                    catch (Exception)
                    {
                        // Si algo sale mal en la intersección, consideramos la línea como no visible
                        break;
                    }

                    // Reemplazar el punto fuera por la intersección calculada
                    if (outcodeOut == outcode1)
                    {
                        x1 = x;
                        y1 = y;
                        outcode1 = ComputeOutCode((int)Math.Round(x1), (int)Math.Round(y1), clipRect);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        outcode2 = ComputeOutCode((int)Math.Round(x2), (int)Math.Round(y2), clipRect);
                    }
                }

                // Prevención contra bucles infinitos por datos extraños
                safety++;
                if (safety > 1000)
                    break;
            }

            if (accept)
            {
                p1 = new Point((int)Math.Round(x1), (int)Math.Round(y1));
                p2 = new Point((int)Math.Round(x2), (int)Math.Round(y2));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calcula el outcode para un punto (x,y) relativo a un rectángulo.
        /// Las coordenadas de los bordes son inclusivas (Right = Left + Width - 1).
        /// </summary>
        private static int ComputeOutCode(int x, int y, Rectangle rect)
        {
            int code = INSIDE;

            if (x < rect.Left)
                code |= LEFT;
            else if (x > rect.Right)
                code |= RIGHT;

            if (y < rect.Top)
                code |= TOP;
            else if (y > rect.Bottom)
                code |= BOTTOM;

            return code;
        }
    }
}
