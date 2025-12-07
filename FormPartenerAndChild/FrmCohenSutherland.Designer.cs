namespace FormPartenerAndChild
{
    partial class FrmCohenSutherland
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.groupBoxTools = new System.Windows.Forms.GroupBox();
            this.btnSetClipRect = new System.Windows.Forms.Button();
            this.btnDrawLine = new System.Windows.Forms.Button();
            this.groupBoxAlgorithm = new System.Windows.Forms.GroupBox();
            this.cmbVariant = new System.Windows.Forms.ComboBox();
            this.lblVariant = new System.Windows.Forms.Label();
            this.btnClipLines = new System.Windows.Forms.Button();
            this.groupBoxColors = new System.Windows.Forms.GroupBox();
            this.lblClipColor = new System.Windows.Forms.Label();
            this.pnlClipColor = new System.Windows.Forms.Panel();
            this.btnSelectClipColor = new System.Windows.Forms.Button();
            this.lblLineColor = new System.Windows.Forms.Label();
            this.pnlLineColor = new System.Windows.Forms.Panel();
            this.btnSelectLineColor = new System.Windows.Forms.Button();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pnlToolbar.SuspendLayout();
            this.groupBoxTools.SuspendLayout();
            this.groupBoxAlgorithm.SuspendLayout();
            this.groupBoxColors.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanvas.Location = new System.Drawing.Point(12, 12);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(750, 500);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            this.pnlCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseDown);
            this.pnlCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseMove);
            this.pnlCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseUp);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToolbar.AutoScroll = true;
            this.pnlToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.pnlToolbar.Controls.Add(this.groupBoxTools);
            this.pnlToolbar.Controls.Add(this.groupBoxAlgorithm);
            this.pnlToolbar.Controls.Add(this.groupBoxColors);
            this.pnlToolbar.Controls.Add(this.groupBoxActions);
            this.pnlToolbar.Location = new System.Drawing.Point(768, 12);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(220, 500);
            this.pnlToolbar.TabIndex = 1;
            // 
            // groupBoxTools
            // 
            this.groupBoxTools.Controls.Add(this.btnSetClipRect);
            this.groupBoxTools.Controls.Add(this.btnDrawLine);
            this.groupBoxTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTools.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTools.Name = "groupBoxTools";
            this.groupBoxTools.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxTools.Size = new System.Drawing.Size(200, 100);
            this.groupBoxTools.TabIndex = 0;
            this.groupBoxTools.TabStop = false;
            this.groupBoxTools.Text = "Herramientas";
            // 
            // btnSetClipRect
            // 
            this.btnSetClipRect.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSetClipRect.Location = new System.Drawing.Point(10, 62);
            this.btnSetClipRect.Name = "btnSetClipRect";
            this.btnSetClipRect.Size = new System.Drawing.Size(180, 25);
            this.btnSetClipRect.TabIndex = 1;
            this.btnSetClipRect.Text = "Definir Rectángulo de Recorte";
            this.btnSetClipRect.UseVisualStyleBackColor = true;
            this.btnSetClipRect.Click += new System.EventHandler(this.btnSetClipRect_Click);
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDrawLine.Location = new System.Drawing.Point(10, 37);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(180, 25);
            this.btnDrawLine.TabIndex = 0;
            this.btnDrawLine.Text = "Dibujar Línea";
            this.btnDrawLine.UseVisualStyleBackColor = true;
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // groupBoxAlgorithm
            // 
            this.groupBoxAlgorithm.Controls.Add(this.cmbVariant);
            this.groupBoxAlgorithm.Controls.Add(this.lblVariant);
            this.groupBoxAlgorithm.Controls.Add(this.btnClipLines);
            this.groupBoxAlgorithm.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAlgorithm.Location = new System.Drawing.Point(0, 100);
            this.groupBoxAlgorithm.Name = "groupBoxAlgorithm";
            this.groupBoxAlgorithm.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxAlgorithm.Size = new System.Drawing.Size(200, 120);
            this.groupBoxAlgorithm.TabIndex = 1;
            this.groupBoxAlgorithm.TabStop = false;
            this.groupBoxAlgorithm.Text = "Algoritmo";
            // 
            // cmbVariant
            // 
            this.cmbVariant.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariant.FormattingEnabled = true;
            this.cmbVariant.Items.AddRange(new object[] {
            "Cohen-Sutherland",
            "Liang-Barsky (no implementado)"});
            this.cmbVariant.Location = new System.Drawing.Point(10, 38);
            this.cmbVariant.Name = "cmbVariant";
            this.cmbVariant.Size = new System.Drawing.Size(180, 21);
            this.cmbVariant.TabIndex = 2;
            this.cmbVariant.SelectedIndex = 0;
            this.cmbVariant.SelectedIndexChanged += new System.EventHandler(this.cmbVariant_SelectedIndexChanged);
            // 
            // lblVariant
            // 
            this.lblVariant.AutoSize = true;
            this.lblVariant.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVariant.Location = new System.Drawing.Point(10, 25);
            this.lblVariant.Name = "lblVariant";
            this.lblVariant.Size = new System.Drawing.Size(107, 13);
            this.lblVariant.TabIndex = 3;
            this.lblVariant.Text = "Variante del Algoritmo:";
            // 
            // btnClipLines
            // 
            this.btnClipLines.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClipLines.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClipLines.ForeColor = System.Drawing.Color.White;
            this.btnClipLines.Location = new System.Drawing.Point(10, 59);
            this.btnClipLines.Name = "btnClipLines";
            this.btnClipLines.Size = new System.Drawing.Size(180, 30);
            this.btnClipLines.TabIndex = 0;
            this.btnClipLines.Text = "Realizar Recorte";
            this.btnClipLines.UseVisualStyleBackColor = false;
            this.btnClipLines.Click += new System.EventHandler(this.btnClipLines_Click);
            // 
            // groupBoxColors
            // 
            this.groupBoxColors.Controls.Add(this.lblClipColor);
            this.groupBoxColors.Controls.Add(this.pnlClipColor);
            this.groupBoxColors.Controls.Add(this.btnSelectClipColor);
            this.groupBoxColors.Controls.Add(this.lblLineColor);
            this.groupBoxColors.Controls.Add(this.pnlLineColor);
            this.groupBoxColors.Controls.Add(this.btnSelectLineColor);
            this.groupBoxColors.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxColors.Location = new System.Drawing.Point(0, 220);
            this.groupBoxColors.Name = "groupBoxColors";
            this.groupBoxColors.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxColors.Size = new System.Drawing.Size(200, 140);
            this.groupBoxColors.TabIndex = 2;
            this.groupBoxColors.TabStop = false;
            this.groupBoxColors.Text = "Colores";
            // 
            // lblLineColor
            // 
            this.lblLineColor.AutoSize = true;
            this.lblLineColor.Location = new System.Drawing.Point(10, 25);
            this.lblLineColor.Name = "lblLineColor";
            this.lblLineColor.Size = new System.Drawing.Size(78, 13);
            this.lblLineColor.TabIndex = 0;
            this.lblLineColor.Text = "Color de Línea:";
            // 
            // pnlLineColor
            // 
            this.pnlLineColor.BackColor = System.Drawing.Color.Black;
            this.pnlLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLineColor.Location = new System.Drawing.Point(10, 38);
            this.pnlLineColor.Name = "pnlLineColor";
            this.pnlLineColor.Size = new System.Drawing.Size(40, 30);
            this.pnlLineColor.TabIndex = 1;
            // 
            // btnSelectLineColor
            // 
            this.btnSelectLineColor.Location = new System.Drawing.Point(55, 38);
            this.btnSelectLineColor.Name = "btnSelectLineColor";
            this.btnSelectLineColor.Size = new System.Drawing.Size(135, 30);
            this.btnSelectLineColor.TabIndex = 2;
            this.btnSelectLineColor.Text = "Seleccionar Color";
            this.btnSelectLineColor.UseVisualStyleBackColor = true;
            this.btnSelectLineColor.Click += new System.EventHandler(this.btnSelectLineColor_Click);
            // 
            // lblClipColor
            // 
            this.lblClipColor.AutoSize = true;
            this.lblClipColor.Location = new System.Drawing.Point(10, 75);
            this.lblClipColor.Name = "lblClipColor";
            this.lblClipColor.Size = new System.Drawing.Size(88, 13);
            this.lblClipColor.TabIndex = 3;
            this.lblClipColor.Text = "Color de Recorte:";
            // 
            // pnlClipColor
            // 
            this.pnlClipColor.BackColor = System.Drawing.Color.Blue;
            this.pnlClipColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClipColor.Location = new System.Drawing.Point(10, 88);
            this.pnlClipColor.Name = "pnlClipColor";
            this.pnlClipColor.Size = new System.Drawing.Size(40, 30);
            this.pnlClipColor.TabIndex = 4;
            // 
            // btnSelectClipColor
            // 
            this.btnSelectClipColor.Location = new System.Drawing.Point(55, 88);
            this.btnSelectClipColor.Name = "btnSelectClipColor";
            this.btnSelectClipColor.Size = new System.Drawing.Size(135, 30);
            this.btnSelectClipColor.TabIndex = 5;
            this.btnSelectClipColor.Text = "Seleccionar Color";
            this.btnSelectClipColor.UseVisualStyleBackColor = true;
            this.btnSelectClipColor.Click += new System.EventHandler(this.btnSelectClipColor_Click);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.btnUndo);
            this.groupBoxActions.Controls.Add(this.btnClear);
            this.groupBoxActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxActions.Location = new System.Drawing.Point(0, 360);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxActions.Size = new System.Drawing.Size(200, 100);
            this.groupBoxActions.TabIndex = 3;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Acciones";
            // 
            // btnUndo
            // 
            this.btnUndo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUndo.Location = new System.Drawing.Point(10, 37);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(180, 25);
            this.btnUndo.TabIndex = 1;
            this.btnUndo.Text = "Deshacer (Ctrl+Z)";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(10, 62);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(180, 25);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Limpiar Canvas";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblInstruction
            // 
            this.lblInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstruction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInstruction.Location = new System.Drawing.Point(12, 518);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(976, 23);
            this.lblInstruction.TabIndex = 2;
            this.lblInstruction.Text = "Selecciona una herramienta para comenzar";
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 543);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(25, 17);
            this.lblStatus.Text = "OK";
            // 
            // FrmCohenSutherland
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 565);
            this.Controls.Add(this.pnlCanvas);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.statusStrip);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "FrmCohenSutherland";
            this.Text = "Recorte de Líneas - Cohen-Sutherland";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCohenSutherland_KeyDown);
            this.pnlToolbar.ResumeLayout(false);
            this.groupBoxTools.ResumeLayout(false);
            this.groupBoxAlgorithm.ResumeLayout(false);
            this.groupBoxAlgorithm.PerformLayout();
            this.groupBoxColors.ResumeLayout(false);
            this.groupBoxColors.PerformLayout();
            this.groupBoxActions.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.GroupBox groupBoxTools;
        private System.Windows.Forms.Button btnSetClipRect;
        private System.Windows.Forms.Button btnDrawLine;
        private System.Windows.Forms.GroupBox groupBoxAlgorithm;
        private System.Windows.Forms.ComboBox cmbVariant;
        private System.Windows.Forms.Label lblVariant;
        private System.Windows.Forms.Button btnClipLines;
        private System.Windows.Forms.GroupBox groupBoxColors;
        private System.Windows.Forms.Label lblLineColor;
        private System.Windows.Forms.Panel pnlLineColor;
        private System.Windows.Forms.Button btnSelectLineColor;
        private System.Windows.Forms.Label lblClipColor;
        private System.Windows.Forms.Panel pnlClipColor;
        private System.Windows.Forms.Button btnSelectClipColor;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}