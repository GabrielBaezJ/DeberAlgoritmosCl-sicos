namespace FormPartenerAndChild
{
    partial class FrmGreinerHormann
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.RadioButton rbDrawSubject;
        private System.Windows.Forms.RadioButton rbDrawClip;
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
            this.rbDrawSubject = new System.Windows.Forms.RadioButton();
            this.rbDrawClip = new System.Windows.Forms.RadioButton();
            this.btnClosePolygon = new System.Windows.Forms.Button();
            this.btnClip = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // panelTop
            this.panelTop.Controls.Add(this.rbDrawSubject);
            this.panelTop.Controls.Add(this.rbDrawClip);
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
            // rbDrawSubject
            this.rbDrawSubject.AutoSize = true;
            this.rbDrawSubject.Location = new System.Drawing.Point(8, 18);
            this.rbDrawSubject.Name = "rbDrawSubject";
            this.rbDrawSubject.Size = new System.Drawing.Size(112, 17);
            this.rbDrawSubject.TabIndex = 0;
            this.rbDrawSubject.TabStop = true;
            this.rbDrawSubject.Text = "Dibujar subject";
            this.rbDrawSubject.UseVisualStyleBackColor = true;
            this.rbDrawSubject.CheckedChanged += new System.EventHandler(this.rbDrawSubject_CheckedChanged);
            // rbDrawClip
            this.rbDrawClip.AutoSize = true;
            this.rbDrawClip.Location = new System.Drawing.Point(132, 18);
            this.rbDrawClip.Name = "rbDrawClip";
            this.rbDrawClip.Size = new System.Drawing.Size(92, 17);
            this.rbDrawClip.TabIndex = 1;
            this.rbDrawClip.TabStop = true;
            this.rbDrawClip.Text = "Dibujar clip";
            this.rbDrawClip.UseVisualStyleBackColor = true;
            this.rbDrawClip.CheckedChanged += new System.EventHandler(this.rbDrawClip_CheckedChanged);
            // btnClosePolygon
            this.btnClosePolygon.Location = new System.Drawing.Point(232, 10);
            this.btnClosePolygon.Name = "btnClosePolygon";
            this.btnClosePolygon.Size = new System.Drawing.Size(100, 32);
            this.btnClosePolygon.TabIndex = 2;
            this.btnClosePolygon.Text = "Cerrar polígono";
            this.btnClosePolygon.UseVisualStyleBackColor = true;
            this.btnClosePolygon.Click += new System.EventHandler(this.btnClosePolygon_Click);
            // btnClip
            this.btnClip.Location = new System.Drawing.Point(340, 10);
            this.btnClip.Name = "btnClip";
            this.btnClip.Size = new System.Drawing.Size(84, 32);
            this.btnClip.TabIndex = 3;
            this.btnClip.Text = "Recortar";
            this.btnClip.UseVisualStyleBackColor = true;
            this.btnClip.Click += new System.EventHandler(this.btnClip_Click);
            // btnUndo
            this.btnUndo.Location = new System.Drawing.Point(432, 10);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(84, 32);
            this.btnUndo.TabIndex = 4;
            this.btnUndo.Text = "Deshacer";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // btnClear
            this.btnClear.Location = new System.Drawing.Point(524, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 32);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(620, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Listo";
            // pnlCanvas
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
            // FrmGreinerHormann
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlCanvas);
            this.Controls.Add(this.panelTop);
            this.Name = "FrmGreinerHormann";
            this.Text = "Greiner-Hormann - Recorte de polígonos";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}