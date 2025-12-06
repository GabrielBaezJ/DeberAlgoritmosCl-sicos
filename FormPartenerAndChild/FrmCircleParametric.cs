using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    public partial class FrmCircleParametric : Form
    {
        private CParametricCircleAlgorithm algo;
        private List<Point> points = new List<Point>();
        private float zoom = 1.0f;
        private int lastCenterX = 0;
        private int lastCenterY = 0;

        public FrmCircleParametric()
        {
            InitializeComponent();
            algo = new CParametricCircleAlgorithm();

            txtRadius.Text = "50";
            txtCenterX.Text = "0";
            txtCenterY.Text = "0";
            txtAngle.Text = "360";
            trkZoom.Value = 100;
            zoom = trkZoom.Value / 100f;
            lblZoom.Text = $"Zoom: {trkZoom.Value}%";
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRadius.Text, out int r) || r <= 0)
            {
                MessageBox.Show("Ingrese un radio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCenterX.Text, out int xc) || !int.TryParse(txtCenterY.Text, out int yc))
            {
                MessageBox.Show("Ingrese coordenadas de centro válidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtAngle.Text, out double angle))
            {
                MessageBox.Show("Ingrese un ángulo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardar centro usado (coordenadas en sistema centrado en el panel)
            lastCenterX = xc;
            lastCenterY = yc;

            // Generar puntos centrados en (xc,yc)
            points = algo.GenerateCirclePoints(xc, yc, r, 0, angle).Distinct().ToList();
            pnlCanvas.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            points.Clear();
            pnlCanvas.Invalidate();
        }

        private void trkZoom_Scroll(object sender, EventArgs e)
        {
            zoom = trkZoom.Value / 100f;
            lblZoom.Text = $"Zoom: {trkZoom.Value}%";
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            var state = e.Graphics.Save();

            int centerX = pnlCanvas.Width / 2;
            int centerY = pnlCanvas.Height / 2;

            // Centrar el sistema de coordenadas en el panel
            e.Graphics.TranslateTransform(centerX, centerY);
            e.Graphics.ScaleTransform(zoom, zoom);

            int grid = 10;
            using (var pen = new Pen(Color.LightGray, 1f / Math.Max(zoom, 0.0001f)))
            {
                int w = (int)Math.Ceiling(pnlCanvas.Width / (2 * Math.Max(zoom, 0.0001f)));
                int h = (int)Math.Ceiling(pnlCanvas.Height / (2 * Math.Max(zoom, 0.0001f)));

                for (int x = -w; x <= w; x += grid)
                    e.Graphics.DrawLine(pen, x, -h, x, h);
                for (int y = -h; y <= h; y += grid)
                    e.Graphics.DrawLine(pen, -w, y, w, y);
            }

            using (var axisPen = new Pen(Color.Red, 1f / Math.Max(zoom, 0.0001f)))
            {
                e.Graphics.DrawLine(axisPen, 0, -pnlCanvas.Height, 0, pnlCanvas.Height);
                e.Graphics.DrawLine(axisPen, -pnlCanvas.Width, 0, pnlCanvas.Width, 0);
            }

            // Marcar el centro del círculo si existe
            using (var centerPen = new Pen(Color.Blue, 2f / Math.Max(zoom, 0.0001f)))
            {
                // dibujar pequeña cruz en (lastCenterX, lastCenterY)
                e.Graphics.DrawLine(centerPen, lastCenterX - 5, lastCenterY, lastCenterX + 5, lastCenterY);
                e.Graphics.DrawLine(centerPen, lastCenterX, lastCenterY - 5, lastCenterX, lastCenterY + 5);
            }

            if (points != null && points.Count > 1)
            {
                // Ordenar por ángulo relativo al centro usado
                var ordered = points
                    .Distinct()
                    .OrderBy(p => Math.Atan2(p.Y - lastCenterY, p.X - lastCenterX))
                    .Select(p => new PointF(p.X, p.Y))
                    .ToArray();

                if (ordered.Length > 1)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var penCircle = new Pen(Color.Black, 1f / Math.Max(zoom, 0.0001f)))
                    {
                        e.Graphics.DrawPolygon(penCircle, ordered);
                    }
                }
            }

            e.Graphics.Restore(state);
        }
    }
}
