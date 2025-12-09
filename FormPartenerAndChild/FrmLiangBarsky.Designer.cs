namespace FormPartenerAndChild
{
    partial class FrmLiangBarsky
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.RadioButton rbDrawLine;
        private System.Windows.Forms.RadioButton rbDrawRect;
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
            this.rbDrawRect = new System.Windows.Forms.RadioButton();
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
            this.panelTop.Controls.Add(this.rbDrawRect);
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
            // rbDrawRect
            // 
            this.rbDrawRect.AutoSize = true;
            this.rbDrawRect.Location = new System.Drawing.Point(110, 18);
            this.rbDrawRect.Name = "rbDrawRect";
            this.rbDrawRect.Size = new System.Drawing.Size(156, 17);
            this.rbDrawRect.TabIndex = 1;
            this.rbDrawRect.TabStop = true;
            this.rbDrawRect.Text = "Dibujar rect. (definir clip)";
            this.rbDrawRect.UseVisualStyleBackColor = true;
            this.rbDrawRect.CheckedChanged += new System.EventHandler(this.rbDrawRect_CheckedChanged);
            // 
            // btnClip
            // 
            this.btnClip.Location = new System.Drawing.Point(280, 10);
            this.btnClip.Name = "btnClip";
            this.btnClip.Size = new System.Drawing.Size(84, 32);
            this.btnClip.TabIndex = 2;
            this.btnClip.Text = "Recortar";
            this.btnClip.UseVisualStyleBackColor = true;
            this.btnClip.Click += new System.EventHandler(this.btnClip_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(372, 10);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(84, 32);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "Deshacer";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(464, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(560, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(34, 13);
            this.lblStatus.TabIndex = 5;
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
            // FrmLiangBarsky
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlCanvas);
            this.Controls.Add(this.panelTop);
            this.Name = "FrmLiangBarsky";
            this.Text = "Liang-Barsky - Recorte de líneas";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}