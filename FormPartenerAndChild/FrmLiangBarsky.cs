using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Formulario para interactuar con el algoritmo de recorte Liang-Barsky.
    /// Permite dibujar líneas, definir un rectángulo de recorte y aplicar el algoritmo.
    /// La interacción se realiza con el ratón sobre el área de dibujo.
    /// </summary>
    public partial class FrmLiangBarsky : Form
    {
        // Modo de interacción del usuario
        private enum DrawMode { None, DrawLine, DrawClipRect }

        private DrawMode _currentMode = DrawMode.DrawLine;

        // Colección de segmentos definidos por el usuario
        private readonly List<(PointF, PointF)> _lines = new List<(PointF, PointF)>();

        // Resultado de recorte
        private readonly List<(PointF, PointF)> _clippedLines = new List<(PointF, PointF)>();

        // Rectángulo de recorte
        private RectangleF _clipRect;
        private bool _clipRectDefined = false;

        // Interacción de dibujo
        private bool _isDrawing = false;
        private PointF _startPoint;
        private PointF _endPoint;

        public FrmLiangBarsky()
        {
            InitializeComponent();
            InitializeCustom();
        }

        /// <summary>
        /// Inicialización adicional no generada por el diseñador.
        /// Establece valores iniciales y habilita doble buffer en el panel para mejorar el dibujo.
        /// </summary>
        private void InitializeCustom()
        {
            try
            {
                rbDrawLine.Checked = true;
                lblStatus.Text = "Modo: Dibujar línea";

                // Intentar activar DoubleBuffered en el panel mediante reflexión
                try
                {
                    var prop = typeof(Panel).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (prop != null)
                        prop.SetValue(pnlCanvas, true, null);
                }
                catch
                {
                    // No fatal: si no se puede activar, se sigue adelante sin excepción.
                }
            }
            catch
            {
                // Si ocurre algún error en la inicialización visual, no cerramos la app.
            }
        }

        /// <summary>
        /// Inicia la operación de dibujo (línea o rectángulo) en la posición del ratón.
        /// </summary>
        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isDrawing = true;
            _startPoint = e.Location;
            _endPoint = e.Location;
            pnlCanvas.Capture = true;
        }

        /// <summary>
        /// Actualiza la operación de dibujo mientras el usuario arrastra el ratón.
        /// </summary>
        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing) return;
            _endPoint = e.Location;
            lblStatus.Text = $"Dibujando: {(int)_startPoint.X},{(int)_startPoint.Y} -> {(int)_endPoint.X},{(int)_endPoint.Y}";
            pnlCanvas.Invalidate();
        }

        /// <summary>
        /// Finaliza la operación de dibujo: agrega la línea o define el rectángulo de recorte.
        /// Se realizan validaciones para evitar segmentos degenerados o rectángulos inválidos.
        /// </summary>
        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isDrawing) return;
            _isDrawing = false;
            pnlCanvas.Capture = false;
            _endPoint = e.Location;

            try
            {
                if (_currentMode == DrawMode.DrawLine)
                {
                    // Evitar segmentos demasiado pequeños
                    if (Math.Abs(_startPoint.X - _endPoint.X) < 1e-3f && Math.Abs(_startPoint.Y - _endPoint.Y) < 1e-3f)
                    {
                        lblStatus.Text = "Línea demasiado corta, ignorada.";
                    }
                    else
                    {
                        _lines.Add((_startPoint, _endPoint));
                        lblStatus.Text = $"Líneas: {_lines.Count}";
                        _clippedLines.Clear(); // invalidar resultados previos
                    }
                }
                else if (_currentMode == DrawMode.DrawClipRect)
                {
                    // Normalizar rectángulo y validar tamaño mínimo
                    float left = Math.Min(_startPoint.X, _endPoint.X);
                    float top = Math.Min(_startPoint.Y, _endPoint.Y);
                    float right = Math.Max(_startPoint.X, _endPoint.X);
                    float bottom = Math.Max(_startPoint.Y, _endPoint.Y);
                    float width = right - left;
                    float height = bottom - top;

                    if (width < 2 || height < 2)
                    {
                        _clipRectDefined = false;
                        MessageBox.Show(this, "El rectángulo es demasiado pequeño.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        _clipRect = RectangleF.FromLTRB(left, top, right, bottom);
                        _clipRectDefined = true;
                        lblStatus.Text = $"Rectángulo definido: {(int)left},{(int)top} - {(int)right},{(int)bottom}";
                        _clippedLines.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error al finalizar la operación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pnlCanvas.Invalidate();
        }

        /// <summary>
        /// Dibuja el contenido: líneas del usuario, rectángulo de recorte y resultados recortados.
        /// También muestra la entidad que se está dibujando en tiempo real (rubber-band).
        /// </summary>
        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Dibujar líneas originales en negro
            using (var pen = new Pen(Color.Black, 2f))
            {
                foreach (var seg in _lines)
                {
                    g.DrawLine(pen, seg.Item1, seg.Item2);
                }
            }

            // Dibujar resultados recortados en rojo
            using (var penClipped = new Pen(Color.Red, 2f))
            {
                foreach (var seg in _clippedLines)
                {
                    g.DrawLine(penClipped, seg.Item1, seg.Item2);
                }
            }

            // Dibujar rectángulo de recorte si existe (azul, línea punteada)
            if (_clipRectDefined)
            {
                using (var penRect = new Pen(Color.Blue, 2f))
                {
                    penRect.DashStyle = DashStyle.Dash;
                    g.DrawRectangle(penRect, _clipRect.X, _clipRect.Y, _clipRect.Width, _clipRect.Height);
                }
            }

            // Dibujar la figura en curso (rubber-band) con estilo punteado
            if (_isDrawing)
            {
                using (var pen = new Pen(Color.DarkGreen, 1.5f))
                {
                    pen.DashStyle = DashStyle.Dash;
                    if (_currentMode == DrawMode.DrawLine)
                    {
                        g.DrawLine(pen, _startPoint, _endPoint);
                    }
                    else if (_currentMode == DrawMode.DrawClipRect)
                    {
                        float left = Math.Min(_startPoint.X, _endPoint.X);
                        float top = Math.Min(_startPoint.Y, _endPoint.Y);
                        float right = Math.Max(_startPoint.X, _endPoint.X);
                        float bottom = Math.Max(_startPoint.Y, _endPoint.Y);
                        g.DrawRectangle(pen, left, top, right - left, bottom - top);
                    }
                }
            }
        }

        /// <summary>
        /// Aplica el algoritmo de Liang-Barsky a todas las líneas definidas contra el rectángulo de recorte.
        /// Valida entradas y muestra mensajes de error en caso de valores inválidos.
        /// </summary>
        private void btnClip_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_clipRectDefined)
                {
                    MessageBox.Show(this, "Defina primero el rectángulo de recorte (modo 'Dibujar rect.').", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_lines.Count == 0)
                {
                    MessageBox.Show(this, "No hay líneas para recortar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_clipRect.Width <= 0 || _clipRect.Height <= 0)
                {
                    MessageBox.Show(this, "Rectángulo de recorte inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamada modular al algoritmo implementado en CLiangBarsky
                var clipped = CLiangBarsky.ClipSegments(_clipRect, _lines.Select(l => (l.Item1, l.Item2)));
                _clippedLines.Clear();
                foreach (var s in clipped)
                {
                    _clippedLines.Add((s.Item1, s.Item2));
                }

                lblStatus.Text = $"Segmentos recortados: {_clippedLines.Count}";
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(this, "Entrada inválida: " + aex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error al recortar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pnlCanvas.Invalidate();
        }

        /// <summary>
        /// Borra todas las entidades (líneas y rectángulo) y resultados.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            _lines.Clear();
            _clippedLines.Clear();
            _clipRectDefined = false;
            lblStatus.Text = "Listo";
            pnlCanvas.Invalidate();
        }

        /// <summary>
        /// Deshace la última línea añadida por el usuario.
        /// </summary>
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_lines.Count > 0)
            {
                _lines.RemoveAt(_lines.Count - 1);
                lblStatus.Text = $"Líneas: {_lines.Count}";
                _clippedLines.Clear();
                pnlCanvas.Invalidate();
            }
            else
            {
                MessageBox.Show(this, "No hay líneas para deshacer.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbDrawLine_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDrawLine.Checked)
            {
                _currentMode = DrawMode.DrawLine;
                lblStatus.Text = "Modo: Dibujar línea";
            }
        }

        private void rbDrawRect_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDrawRect.Checked)
            {
                _currentMode = DrawMode.DrawClipRect;
                lblStatus.Text = "Modo: Dibujar rectángulo (clip)";
            }
        }
    }
}
