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
    /// Formulario para interactuar con el algoritmo Cyrus-Beck.
    /// Permite dibujar un polígono convexo (ventana de recorte) y líneas a recortar.
    /// La UI valida entradas y evita cerrar la aplicación por entradas inválidas.
    /// </summary>
    public partial class FrmCyrusBeck : Form
    {
        private enum DrawMode { None, DrawLine, DrawPolygon }

        private DrawMode _currentMode = DrawMode.DrawLine;

        // Líneas originales definidas por el usuario
        private readonly List<(PointF, PointF)> _lines = new List<(PointF, PointF)>();

        // Resultado del recorte
        private readonly List<(PointF, PointF)> _clippedLines = new List<(PointF, PointF)>();

        // Polígono ventana de recorte
        private readonly List<PointF> _polygon = new List<PointF>();
        private bool _polygonClosed = false;

        // Interacción de dibujo
        private bool _isDrawing = false; // usado para dibujar líneas
        private PointF _startPoint;
        private PointF _endPoint;

        private const float MIN_SEGMENT = 1e-3f;
        private const float EPSILON = 1e-6f;

        public FrmCyrusBeck()
        {
            InitializeComponent();
            InitializeCustom();
        }

        private void InitializeCustom()
        {
            try
            {
                rbDrawLine.Checked = true;
                lblStatus.Text = "Modo: Dibujar línea";

                // Activar double buffering en el panel para mejorar el dibujo
                try
                {
                    var prop = typeof(Panel).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (prop != null)
                        prop.SetValue(pnlCanvas, true, null);
                }
                catch
                {
                    // No crítico
                }
            }
            catch
            {
                // No dejar que excepciones en la inicialización detengan la app
            }
        }

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_currentMode == DrawMode.DrawLine)
            {
                _isDrawing = true;
                _startPoint = e.Location;
                _endPoint = e.Location;
                pnlCanvas.Capture = true;
            }
            else if (_currentMode == DrawMode.DrawPolygon)
            {
                if (_polygonClosed)
                {
                    MessageBox.Show(this, "El polígono ya está cerrado. Pulse 'Deshacer' para reabrir.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Añadir vértice
                _polygon.Add(e.Location);
                lblStatus.Text = $"Vértices: {_polygon.Count}";
                _clippedLines.Clear();
                pnlCanvas.Invalidate();
            }
        }

        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_currentMode == DrawMode.DrawLine)
            {
                if (!_isDrawing) return;
                _endPoint = e.Location;
                lblStatus.Text = $"Dibujando línea: {(int)_startPoint.X},{(int)_startPoint.Y} -> {(int)_endPoint.X},{(int)_endPoint.Y}";
                pnlCanvas.Invalidate();
            }
            else if (_currentMode == DrawMode.DrawPolygon)
            {
                if (_polygon.Count > 0 && !_polygonClosed)
                {
                    _endPoint = e.Location; // rubber-band preview from last vertex
                    pnlCanvas.Invalidate();
                }
            }
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_currentMode == DrawMode.DrawLine && _isDrawing)
            {
                _isDrawing = false;
                pnlCanvas.Capture = false;
                _endPoint = e.Location;

                try
                {
                    if (Math.Abs(_startPoint.X - _endPoint.X) < MIN_SEGMENT && Math.Abs(_startPoint.Y - _endPoint.Y) < MIN_SEGMENT)
                    {
                        lblStatus.Text = "Línea demasiado corta, ignorada.";
                    }
                    else
                    {
                        _lines.Add((_startPoint, _endPoint));
                        lblStatus.Text = $"Líneas: {_lines.Count}";
                        _clippedLines.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error al finalizar la línea: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                pnlCanvas.Invalidate();
            }
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Fondo
            g.Clear(Color.White);

            // Dibujar líneas originales en negro
            using (var pen = new Pen(Color.Black, 2f))
            {
                foreach (var seg in _lines)
                {
                    g.DrawLine(pen, seg.Item1, seg.Item2);
                }
            }

            // Dibujar líneas recortadas en rojo
            using (var penClip = new Pen(Color.Red, 2f))
            {
                foreach (var seg in _clippedLines)
                {
                    g.DrawLine(penClip, seg.Item1, seg.Item2);
                }
            }

            // Dibujar polígono (azul)
            if (_polygon.Count > 0)
            {
                using (var penPoly = new Pen(Color.Blue, 2f))
                {
                    // Dibujar aristas existentes
                    for (int i = 0; i < _polygon.Count - 1; i++)
                    {
                        g.DrawLine(penPoly, _polygon[i], _polygon[i + 1]);
                    }

                    // Si polígono cerrado, dibujar arista final
                    if (_polygonClosed && _polygon.Count > 1)
                    {
                        g.DrawLine(penPoly, _polygon[_polygon.Count - 1], _polygon[0]);
                    }
                    else if (!_polygonClosed && _polygon.Count > 0)
                    {
                        // rubber-band desde último vértice hasta cursor
                        var last = _polygon[_polygon.Count - 1];
                        g.DrawLine(penPoly, last, _endPoint);
                    }
                }

                // Dibujar vértices
                using (var brush = new SolidBrush(Color.Blue))
                {
                    foreach (var p in _polygon)
                    {
                        g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
                    }
                }
            }

            // Dibujar línea en curso (modo línea)
            if (_isDrawing && _currentMode == DrawMode.DrawLine)
            {
                using (var pen = new Pen(Color.DarkGreen, 1.5f))
                {
                    pen.DashStyle = DashStyle.Dash;
                    g.DrawLine(pen, _startPoint, _endPoint);
                }
            }
        }

        private void btnClosePolygon_Click(object sender, EventArgs e)
        {
            try
            {
                if (_polygon.Count < 3)
                {
                    MessageBox.Show(this, "El polígono debe tener al menos 3 vértices.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsPolygonConvex(_polygon))
                {
                    MessageBox.Show(this, "El polígono no es convexo. Ajuste los vértices para formar un polígono convexo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _polygonClosed = true;
                lblStatus.Text = $"Polígono cerrado (vértices: {_polygon.Count})";
                _clippedLines.Clear();
                pnlCanvas.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error al cerrar el polígono: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClip_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_polygonClosed)
                {
                    MessageBox.Show(this, "Defina y cierre primero el polígono convexo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_lines.Count == 0)
                {
                    MessageBox.Show(this, "No hay líneas para recortar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación final del polígono antes de recortar
                if (_polygon.Count < 3)
                {
                    MessageBox.Show(this, "Polígono inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsPolygonConvex(_polygon))
                {
                    MessageBox.Show(this, "El polígono no es convexo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamada modular al algoritmo Cyrus-Beck
                var clipped = CCyrusBeck.ClipSegments(_polygon, _lines.Select(l => (l.Item1, l.Item2)));
                _clippedLines.Clear();
                foreach (var s in clipped)
                    _clippedLines.Add((s.Item1, s.Item2));

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            _lines.Clear();
            _clippedLines.Clear();
            _polygon.Clear();
            _polygonClosed = false;
            lblStatus.Text = "Listo";
            pnlCanvas.Invalidate();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentMode == DrawMode.DrawLine)
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
                else if (_currentMode == DrawMode.DrawPolygon)
                {
                    if (_polygonClosed)
                    {
                        // Reabrir para editar
                        _polygonClosed = false;
                        lblStatus.Text = "Polígono reabierto para editar.";
                        pnlCanvas.Invalidate();
                        return;
                    }

                    if (_polygon.Count > 0)
                    {
                        _polygon.RemoveAt(_polygon.Count - 1);
                        lblStatus.Text = $"Vértices: {_polygon.Count}";
                        pnlCanvas.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show(this, "No hay vértices para deshacer.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error en Deshacer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void rbDrawPolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDrawPolygon.Checked)
            {
                _currentMode = DrawMode.DrawPolygon;
                lblStatus.Text = "Modo: Dibujar polígono (clic izquierdo para añadir vértices)";
            }
        }

        #region Utilidades para validación de polígono

        private static float SignedArea(IList<PointF> poly)
        {
            float area = 0f;
            for (int i = 0; i < poly.Count; i++)
            {
                var p0 = poly[i];
                var p1 = poly[(i + 1) % poly.Count];
                area += (p0.X * p1.Y - p1.X * p0.Y);
            }
            return area / 2f;
        }

        private static bool IsPolygonConvex(IList<PointF> poly)
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

        #endregion
    }
}
