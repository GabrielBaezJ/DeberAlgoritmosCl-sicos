namespace FormPartenerAndChild
{
    partial class FrmCyrusBeck
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.RadioButton rbDrawLine;
        private System.Windows.Forms.RadioButton rbDrawPolygon;
        private System.Windows.Forms.Button btnClosePolygon;
        private System.Windows.Forms.Button btnClip;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Label lblStatus;

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
            this.panelTop = new System.Windows.Forms.Panel();
            this.rbDrawLine = new System.Windows.Forms.RadioButton();
            this.rbDrawPolygon = new System.Windows.Forms.RadioButton();
            this.btnClosePolygon = new System.Windows.Forms.Button();
            this.btnClip = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.rbDrawLine);
            this.panelTop.Controls.Add(this.rbDrawPolygon);
            this.panelTop.Controls.Add(this.btnClosePolygon);
            this.panelTop.Controls.Add(this.btnClip);
            this.panelTop.Controls.Add(this.btnUndo);
            this.panelTop.Controls.Add(this.btnClear);
            this.panelTop.Controls.Add(this.lblStatus);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 56);
            this.panelTop.TabIndex = 0;
            // 
            // rbDrawLine
            // 
            this.rbDrawLine.AutoSize = true;
            this.rbDrawLine.Location = new System.Drawing.Point(8, 18);
            this.rbDrawLine.Name = "rbDrawLine";
            this.rbDrawLine.Size = new System.Drawing.Size(88, 17);
            this.rbDrawLine.TabIndex = 0;
            this.rbDrawLine.TabStop = true;
            this.rbDrawLine.Text = "Dibujar línea";
            this.rbDrawLine.UseVisualStyleBackColor = true;
            this.rbDrawLine.CheckedChanged += new System.EventHandler(this.rbDrawLine_CheckedChanged);
            // 
            // rbDrawPolygon
            // 
            this.rbDrawPolygon.AutoSize = true;
            this.rbDrawPolygon.Location = new System.Drawing.Point(110, 18);
            this.rbDrawPolygon.Name = "rbDrawPolygon";
            this.rbDrawPolygon.Size = new System.Drawing.Size(116, 17);
            this.rbDrawPolygon.TabIndex = 1;
            this.rbDrawPolygon.TabStop = true;
            this.rbDrawPolygon.Text = "Dibujar polígono";
            this.rbDrawPolygon.UseVisualStyleBackColor = true;
            this.rbDrawPolygon.CheckedChanged += new System.EventHandler(this.rbDrawPolygon_CheckedChanged);
            // 
            // btnClosePolygon
            // 
            this.btnClosePolygon.Location = new System.Drawing.Point(236, 10);
            this.btnClosePolygon.Name = "btnClosePolygon";
            this.btnClosePolygon.Size = new System.Drawing.Size(100, 32);
            this.btnClosePolygon.TabIndex = 2;
            this.btnClosePolygon.Text = "Cerrar polígono";
            this.btnClosePolygon.UseVisualStyleBackColor = true;
            this.btnClosePolygon.Click += new System.EventHandler(this.btnClosePolygon_Click);
            // 
            // btnClip
            // 
            this.btnClip.Location = new System.Drawing.Point(344, 10);
            this.btnClip.Name = "btnClip";
            this.btnClip.Size = new System.Drawing.Size(84, 32);
            this.btnClip.TabIndex = 3;
            this.btnClip.Text = "Recortar";
            this.btnClip.UseVisualStyleBackColor = true;
            this.btnClip.Click += new System.EventHandler(this.btnClip_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(436, 10);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(84, 32);
            this.btnUndo.TabIndex = 4;
            this.btnUndo.Text = "Deshacer";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(528, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 32);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(620, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Listo";
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 56);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(800, 394);
            this.pnlCanvas.TabIndex = 1;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            this.pnlCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseDown);
            this.pnlCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseMove);
            this.pnlCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseUp);
            // 
            // FrmCyrusBeck
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlCanvas);
            this.Controls.Add(this.panelTop);
            this.Name = "FrmCyrusBeck";
            this.Text = "Cyrus-Beck - Recorte por polígono convexo";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}