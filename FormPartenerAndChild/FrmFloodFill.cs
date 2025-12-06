using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    public partial class FrmFloodFill : Form
    {
        private CFloodFillAlgorithm algo;
        private Bitmap canvas;
        private Color fillColor = Color.Red;

        private enum Tool { None, Circle, Rectangle, FloodFill }
        private Tool currentTool = Tool.None;

        private bool isDrawing = false;
        private Point startPt;
        private Point currentPt;

        public FrmFloodFill()
        {
            InitializeComponent();
            algo = new CFloodFillAlgorithm();
            InitializeCanvas();
            pnlSelectedColor.BackColor = fillColor;
            lblInstruction.Text = "Select a tool to begin";
        }

        private void InitializeCanvas()
        {
            canvas = new Bitmap(600, 400);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }
            pnlCanvas?.Invalidate();
        }

        private void SetTool(Tool t)
        {
            currentTool = t;
            isDrawing = false;
            Cursor = (t == Tool.None) ? Cursors.Default : Cursors.Cross;

            switch (t)
            {
                case Tool.Circle:
                    lblInstruction.Text = "Circle tool: drag to draw a circle (left button).";
                    break;
                case Tool.Rectangle:
                    lblInstruction.Text = "Rectangle tool: drag to draw a rectangle (left button).";
                    break;
                case Tool.FloodFill:
                    lblInstruction.Text = "Flood Fill tool: click on canvas to fill.";
                    break;
                default:
                    lblInstruction.Text = "Select a tool.";
                    break;
            }
        }

        private void btnDrawCircle_Click(object sender, EventArgs e)
        {
            SetTool(Tool.Circle);
        }

        private void btnDrawRectangle_Click(object sender, EventArgs e)
        {
            SetTool(Tool.Rectangle);
        }

        private void btnFloodFill_Click(object sender, EventArgs e)
        {
            SetTool(Tool.FloodFill);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InitializeCanvas();
            SetTool(Tool.None);
            lblInstruction.Text = "Canvas cleared";
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                fillColor = colorDialog.Color;
                pnlSelectedColor.BackColor = fillColor;
            }
        }

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (currentTool == Tool.Circle || currentTool == Tool.Rectangle)
            {
                isDrawing = true;
                startPt = e.Location;
                currentPt = e.Location;
                pnlCanvas.Capture = true;
            }
            else if (currentTool == Tool.FloodFill)
            {
                if (canvas != null)
                {
                    int x = Math.Max(0, Math.Min(e.X, canvas.Width - 1));
                    int y = Math.Max(0, Math.Min(e.Y, canvas.Height - 1));
                    algo.FloodFill(canvas, x, y, fillColor);
                    pnlCanvas.Invalidate();
                    lblInstruction.Text = $"Flood fill applied at ({x}, {y})";
                }
            }
        }

        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            currentPt = new Point(Math.Max(0, Math.Min(e.X, canvas.Width - 1)), Math.Max(0, Math.Min(e.Y, canvas.Height - 1)));
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing || e.Button != MouseButtons.Left) return;

            isDrawing = false;
            pnlCanvas.Capture = false;

            if (canvas != null)
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.SmoothingMode = SmoothingMode.None; // ensure crisp 1px boundary
                    using (var pen = new Pen(Color.Black, 1))
                    {
                        if (currentTool == Tool.Circle)
                        {
                            int dx = currentPt.X - startPt.X;
                            int dy = currentPt.Y - startPt.Y;
                            int r = (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                            Rectangle rect = new Rectangle(startPt.X - r, startPt.Y - r, r * 2, r * 2);
                            g.DrawEllipse(pen, rect);
                        }
                        else if (currentTool == Tool.Rectangle)
                        {
                            Rectangle rect = GetNormalizedRect(startPt, currentPt);
                            g.DrawRectangle(pen, rect);
                        }
                    }
                }

                pnlCanvas.Invalidate();
                lblInstruction.Text = "Shape drawn.";
            }
        }

        private Rectangle GetNormalizedRect(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p1.X - p2.X);
            int h = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, w, h);
        }

        private void pnlCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            // Backwards-compatible flood fill click
            if (currentTool == Tool.FloodFill && canvas != null)
            {
                int x = Math.Max(0, Math.Min(e.X, canvas.Width - 1));
                int y = Math.Max(0, Math.Min(e.Y, canvas.Height - 1));
                algo.FloodFill(canvas, x, y, fillColor);
                pnlCanvas.Invalidate();
                lblInstruction.Text = $"Flood fill applied at ({x}, {y})";
            }
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (canvas != null)
            {
                e.Graphics.DrawImageUnscaled(canvas, 0, 0);
            }

            // Preview while drawing
            if (isDrawing && (currentTool == Tool.Circle || currentTool == Tool.Rectangle))
            {
                using (var previewPen = new Pen(Color.Blue))
                {
                    previewPen.DashStyle = DashStyle.Dash;

                    if (currentTool == Tool.Circle)
                    {
                        int dx = currentPt.X - startPt.X;
                        int dy = currentPt.Y - startPt.Y;
                        int r = (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                        Rectangle rect = new Rectangle(startPt.X - r, startPt.Y - r, r * 2, r * 2);
                        e.Graphics.DrawEllipse(previewPen, rect);
                    }
                    else
                    {
                        var rect = GetNormalizedRect(startPt, currentPt);
                        e.Graphics.DrawRectangle(previewPen, rect);
                    }
                }
            }
        }
    }
}
