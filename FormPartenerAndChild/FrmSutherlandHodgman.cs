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
    /// Form para Sutherland-Hodgman: permite definir un polígono sujeto (subject) y un polígono de recorte (clip)
    /// y aplicar el algoritmo de recorte de polígonos. UI con validaciones y experiencia de usuario básica.
    /// </summary>
    public partial class FrmSutherlandHodgman : Form
    {
        private enum DrawMode { None, DrawSubject, DrawClip }

        private DrawMode _currentMode = DrawMode.DrawSubject;

        private readonly List<PointF> _subject = new List<PointF>();
        private readonly List<PointF> _clip = new List<PointF>();
        private bool _subjectClosed = false;
        private bool _clipClosed = false;

        private List<PointF> _clippedResult = new List<PointF>();

        // Interaction
        private bool _isDrawing = false;
        private PointF _startPoint;
        private PointF _endPoint;

        private const float EPSILON = 1e-6f;

        public FrmSutherlandHodgman()
        {
            InitializeComponent();
            InitializeCustom();
        }

        private void InitializeCustom()
        {
            try
            {
                rbDrawSubject.Checked = true;
                lblStatus.Text = "Modo: Dibujar subject";

                // Enable double buffering on panel
                try
                {
                    var prop = typeof(Panel).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (prop != null) prop.SetValue(pnlCanvas, true, null);
                }
                catch { }
            }
            catch { }
        }

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_currentMode == DrawMode.DrawSubject)
            {
                if (_subjectClosed)
                {
                    MessageBox.Show(this, "El subject ya está cerrado. Use Deshacer para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _subject.Add(e.Location);
                lblStatus.Text = $"Subject vértices: {_subject.Count}";
                _clippedResult.Clear();
                pnlCanvas.Invalidate();
            }
            else if (_currentMode == DrawMode.DrawClip)
            {
                if (_clipClosed)
                {
                    MessageBox.Show(this, "El clip ya está cerrado. Use Deshacer para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _clip.Add(e.Location);
                lblStatus.Text = $"Clip vértices: {_clip.Count}";
                _clippedResult.Clear();
                pnlCanvas.Invalidate();
            }
        }

        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_currentMode == DrawMode.DrawSubject)
            {
                if (_subject.Count > 0 && !_subjectClosed)
                {
                    _endPoint = e.Location;
                    pnlCanvas.Invalidate();
                }
            }
            else if (_currentMode == DrawMode.DrawClip)
            {
                if (_clip.Count > 0 && !_clipClosed)
                {
                    _endPoint = e.Location;
                    pnlCanvas.Invalidate();
                }
            }
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            // Nothing to do on mouse up for vertex-based drawing
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            // Draw subject polygon
            if (_subject.Count > 0)
            {
                using (var pen = new Pen(Color.Black, 2f))
                {
                    for (int i = 0; i < _subject.Count - 1; i++)
                        g.DrawLine(pen, _subject[i], _subject[i + 1]);

                    if (_subjectClosed && _subject.Count > 1)
                        g.DrawLine(pen, _subject[_subject.Count - 1], _subject[0]);
                    else if (!_subjectClosed && _subject.Count > 0)
                        g.DrawLine(pen, _subject[_subject.Count - 1], _endPoint);
                }

                using (var brush = new SolidBrush(Color.Black))
                {
                    foreach (var p in _subject)
                        g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
                }
            }

            // Draw clip polygon
            if (_clip.Count > 0)
            {
                using (var pen = new Pen(Color.Blue, 2f))
                {
                    for (int i = 0; i < _clip.Count - 1; i++)
                        g.DrawLine(pen, _clip[i], _clip[i + 1]);

                    if (_clipClosed && _clip.Count > 1)
                        g.DrawLine(pen, _clip[_clip.Count - 1], _clip[0]);
                    else if (!_clipClosed && _clip.Count > 0)
                        g.DrawLine(pen, _clip[_clip.Count - 1], _endPoint);
                }

                using (var brush = new SolidBrush(Color.Blue))
                {
                    foreach (var p in _clip)
                        g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
                }
            }

            // Draw clipped result in red
            if (_clippedResult != null && _clippedResult.Count > 0)
            {
                using (var pen = new Pen(Color.Red, 2f))
                {
                    for (int i = 0; i < _clippedResult.Count; i++)
                    {
                        var p0 = _clippedResult[i];
                        var p1 = _clippedResult[(i + 1) % _clippedResult.Count];
                        g.DrawLine(pen, p0, p1);
                    }
                }

                using (var brush = new SolidBrush(Color.Red))
                {
                    foreach (var p in _clippedResult)
                        g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
                }
            }
        }

        private void btnClosePolygon_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentMode == DrawMode.DrawSubject)
                {
                    if (_subject.Count < 3)
                    {
                        MessageBox.Show(this, "El subject debe tener al menos 3 vértices.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _subjectClosed = true;
                    lblStatus.Text = $"Subject cerrado (vértices: {_subject.Count})";
                    pnlCanvas.Invalidate();
                }
                else if (_currentMode == DrawMode.DrawClip)
                {
                    if (_clip.Count < 3)
                    {
                        MessageBox.Show(this, "El clip debe tener al menos 3 vértices.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!IsConvex(_clip))
                    {
                        MessageBox.Show(this, "El polígono de recorte debe ser convexo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _clipClosed = true;
                    lblStatus.Text = $"Clip cerrado (vértices: {_clip.Count})";
                    pnlCanvas.Invalidate();
                }
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
                if (!_subjectClosed || !_clipClosed)
                {
                    MessageBox.Show(this, "Cierre ambos polígonos antes de recortar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_subject.Count < 3 || _clip.Count < 3)
                {
                    MessageBox.Show(this, "Polígonos inválidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsConvex(_clip))
                {
                    MessageBox.Show(this, "El polígono de recorte debe ser convexo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _clippedResult = CSutherlandHodgman.ClipPolygon(_subject, _clip);
                lblStatus.Text = $"Vértices resultado: {_clippedResult.Count}";
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
            _subject.Clear();
            _clip.Clear();
            _subjectClosed = false;
            _clipClosed = false;
            _clippedResult.Clear();
            lblStatus.Text = "Listo";
            pnlCanvas.Invalidate();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentMode == DrawMode.DrawSubject)
                {
                    if (_subjectClosed)
                    {
                        _subjectClosed = false;
                        lblStatus.Text = "Subject reabierto para editar.";
                        pnlCanvas.Invalidate();
                        return;
                    }

                    if (_subject.Count > 0)
                    {
                        _subject.RemoveAt(_subject.Count - 1);
                        lblStatus.Text = $"Subject vértices: {_subject.Count}";
                        pnlCanvas.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show(this, "No hay vértices para deshacer.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (_currentMode == DrawMode.DrawClip)
                {
                    if (_clipClosed)
                    {
                        _clipClosed = false;
                        lblStatus.Text = "Clip reabierto para editar.";
                        pnlCanvas.Invalidate();
                        return;
                    }

                    if (_clip.Count > 0)
                    {
                        _clip.RemoveAt(_clip.Count - 1);
                        lblStatus.Text = $"Clip vértices: {_clip.Count}";
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

        private void rbDrawSubject_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDrawSubject.Checked)
            {
                _currentMode = DrawMode.DrawSubject;
                lblStatus.Text = "Modo: Dibujar subject";
            }
        }

        private void rbDrawClip_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDrawClip.Checked)
            {
                _currentMode = DrawMode.DrawClip;
                lblStatus.Text = "Modo: Dibujar clip";
            }
        }

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
    }
}
