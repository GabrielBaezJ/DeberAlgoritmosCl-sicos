using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    public partial class FrmScanlineFill : Form
    {
        private CScanlineFill scanline;
        private Bitmap canvas;
        private float zoom = 1.0f;
        private List<Point> vertices = new List<Point>();
        private Color fillColor = Color.Red;

        public FrmScanlineFill()
        {
            InitializeComponent();
            scanline = new CScanlineFill();
            InitializeCanvas();

            cmbRule.SelectedIndex = 0;
            pnlSelectedColor.BackColor = fillColor;
            trkZoom.Value = 100;
            lblZoom.Text = $"Zoom: {trkZoom.Value}%";
        }

        private void InitializeCanvas()
        {
            canvas = new Bitmap(600, 400);
            using (var g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }
            pnlCanvas.Invalidate();
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                fillColor = colorDialog.Color;
                pnlSelectedColor.BackColor = fillColor;
            }
        }

        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtVertexX.Text, out int x) || !int.TryParse(txtVertexY.Text, out int y))
            {
                MessageBox.Show("Ingrese coordenadas válidas (enteros).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate range
            if (x < 0 || x >= canvas.Width || y < 0 || y >= canvas.Height)
            {
                MessageBox.Show($"Las coordenadas deben estar dentro del lienzo [0..{canvas.Width-1}, 0..{canvas.Height-1}].", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            vertices.Add(new Point(x, y));
            lstVertices.Items.Add($"{x}, {y}");

            // Draw vertex marker on canvas
            using (var g = Graphics.FromImage(canvas))
            {
                g.FillRectangle(Brushes.Black, x - 1, y - 1, 3, 3);
            }
            pnlCanvas.Invalidate();
        }

        private void btnClearVertices_Click(object sender, EventArgs e)
        {
            vertices.Clear();
            lstVertices.Items.Clear();
            InitializeCanvas();
        }

        private void btnClearCanvas_Click(object sender, EventArgs e)
        {
            InitializeCanvas();
            vertices.Clear();
            lstVertices.Items.Clear();
            lstSegments.Items.Clear();
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            if (vertices.Count < 3)
            {
                MessageBox.Show("El polígono debe contener al menos 3 vértices.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rule = (cmbRule.SelectedIndex == 0) ? CScanlineFill.FillRule.EvenOdd : CScanlineFill.FillRule.NonZero;

            try
            {
                lstSegments.Items.Clear();
                var segments = scanline.ScanlineFill(canvas, vertices, fillColor, rule);

                // populate listbox with segments for user feedback
                foreach (var seg in segments)
                {
                    lstSegments.Items.Add($"y={seg.y}: {seg.xStart}..{seg.xEnd}");
                }

                pnlCanvas.Invalidate();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error durante el llenado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trkZoom_Scroll(object sender, EventArgs e)
        {
            zoom = trkZoom.Value / 100f;
            lblZoom.Text = $"Zoom: {trkZoom.Value}%";
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            // Allow quick vertex add by clicking if within canvas
            int x = (int)(e.X / zoom);
            int y = (int)(e.Y / zoom);
            if (x < 0 || x >= canvas.Width || y < 0 || y >= canvas.Height) return;

            vertices.Add(new Point(x, y));
            lstVertices.Items.Add($"{x}, {y}");
            using (var g = Graphics.FromImage(canvas))
            {
                g.FillRectangle(Brushes.Black, x - 1, y - 1, 3, 3);
            }
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            var state = e.Graphics.Save();
            e.Graphics.ScaleTransform(zoom, zoom);
            if (canvas != null)
            {
                e.Graphics.DrawImageUnscaled(canvas, 0, 0);
            }

            // Draw polygon edges if vertices exist
            if (vertices.Count > 1)
            {
                using (var pen = new Pen(Color.Blue, 1f / Math.Max(zoom, 0.0001f)))
                {
                    for (int i = 0; i < vertices.Count - 1; i++)
                    {
                        var p1 = vertices[i];
                        var p2 = vertices[i + 1];
                        e.Graphics.DrawLine(pen, p1, p2);
                    }
                    // close polygon preview
                    if (vertices.Count > 2)
                    {
                        e.Graphics.DrawLine(pen, vertices[vertices.Count - 1], vertices[0]);
                    }
                }
            }

            e.Graphics.Restore(state);
        }
    }
}
