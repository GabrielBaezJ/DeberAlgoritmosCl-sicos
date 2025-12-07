using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    /// <summary>
    /// Formulario para la demostración del algoritmo de Boundary Fill (Relleno de Fronteras)
    /// Permite al usuario dibujar formas cerradas y rellenarlas usando diferentes variantes del algoritmo.
    /// </summary>
    public partial class FrmBoundaryFill : Form
    {
        // Instancia del algoritmo
        private CBoundaryFill boundaryFillAlgo;

        // Canvas para dibujo
        private Bitmap canvas;
        private Stack<Bitmap> undoStack;

        // Colores
        private Color fillColor = Color.Red;
        private Color boundaryColor = Color.Black;

        // Herramientas
        private enum Tool { None, Circle, Rectangle, Line, BoundaryFill }
        private Tool currentTool = Tool.None;

        // Estado de dibujo
        private bool isDrawing = false;
        private Point startPt;
        private Point currentPt;

        public FrmBoundaryFill()
        {
            InitializeComponent();

            // Inicializar componentes
            boundaryFillAlgo = new CBoundaryFill();
            boundaryFillAlgo.CurrentVariant = CBoundaryFill.BoundaryFillVariant.FourConnected;

            undoStack = new Stack<Bitmap>();

            InitializeCanvas();

            // Configurar colores iniciales
            pnlFillColor.BackColor = fillColor;
            pnlBoundaryColor.BackColor = boundaryColor;

            // Mensaje inicial
            UpdateInstruction("Selecciona una herramienta para comenzar");
        }

        /// <summary>
        /// Inicializa o reinicia el canvas con fondo blanco
        /// </summary>
        private void InitializeCanvas()
        {
            canvas = new Bitmap(pnlCanvas.Width, pnlCanvas.Height);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }
            pnlCanvas.Invalidate();
        }

        /// <summary>
        /// Guarda el estado actual del canvas en el historial de deshacer
        /// </summary>
        private void SaveCanvasState()
        {
            try
            {
                if (canvas != null)
                {
                    // Limitar el historial a 10 estados para ahorrar memoria
                    if (undoStack.Count >= 10)
                    {
                        var items = undoStack.ToList();
                        undoStack.Clear();
                        for (int i = 0; i < 9; i++)
                        {
                            undoStack.Push(items[i]);
                        }
                    }

                    undoStack.Push(new Bitmap(canvas));
                    UpdateStatus($"Estado guardado. Historial: {undoStack.Count} estados");
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error al guardar estado: {ex.Message}");
            }
        }

        /// <summary>
        /// Establece la herramienta actual y actualiza la interfaz
        /// </summary>
        /// <param name="tool">La herramienta a establecer</param>
        private void SetTool(Tool tool)
        {
            currentTool = tool;
            isDrawing = false;

            // Cambiar cursor según la herramienta
            Cursor = (tool == Tool.None) ? Cursors.Default : Cursors.Cross;

            // Actualizar instrucciones
            switch (tool)
            {
                case Tool.Circle:
                    UpdateInstruction("Herramienta Círculo: arrastra para dibujar un círculo con línea negra.");
                    break;
                case Tool.Rectangle:
                    UpdateInstruction("Herramienta Rectángulo: arrastra para dibujar un rectángulo con línea negra.");
                    break;
                case Tool.Line:
                    UpdateInstruction("Herramienta Línea: arrastra para dibujar una línea con línea negra.");
                    break;
                case Tool.BoundaryFill:
                    UpdateInstruction("Herramienta Relleno de Fronteras: haz clic dentro de una forma cerrada para rellenar.");
                    break;
                default:
                    UpdateInstruction("Selecciona una herramienta para comenzar");
                    break;
            }
        }

        /// <summary>
        /// Actualiza el texto de instrucciones en la interfaz
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        private void UpdateInstruction(string message)
        {
            lblInstruction.Text = message ?? "Listo";
        }

        /// <summary>
        /// Actualiza el texto de estado en la barra de estado
        /// </summary>
        /// <param name="message">Mensaje de estado a mostrar</param>
        private void UpdateStatus(string message)
        {
            lblStatus.Text = message ?? "OK";
        }

        /// <summary>
        /// Valida que un punto esté dentro del canvas
        /// </summary>
        /// <param name="pt">Punto a validar</param>
        /// <returns>Punto ajustado dentro de los límites del canvas</returns>
        private Point ValidationPoint(Point pt)
        {
            return new Point(
                Math.Max(0, Math.Min(pt.X, pnlCanvas.Width - 1)),
                Math.Max(0, Math.Min(pt.Y, pnlCanvas.Height - 1))
            );
        }

        /// <summary>
        /// Obtiene un rectángulo normalizado entre dos puntos
        /// </summary>
        private Rectangle GetNormalizedRect(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p1.X - p2.X);
            int h = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, w, h);
        }

        // ==================== CONTROLES DE HERRAMIENTAS ====================

        private void btnDrawCircle_Click(object sender, EventArgs e)
        {
            SetTool(Tool.Circle);
        }

        private void btnDrawRectangle_Click(object sender, EventArgs e)
        {
            SetTool(Tool.Rectangle);
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            SetTool(Tool.Line);
        }

        private void btnBoundaryFill_Click(object sender, EventArgs e)
        {
            SetTool(Tool.BoundaryFill);
        }

        // ==================== CONTROLES DE COLOR ====================

        private void btnSelectFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                fillColor = colorDialog.Color;
                pnlFillColor.BackColor = fillColor;
                UpdateStatus($"Color de relleno cambiado a {fillColor.Name}");
            }
        }

        private void btnSelectBoundaryColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                boundaryColor = colorDialog.Color;
                pnlBoundaryColor.BackColor = boundaryColor;
                UpdateStatus($"Color de frontera cambiado a {boundaryColor.Name}");
            }
        }

        // ==================== CONTROLES DE VARIANTE ====================

        private void cmbVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cmbVariant.SelectedIndex)
                {
                    case 0:
                        boundaryFillAlgo.CurrentVariant = CBoundaryFill.BoundaryFillVariant.FourConnected;
                        UpdateStatus("Variante: 4-Conectado (rápida, usa 4 vecinos)");
                        break;
                    case 1:
                        boundaryFillAlgo.CurrentVariant = CBoundaryFill.BoundaryFillVariant.EightConnected;
                        UpdateStatus("Variante: 8-Conectado (incluye diagonales)");
                        break;
                    case 2:
                        boundaryFillAlgo.CurrentVariant = CBoundaryFill.BoundaryFillVariant.OptimizedEightConnected;
                        UpdateStatus("Variante: Optimizado (mejor rendimiento para imágenes grandes)");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar variante: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ACCIONES ====================

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCanvasState();
                InitializeCanvas();
                SetTool(Tool.None);
                UpdateInstruction("Canvas limpiado");
                UpdateStatus("Canvas limpiado - Lista para un nuevo dibujo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar canvas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"Error: {ex.Message}");
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (undoStack.Count > 0)
                {
                    canvas = undoStack.Pop();
                    pnlCanvas.Invalidate();
                    UpdateInstruction("Última acción deshecha");
                    UpdateStatus($"Deshacer realizado. Historial restante: {undoStack.Count}");
                }
                else
                {
                    UpdateInstruction("No hay acciones que deshacer");
                    UpdateStatus("Historial vacío");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al deshacer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"Error: {ex.Message}");
            }
        }

        // ==================== EVENTOS DEL CANVAS ====================

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                startPt = ValidationPoint(e.Location);
                currentPt = startPt;

                switch (currentTool)
                {
                    case Tool.Circle:
                    case Tool.Rectangle:
                    case Tool.Line:
                        isDrawing = true;
                        pnlCanvas.Capture = true;
                        break;

                    case Tool.BoundaryFill:
                        // Ejecutar boundary fill inmediatamente
                        ExecuteBoundaryFill(startPt.X, startPt.Y);
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
                currentPt = ValidationPoint(e.Location);
                pnlCanvas.Invalidate();
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error en movimiento: {ex.Message}");
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

                if (canvas != null)
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.SmoothingMode = SmoothingMode.None;
                        using (var pen = new Pen(boundaryColor, 1))
                        {
                            switch (currentTool)
                            {
                                case Tool.Circle:
                                    {
                                        int dx = currentPt.X - startPt.X;
                                        int dy = currentPt.Y - startPt.Y;
                                        int r = (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                                        if (r > 0)
                                        {
                                            Rectangle rect = new Rectangle(startPt.X - r, startPt.Y - r, r * 2, r * 2);
                                            g.DrawEllipse(pen, rect);
                                            UpdateInstruction($"Círculo dibujado en ({startPt.X}, {startPt.Y}) con radio {r}");
                                        }
                                    }
                                    break;

                                case Tool.Rectangle:
                                    {
                                        Rectangle rect = GetNormalizedRect(startPt, currentPt);
                                        if (rect.Width > 0 && rect.Height > 0)
                                        {
                                            g.DrawRectangle(pen, rect);
                                            UpdateInstruction($"Rectángulo dibujado de {rect.Width}x{rect.Height} píxeles");
                                        }
                                    }
                                    break;

                                case Tool.Line:
                                    {
                                        g.DrawLine(pen, startPt, currentPt);
                                        int distance = (int)Math.Sqrt(Math.Pow(currentPt.X - startPt.X, 2) + Math.Pow(currentPt.Y - startPt.Y, 2));
                                        UpdateInstruction($"Línea dibujada de {distance} píxeles");
                                    }
                                    break;
                            }
                        }
                    }

                    pnlCanvas.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dibujar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"Error: {ex.Message}");
            }
        }

        private void pnlCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentTool == Tool.BoundaryFill && e.Button == MouseButtons.Left)
            {
                try
                {
                    Point pt = ValidationPoint(e.Location);
                    ExecuteBoundaryFill(pt.X, pt.Y);
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Ejecuta el algoritmo Boundary Fill en un punto específico
        /// </summary>
        /// <param name="x">Coordenada X</param>
        /// <param name="y">Coordenada Y</param>
        private void ExecuteBoundaryFill(int x, int y)
        {
            try
            {
                if (canvas == null)
                {
                    UpdateStatus("Error: Canvas no inicializado");
                    return;
                }

                SaveCanvasState();

                var startTime = System.Diagnostics.Stopwatch.StartNew();
                int pixelsFilled = boundaryFillAlgo.BoundaryFill(canvas, x, y, fillColor, boundaryColor);
                startTime.Stop();

                pnlCanvas.Invalidate();

                string variantName;
                switch (cmbVariant.SelectedIndex)
                {
                    case 0:
                        variantName = "4-Conectado";
                        break;
                    case 1:
                        variantName = "8-Conectado";
                        break;
                    case 2:
                        variantName = "Optimizado";
                        break;
                    default:
                        variantName = "Desconocido";
                        break;
                }

                UpdateInstruction($"Relleno completado en ({x}, {y}). Píxeles rellenados: {pixelsFilled}");
                UpdateStatus($"Variante: {variantName} | Píxeles: {pixelsFilled} | Tiempo: {startTime.ElapsedMilliseconds}ms");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                UpdateStatus($"Coordenada fuera de rango: {ex.Message}");
                MessageBox.Show($"El punto ({x}, {y}) está fuera del canvas.\nCanvastamaño: {canvas.Width}x{canvas.Height}", 
                    "Coordenada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error: {ex.Message}");
                MessageBox.Show($"Error durante el relleno:\n{ex.Message}\n\nTipo: {ex.GetType().Name}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (canvas != null)
                {
                    e.Graphics.DrawImageUnscaled(canvas, 0, 0);
                }

                // Preview mientras se dibuja
                if (isDrawing)
                {
                    using (var previewPen = new Pen(Color.Blue))
                    {
                        previewPen.DashStyle = DashStyle.Dash;

                        switch (currentTool)
                        {
                            case Tool.Circle:
                                {
                                    int dx = currentPt.X - startPt.X;
                                    int dy = currentPt.Y - startPt.Y;
                                    int r = (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                                    if (r > 0)
                                    {
                                        Rectangle rect = new Rectangle(startPt.X - r, startPt.Y - r, r * 2, r * 2);
                                        e.Graphics.DrawEllipse(previewPen, rect);
                                    }
                                }
                                break;

                            case Tool.Rectangle:
                                {
                                    var rect = GetNormalizedRect(startPt, currentPt);
                                    if (rect.Width > 0 && rect.Height > 0)
                                        e.Graphics.DrawRectangle(previewPen, rect);
                                }
                                break;

                            case Tool.Line:
                                e.Graphics.DrawLine(previewPen, startPt, currentPt);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Paint: {ex.Message}");
            }
        }

        // ==================== ATAJOS DE TECLADO ====================

        private void FrmBoundaryFill_KeyDown(object sender, KeyEventArgs e)
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
