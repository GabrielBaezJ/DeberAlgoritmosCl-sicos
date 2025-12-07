using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Formulario para recorte de líneas usando el algoritmo de Cohen-Sutherland.
    /// UI diseñada para ser clara e intuitiva; código modular y con validaciones.
    /// </summary>
    public partial class FrmCohenSutherland : Form
    {
        private CCohenSutherland clipAlgo;

        // Canvas y estado
        private Bitmap canvas;
        private Stack<CanvasSnapshot> undoStack;

        // Colores
        private Color lineColor = Color.Black;
        private Color clipRectColor = Color.Blue;
        private Color clippedSegmentColor = Color.Red;

        // Herramientas
        private enum Tool { None, Line, ClipRect }
        private Tool currentTool = Tool.None;

        // Estado de dibujo
        private bool isDrawing = false;
        private Point startPt;
        private Point currentPt;

        // Líneas y rectángulo de recorte
        private List<Tuple<Point, Point>> originalLines; // líneas dibujadas por el usuario
        private List<Tuple<Point, Point>> visibleLines; // líneas visibles (pueden ser recortadas)
        private Rectangle clipRect;
        private bool clipRectSet = false;
        private bool isClippedApplied = false;

        // Clase para almacenar snapshots (undo)
        private class CanvasSnapshot
        {
            public Bitmap Image { get; set; }
            public List<Tuple<Point, Point>> OriginalLines { get; set; }
            public List<Tuple<Point, Point>> VisibleLines { get; set; }
            public Rectangle ClipRect { get; set; }
            public bool ClipRectSet { get; set; }
            public bool IsClippedApplied { get; set; }
            public Color LineColor { get; set; }
            public Color ClipRectColor { get; set; }
        }

        public FrmCohenSutherland()
        {
            InitializeComponent();

            clipAlgo = new CCohenSutherland();

            originalLines = new List<Tuple<Point, Point>>();
            visibleLines = new List<Tuple<Point, Point>>();
            undoStack = new Stack<CanvasSnapshot>();

            InitializeCanvas();

            pnlLineColor.BackColor = lineColor;
            pnlClipColor.BackColor = clipRectColor;

            UpdateInstruction("Selecciona una herramienta para comenzar");
            UpdateStatus("Listo");
        }

        private void InitializeCanvas()
        {
            canvas = new Bitmap(Math.Max(1, pnlCanvas.Width), Math.Max(1, pnlCanvas.Height));
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }

            originalLines.Clear();
            visibleLines.Clear();
            clipRectSet = false;
            isClippedApplied = false;

            pnlCanvas.Invalidate();
        }

        private void SaveCanvasState()
        {
            try
            {
                if (canvas == null) return;

                // Limitar historial a 10
                if (undoStack.Count >= 10)
                {
                    var items = undoStack.Reverse().Skip(1).Reverse().ToList();
                    undoStack.Clear();
                    foreach (var it in items)
                        undoStack.Push(it);
                }

                var snapshot = new CanvasSnapshot();
                snapshot.Image = new Bitmap(canvas);
                snapshot.OriginalLines = originalLines.Select(l => new Tuple<Point, Point>(l.Item1, l.Item2)).ToList();
                snapshot.VisibleLines = visibleLines.Select(l => new Tuple<Point, Point>(l.Item1, l.Item2)).ToList();
                snapshot.ClipRect = clipRect;
                snapshot.ClipRectSet = clipRectSet;
                snapshot.IsClippedApplied = isClippedApplied;
                snapshot.LineColor = lineColor;
                snapshot.ClipRectColor = clipRectColor;

                undoStack.Push(snapshot);
                UpdateStatus($"Estado guardado. Historial: {undoStack.Count}");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error al guardar estado: {ex.Message}");
            }
        }

        private void RestoreCanvasState(CanvasSnapshot snapshot)
        {
            try
            {
                if (snapshot == null) return;

                canvas = new Bitmap(snapshot.Image);
                originalLines = snapshot.OriginalLines.Select(l => new Tuple<Point, Point>(l.Item1, l.Item2)).ToList();
                visibleLines = snapshot.VisibleLines.Select(l => new Tuple<Point, Point>(l.Item1, l.Item2)).ToList();
                clipRect = snapshot.ClipRect;
                clipRectSet = snapshot.ClipRectSet;
                isClippedApplied = snapshot.IsClippedApplied;
                lineColor = snapshot.LineColor;
                clipRectColor = snapshot.ClipRectColor;

                pnlLineColor.BackColor = lineColor;
                pnlClipColor.BackColor = clipRectColor;

                pnlCanvas.Invalidate();
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error al restaurar estado: {ex.Message}");
            }
        }

        private void SetTool(Tool tool)
        {
            currentTool = tool;
            isDrawing = false;
            Cursor = (tool == Tool.None) ? Cursors.Default : Cursors.Cross;

            switch (tool)
            {
                case Tool.Line:
                    UpdateInstruction("Herramienta Línea: arrastra para dibujar una línea (botón izquierdo).");
                    break;
                case Tool.ClipRect:
                    UpdateInstruction("Herramienta Rectángulo de Recorte: arrastra para definir el área de recorte.");
                    break;
                default:
                    UpdateInstruction("Selecciona una herramienta para comenzar");
                    break;
            }
        }

        private void UpdateInstruction(string message)
        {
            lblInstruction.Text = message ?? "";
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message ?? "";
        }

        private Point ValidatePoint(Point pt)
        {
            return new Point(Math.Max(0, Math.Min(pt.X, pnlCanvas.Width - 1)), Math.Max(0, Math.Min(pt.Y, pnlCanvas.Height - 1)));
        }

        private Rectangle GetNormalizedRect(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p1.X - p2.X);
            int h = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, w, h);
        }

        // ==================== CONTROLES ====================

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            SetTool(Tool.Line);
        }

        private void btnSetClipRect_Click(object sender, EventArgs e)
        {
            SetTool(Tool.ClipRect);
        }

        private void btnClipLines_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clipRectSet)
                {
                    MessageBox.Show("Defina primero un rectángulo de recorte.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (originalLines.Count == 0)
                {
                    MessageBox.Show("No hay líneas para recortar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveCanvasState();

                var clipped = new List<Tuple<Point, Point>>();
                int accepted = 0;
                foreach (var ln in originalLines)
                {
                    Point a = ln.Item1;
                    Point b = ln.Item2;
                    try
                    {
                        Point pa = new Point(a.X, a.Y);
                        Point pb = new Point(b.X, b.Y);
                        bool ok = clipAlgo.ClipLine(clipRect, ref pa, ref pb);
                        if (ok)
                        {
                            clipped.Add(new Tuple<Point, Point>(pa, pb));
                            accepted++;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        UpdateStatus($"Error en recorte: {ex.Message}");
                    }
                }

                visibleLines = clipped;
                isClippedApplied = true;

                RenderToCanvas();

                UpdateInstruction($"Recorte completado. Líneas visibles: {visibleLines.Count}");
                UpdateStatus($"Recorte: {accepted} segmentos aceptados / {originalLines.Count} líneas procesadas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al recortar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"Error: {ex.Message}");
            }
        }

        private void btnSelectLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lineColor = colorDialog.Color;
                pnlLineColor.BackColor = lineColor;
                UpdateStatus($"Color de línea cambiado a {lineColor.Name}");
                RenderToCanvas();
            }
        }

        private void btnSelectClipColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                clipRectColor = colorDialog.Color;
                pnlClipColor.BackColor = clipRectColor;
                UpdateStatus($"Color de recorte cambiado a {clipRectColor.Name}");
                RenderToCanvas();
            }
        }

        private void cmbVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Interfaz preparada para cambiar de algoritmo. Actualmente solo Cohen-Sutherland está implementado.
            if (cmbVariant.SelectedIndex == 0)
            {
                UpdateStatus("Algoritmo: Cohen-Sutherland");
            }
            else
            {
                UpdateStatus("Variante seleccionada no implementada. Se usará Cohen-Sutherland.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCanvasState();
                InitializeCanvas();
                UpdateInstruction("Canvas limpiado");
                UpdateStatus("Canvas limpiado - listo para dibujar");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (undoStack.Count > 0)
                {
                    var snap = undoStack.Pop();
                    RestoreCanvasState(snap);
                    UpdateInstruction("Deshacer realizado");
                    UpdateStatus($"Deshacer realizado. Historial: {undoStack.Count}");
                }
                else
                {
                    UpdateStatus("No hay acciones para deshacer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al deshacer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== EVENTOS DEL CANVAS ====================

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                startPt = ValidatePoint(e.Location);
                currentPt = startPt;

                switch (currentTool)
                {
                    case Tool.Line:
                    case Tool.ClipRect:
                        isDrawing = true;
                        pnlCanvas.Capture = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error: {ex.Message}");
            }
        }

        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            try
            {
                currentPt = ValidatePoint(e.Location);
                pnlCanvas.Invalidate();
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error al mover: {ex.Message}");
            }
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing || e.Button != MouseButtons.Left) return;

            try
            {
                isDrawing = false;
                pnlCanvas.Capture = false;

                SaveCanvasState();

                if (currentTool == Tool.Line)
                {
                    // Añadir línea
                    var ln = new Tuple<Point, Point>(startPt, currentPt);
                    originalLines.Add(ln);
                    visibleLines.Add(new Tuple<Point, Point>(startPt, currentPt));
                    isClippedApplied = false;

                    RenderToCanvas();
                    UpdateInstruction($"Línea dibujada de ({startPt.X},{startPt.Y}) a ({currentPt.X},{currentPt.Y})");
                    UpdateStatus($"Líneas totales: {originalLines.Count}");
                }
                else if (currentTool == Tool.ClipRect)
                {
                    // Establecer rectángulo de recorte
                    Rectangle rect = GetNormalizedRect(startPt, currentPt);
                    if (rect.Width > 0 && rect.Height > 0)
                    {
                        clipRect = rect;
                        clipRectSet = true;
                        isClippedApplied = false;
                        RenderToCanvas();
                        UpdateInstruction($"Rectángulo de recorte definido: {rect.Width}x{rect.Height}");
                        UpdateStatus($"Rectángulo: ({rect.Left},{rect.Top}) - ({rect.Right},{rect.Bottom})");
                    }
                    else
                    {
                        UpdateStatus("Rectángulo inválido: ancho/alto debe ser > 0");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al finalizar dibujo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"Error: {ex.Message}");
            }
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                // Dibujar el bitmap base
                if (canvas != null)
                {
                    e.Graphics.DrawImageUnscaled(canvas, 0, 0);
                }

                // Preview mientras se dibuja
                if (isDrawing)
                {
                    using (var previewPen = new Pen(Color.Green))
                    {
                        previewPen.DashStyle = DashStyle.Dash;
                        previewPen.Width = 1;

                        if (currentTool == Tool.Line)
                        {
                            e.Graphics.DrawLine(previewPen, startPt, currentPt);
                        }
                        else if (currentTool == Tool.ClipRect)
                        {
                            var rect = GetNormalizedRect(startPt, currentPt);
                            e.Graphics.DrawRectangle(previewPen, rect);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Paint: {ex.Message}");
            }
        }

        /// <summary>
        /// Renderiza todas las entidades actuales en el bitmap del canvas.
        /// El diseño está pensado para ser claro: cuando se aplica el recorte se muestran
        /// las líneas originales en gris y los segmentos recortados en color destacado.
        /// </summary>
        private void RenderToCanvas()
        {
            if (canvas == null) InitializeCanvas();

            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                // Dibujar líneas originales en gris si se aplicó recorte
                if (isClippedApplied && originalLines.Count > 0)
                {
                    using (var pen = new Pen(Color.LightGray, 1))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        foreach (var ln in originalLines)
                        {
                            g.DrawLine(pen, ln.Item1, ln.Item2);
                        }
                    }
                }

                // Dibujar líneas visibles
                using (var pen = new Pen(isClippedApplied ? clippedSegmentColor : lineColor, 1))
                {
                    foreach (var ln in visibleLines)
                    {
                        g.DrawLine(pen, ln.Item1, ln.Item2);
                    }
                }

                // Dibujar rectángulo de recorte si está definido
                if (clipRectSet)
                {
                    using (var pen = new Pen(clipRectColor, 1))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        g.DrawRectangle(pen, clipRect);
                    }
                }
            }

            pnlCanvas.Invalidate();
        }

        // ==================== ATAJOS ====================

        private void FrmCohenSutherland_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.Z)
                {
                    btnUndo_Click(null, null);
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.N)
                {
                    btnClear_Click(null, null);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    SetTool(Tool.None);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error en atajo: {ex.Message}");
            }
        }
    }
}
