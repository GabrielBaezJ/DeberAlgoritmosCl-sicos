namespace FormPartenerAndChild
{
    partial class FrmBoundaryFill
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
            this.groupBoxShapeTools = new System.Windows.Forms.GroupBox();
            this.btnDrawRectangle = new System.Windows.Forms.Button();
            this.btnDrawCircle = new System.Windows.Forms.Button();
            this.btnDrawLine = new System.Windows.Forms.Button();
            this.groupBoxFillTools = new System.Windows.Forms.GroupBox();
            this.lblVariant = new System.Windows.Forms.Label();
            this.cmbVariant = new System.Windows.Forms.ComboBox();
            this.btnBoundaryFill = new System.Windows.Forms.Button();
            this.groupBoxColors = new System.Windows.Forms.GroupBox();
            this.lblBoundaryColor = new System.Windows.Forms.Label();
            this.pnlBoundaryColor = new System.Windows.Forms.Panel();
            this.btnSelectBoundaryColor = new System.Windows.Forms.Button();
            this.lblFillColor = new System.Windows.Forms.Label();
            this.pnlFillColor = new System.Windows.Forms.Panel();
            this.btnSelectFillColor = new System.Windows.Forms.Button();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pnlToolbar.SuspendLayout();
            this.groupBoxShapeTools.SuspendLayout();
            this.groupBoxFillTools.SuspendLayout();
            this.groupBoxColors.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            
            // pnlCanvas
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
            this.pnlCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseClick);
            this.pnlCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseDown);
            this.pnlCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseMove);
            this.pnlCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseUp);
            
            // pnlToolbar
            this.pnlToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToolbar.AutoScroll = true;
            this.pnlToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.pnlToolbar.Controls.Add(this.groupBoxShapeTools);
            this.pnlToolbar.Controls.Add(this.groupBoxFillTools);
            this.pnlToolbar.Controls.Add(this.groupBoxColors);
            this.pnlToolbar.Controls.Add(this.groupBoxActions);
            this.pnlToolbar.Location = new System.Drawing.Point(768, 12);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(220, 500);
            this.pnlToolbar.TabIndex = 1;
            
            // groupBoxShapeTools
            this.groupBoxShapeTools.Controls.Add(this.btnDrawRectangle);
            this.groupBoxShapeTools.Controls.Add(this.btnDrawCircle);
            this.groupBoxShapeTools.Controls.Add(this.btnDrawLine);
            this.groupBoxShapeTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxShapeTools.Location = new System.Drawing.Point(0, 0);
            this.groupBoxShapeTools.Name = "groupBoxShapeTools";
            this.groupBoxShapeTools.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxShapeTools.Size = new System.Drawing.Size(200, 130);
            this.groupBoxShapeTools.TabIndex = 0;
            this.groupBoxShapeTools.TabStop = false;
            this.groupBoxShapeTools.Text = "Herramientas de Forma";
            
            // btnDrawRectangle
            this.btnDrawRectangle.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDrawRectangle.Location = new System.Drawing.Point(10, 62);
            this.btnDrawRectangle.Name = "btnDrawRectangle";
            this.btnDrawRectangle.Size = new System.Drawing.Size(180, 25);
            this.btnDrawRectangle.TabIndex = 1;
            this.btnDrawRectangle.Text = "Dibujar Rectángulo";
            this.btnDrawRectangle.UseVisualStyleBackColor = true;
            this.btnDrawRectangle.Click += new System.EventHandler(this.btnDrawRectangle_Click);
            
            // btnDrawCircle
            this.btnDrawCircle.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDrawCircle.Location = new System.Drawing.Point(10, 37);
            this.btnDrawCircle.Name = "btnDrawCircle";
            this.btnDrawCircle.Size = new System.Drawing.Size(180, 25);
            this.btnDrawCircle.TabIndex = 0;
            this.btnDrawCircle.Text = "Dibujar Círculo";
            this.btnDrawCircle.UseVisualStyleBackColor = true;
            this.btnDrawCircle.Click += new System.EventHandler(this.btnDrawCircle_Click);
            
            // btnDrawLine
            this.btnDrawLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDrawLine.Location = new System.Drawing.Point(10, 87);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(180, 25);
            this.btnDrawLine.TabIndex = 2;
            this.btnDrawLine.Text = "Dibujar Línea";
            this.btnDrawLine.UseVisualStyleBackColor = true;
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            
            // groupBoxFillTools
            this.groupBoxFillTools.Controls.Add(this.lblVariant);
            this.groupBoxFillTools.Controls.Add(this.cmbVariant);
            this.groupBoxFillTools.Controls.Add(this.btnBoundaryFill);
            this.groupBoxFillTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFillTools.Location = new System.Drawing.Point(0, 130);
            this.groupBoxFillTools.Name = "groupBoxFillTools";
            this.groupBoxFillTools.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxFillTools.Size = new System.Drawing.Size(200, 120);
            this.groupBoxFillTools.TabIndex = 1;
            this.groupBoxFillTools.TabStop = false;
            this.groupBoxFillTools.Text = "Herramientas de Relleno";
            
            // lblVariant
            this.lblVariant.AutoSize = true;
            this.lblVariant.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVariant.Location = new System.Drawing.Point(10, 25);
            this.lblVariant.Name = "lblVariant";
            this.lblVariant.Size = new System.Drawing.Size(100, 13);
            this.lblVariant.TabIndex = 2;
            this.lblVariant.Text = "Variante del Algoritmo:";
            
            // cmbVariant
            this.cmbVariant.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariant.FormattingEnabled = true;
            this.cmbVariant.Items.AddRange(new object[] {
                "4-Conectado",
                "8-Conectado",
                "Optimizado (8-Conectado)"});
            this.cmbVariant.Location = new System.Drawing.Point(10, 38);
            this.cmbVariant.Name = "cmbVariant";
            this.cmbVariant.Size = new System.Drawing.Size(180, 21);
            this.cmbVariant.TabIndex = 1;
            this.cmbVariant.SelectedIndex = 0;
            this.cmbVariant.SelectedIndexChanged += new System.EventHandler(this.cmbVariant_SelectedIndexChanged);
            
            // btnBoundaryFill
            this.btnBoundaryFill.BackColor = System.Drawing.Color.LimeGreen;
            this.btnBoundaryFill.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBoundaryFill.ForeColor = System.Drawing.Color.White;
            this.btnBoundaryFill.Location = new System.Drawing.Point(10, 59);
            this.btnBoundaryFill.Name = "btnBoundaryFill";
            this.btnBoundaryFill.Size = new System.Drawing.Size(180, 30);
            this.btnBoundaryFill.TabIndex = 0;
            this.btnBoundaryFill.Text = "Rellenar Frontera";
            this.btnBoundaryFill.UseVisualStyleBackColor = false;
            this.btnBoundaryFill.Click += new System.EventHandler(this.btnBoundaryFill_Click);
            
            // groupBoxColors
            this.groupBoxColors.Controls.Add(this.lblBoundaryColor);
            this.groupBoxColors.Controls.Add(this.pnlBoundaryColor);
            this.groupBoxColors.Controls.Add(this.btnSelectBoundaryColor);
            this.groupBoxColors.Controls.Add(this.lblFillColor);
            this.groupBoxColors.Controls.Add(this.pnlFillColor);
            this.groupBoxColors.Controls.Add(this.btnSelectFillColor);
            this.groupBoxColors.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxColors.Location = new System.Drawing.Point(0, 250);
            this.groupBoxColors.Name = "groupBoxColors";
            this.groupBoxColors.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxColors.Size = new System.Drawing.Size(200, 140);
            this.groupBoxColors.TabIndex = 2;
            this.groupBoxColors.TabStop = false;
            this.groupBoxColors.Text = "Colores";
            
            // lblFillColor
            this.lblFillColor.AutoSize = true;
            this.lblFillColor.Location = new System.Drawing.Point(10, 25);
            this.lblFillColor.Name = "lblFillColor";
            this.lblFillColor.Size = new System.Drawing.Size(88, 13);
            this.lblFillColor.TabIndex = 0;
            this.lblFillColor.Text = "Color de Relleno:";
            
            // pnlFillColor
            this.pnlFillColor.BackColor = System.Drawing.Color.Red;
            this.pnlFillColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFillColor.Location = new System.Drawing.Point(10, 38);
            this.pnlFillColor.Name = "pnlFillColor";
            this.pnlFillColor.Size = new System.Drawing.Size(40, 30);
            this.pnlFillColor.TabIndex = 1;
            
            // btnSelectFillColor
            this.btnSelectFillColor.Location = new System.Drawing.Point(55, 38);
            this.btnSelectFillColor.Name = "btnSelectFillColor";
            this.btnSelectFillColor.Size = new System.Drawing.Size(135, 30);
            this.btnSelectFillColor.TabIndex = 2;
            this.btnSelectFillColor.Text = "Seleccionar Color";
            this.btnSelectFillColor.UseVisualStyleBackColor = true;
            this.btnSelectFillColor.Click += new System.EventHandler(this.btnSelectFillColor_Click);
            
            // lblBoundaryColor
            this.lblBoundaryColor.AutoSize = true;
            this.lblBoundaryColor.Location = new System.Drawing.Point(10, 75);
            this.lblBoundaryColor.Name = "lblBoundaryColor";
            this.lblBoundaryColor.Size = new System.Drawing.Size(108, 13);
            this.lblBoundaryColor.TabIndex = 3;
            this.lblBoundaryColor.Text = "Color de Frontera:";
            
            // pnlBoundaryColor
            this.pnlBoundaryColor.BackColor = System.Drawing.Color.Black;
            this.pnlBoundaryColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBoundaryColor.Location = new System.Drawing.Point(10, 88);
            this.pnlBoundaryColor.Name = "pnlBoundaryColor";
            this.pnlBoundaryColor.Size = new System.Drawing.Size(40, 30);
            this.pnlBoundaryColor.TabIndex = 4;
            
            // btnSelectBoundaryColor
            this.btnSelectBoundaryColor.Location = new System.Drawing.Point(55, 88);
            this.btnSelectBoundaryColor.Name = "btnSelectBoundaryColor";
            this.btnSelectBoundaryColor.Size = new System.Drawing.Size(135, 30);
            this.btnSelectBoundaryColor.TabIndex = 5;
            this.btnSelectBoundaryColor.Text = "Seleccionar Color";
            this.btnSelectBoundaryColor.UseVisualStyleBackColor = true;
            this.btnSelectBoundaryColor.Click += new System.EventHandler(this.btnSelectBoundaryColor_Click);
            
            // groupBoxActions
            this.groupBoxActions.Controls.Add(this.btnUndo);
            this.groupBoxActions.Controls.Add(this.btnClear);
            this.groupBoxActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxActions.Location = new System.Drawing.Point(0, 390);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxActions.Size = new System.Drawing.Size(200, 100);
            this.groupBoxActions.TabIndex = 3;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Acciones";
            
            // btnUndo
            this.btnUndo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUndo.Location = new System.Drawing.Point(10, 37);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(180, 25);
            this.btnUndo.TabIndex = 1;
            this.btnUndo.Text = "Deshacer (Ctrl+Z)";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            
            // btnClear
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
            
            // lblInstruction
            this.lblInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstruction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInstruction.Location = new System.Drawing.Point(12, 518);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(976, 23);
            this.lblInstruction.TabIndex = 2;
            this.lblInstruction.Text = "Selecciona una herramienta para comenzar";
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 543);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip.TabIndex = 3;
            
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(25, 17);
            this.lblStatus.Text = "OK";
            
            // FrmBoundaryFill
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 565);
            this.Controls.Add(this.pnlCanvas);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.statusStrip);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "FrmBoundaryFill";
            this.Text = "Algoritmo de Relleno de Fronteras (Boundary Fill)";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBoundaryFill_KeyDown);
            this.pnlToolbar.ResumeLayout(false);
            this.groupBoxShapeTools.ResumeLayout(false);
            this.groupBoxFillTools.ResumeLayout(false);
            this.groupBoxFillTools.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxShapeTools;
        private System.Windows.Forms.Button btnDrawRectangle;
        private System.Windows.Forms.Button btnDrawCircle;
        private System.Windows.Forms.Button btnDrawLine;
        private System.Windows.Forms.GroupBox groupBoxFillTools;
        private System.Windows.Forms.Button btnBoundaryFill;
        private System.Windows.Forms.GroupBox groupBoxColors;
        private System.Windows.Forms.Label lblFillColor;
        private System.Windows.Forms.Panel pnlFillColor;
        private System.Windows.Forms.Button btnSelectFillColor;
        private System.Windows.Forms.Label lblBoundaryColor;
        private System.Windows.Forms.Panel pnlBoundaryColor;
        private System.Windows.Forms.Button btnSelectBoundaryColor;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ComboBox cmbVariant;
        private System.Windows.Forms.Label lblVariant;
    }
}