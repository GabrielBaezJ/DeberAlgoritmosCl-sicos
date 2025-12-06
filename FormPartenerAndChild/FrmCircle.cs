using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    public partial class FrmCircle : Form
    {
        private CCircle circle;
        private int lastCenterX = -1;
        private int lastCenterY = -1;

        public FrmCircle()
        {
            InitializeComponent();
            circle = new CCircle();

            // Inicializar valores por defecto para centro en el centro del panel
            txtCenterX.Text = (pnlCanvas.Width / 2).ToString();
            txtCenterY.Text = (pnlCanvas.Height / 2).ToString();
            lblPixelCount.Text = "Pixels: 0";
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRadius.Text, out int radius) || radius <= 0)
            {
                MessageBox.Show("Ingrese un radio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCenterX.Text, out int centerX) || !int.TryParse(txtCenterY.Text, out int centerY))
            {
                MessageBox.Show("Ingrese coordenadas de centro válidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            circle.points.Clear();

            // Guardar último centro para dibujar ejes
            lastCenterX = centerX;
            lastCenterY = centerY;

            circle.CircleMidPoint(centerX, centerY, radius);

            // Calcular puntos únicos y mostrar en la lista
            var unique = circle.points.Distinct().OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            lstPixels.BeginUpdate();
            lstPixels.Items.Clear();
            foreach (var p in unique)
            {
                lstPixels.Items.Add($"{p.X}, {p.Y}");
            }
            lstPixels.EndUpdate();

            lblPixelCount.Text = $"Pixels: {unique.Count}";

            pnlCanvas.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            circle.points.Clear();
            lstPixels.Items.Clear();
            lblPixelCount.Text = "Pixels: 0";
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar fondo
            e.Graphics.Clear(Color.White);

            int grid = 10; // tamaño de la cuadricula en pixeles
            using (var pen = new Pen(Color.LightGray))
            {
                // Lineas verticales
                for (int x = 0; x <= pnlCanvas.Width; x += grid)
                {
                    e.Graphics.DrawLine(pen, x, 0, x, pnlCanvas.Height);
                }
                // Lineas horizontales
                for (int y = 0; y <= pnlCanvas.Height; y += grid)
                {
                    e.Graphics.DrawLine(pen, 0, y, pnlCanvas.Width, y);
                }
            }

            // Dibujar ejes del centro, si se proporcionó
            if (lastCenterX >= 0 && lastCenterY >= 0)
            {
                using (var axisPen = new Pen(Color.Red, 1))
                {
                    e.Graphics.DrawLine(axisPen, lastCenterX, 0, lastCenterX, pnlCanvas.Height);
                    e.Graphics.DrawLine(axisPen, 0, lastCenterY, pnlCanvas.Width, lastCenterY);
                }
            }

            // Dibujar pixeles usados (puntos únicos)
            foreach (var p in circle.points.Distinct())
            {
                // Asegurar que el punto está dentro del panel antes de dibujar
                if (p.X >= 0 && p.X < pnlCanvas.Width && p.Y >= 0 && p.Y < pnlCanvas.Height)
                {
                    e.Graphics.FillRectangle(Brushes.Black, p.X, p.Y, 1, 1);
                }
            }
        }
    }
}
