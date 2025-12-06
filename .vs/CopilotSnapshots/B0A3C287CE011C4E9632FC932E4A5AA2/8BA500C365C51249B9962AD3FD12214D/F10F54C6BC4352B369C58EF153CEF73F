using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    public partial class FrmBresenham : Form
    {
        private CBresenhamAlgorithm algo;
        private List<Point> points = new List<Point>();
        private float zoom = 1.0f;

        public FrmBresenham()
        {
            InitializeComponent();
            algo = new CBresenhamAlgorithm();

            txtX0.Text = "-100";
            txtY0.Text = "0";
            txtX1.Text = "100";
            txtY1.Text = "0";
            trkZoom.Value = 100;
            zoom = trkZoom.Value / 100f;
            lblZoom.Text = $"Zoom: {trkZoom.Value}%";
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtX0.Text, out int x0) || !int.TryParse(txtY0.Text, out int y0) ||
                !int.TryParse(txtX1.Text, out int x1) || !int.TryParse(txtY1.Text, out int y1))
            {
                MessageBox.Show("Ingrese coordenadas válidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            points = algo.GenerateLinePoints(x0, y0, x1, y1).Distinct().ToList();

            lstPixels.BeginUpdate();
            lstPixels.Items.Clear();
            foreach (var p in points)
            {
                lstPixels.Items.Add($"{p.X}, {p.Y}");
            }
            lstPixels.EndUpdate();

            lblPixelCount.Text = $"Pixels: {points.Count}";

            pnlCanvas.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            points.Clear();
            lstPixels.Items.Clear();
            lblPixelCount.Text = "Pixels: 0";
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

            // Dibujar los pixeles usados como rectángulos 1x1 en coordenadas del mundo
            foreach (var p in points)
            {
                // Asegurar que el pixel está dentro del área visible
                if (p.X >= -pnlCanvas.Width && p.X <= pnlCanvas.Width && p.Y >= -pnlCanvas.Height && p.Y <= pnlCanvas.Height)
                {
                    e.Graphics.FillRectangle(Brushes.Black, p.X, p.Y, 1, 1);
                }
            }

            e.Graphics.Restore(state);
        }
    }
}
