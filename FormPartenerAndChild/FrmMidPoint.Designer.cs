namespace FormPartenerAndChild
{
    partial class FrmMidPoint
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
            this.lblX0 = new System.Windows.Forms.Label();
            this.txtX0 = new System.Windows.Forms.TextBox();
            this.lblY0 = new System.Windows.Forms.Label();
            this.txtY0 = new System.Windows.Forms.TextBox();
            this.lblX1 = new System.Windows.Forms.Label();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.lblY1 = new System.Windows.Forms.Label();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.trkZoom = new System.Windows.Forms.TrackBar();
            this.lblZoom = new System.Windows.Forms.Label();
            this.lstPixels = new System.Windows.Forms.ListBox();
            this.lblPixelCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanvas.Location = new System.Drawing.Point(12, 12);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(600, 400);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            // 
            // lblX0
            // 
            this.lblX0.AutoSize = true;
            this.lblX0.Location = new System.Drawing.Point(12, 425);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(24, 13);
            this.lblX0.TabIndex = 1;
            this.lblX0.Text = "X0:";
            // 
            // txtX0
            // 
            this.txtX0.Location = new System.Drawing.Point(42, 422);
            this.txtX0.Name = "txtX0";
            this.txtX0.Size = new System.Drawing.Size(60, 20);
            this.txtX0.TabIndex = 2;
            this.txtX0.Text = "-100";
            // 
            // lblY0
            // 
            this.lblY0.AutoSize = true;
            this.lblY0.Location = new System.Drawing.Point(110, 425);
            this.lblY0.Name = "lblY0";
            this.lblY0.Size = new System.Drawing.Size(24, 13);
            this.lblY0.TabIndex = 3;
            this.lblY0.Text = "Y0:";
            // 
            // txtY0
            // 
            this.txtY0.Location = new System.Drawing.Point(140, 422);
            this.txtY0.Name = "txtY0";
            this.txtY0.Size = new System.Drawing.Size(60, 20);
            this.txtY0.TabIndex = 4;
            this.txtY0.Text = "0";
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.Location = new System.Drawing.Point(208, 425);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(24, 13);
            this.lblX1.TabIndex = 5;
            this.lblX1.Text = "X1:";
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(238, 422);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(60, 20);
            this.txtX1.TabIndex = 6;
            this.txtX1.Text = "100";
            // 
            // lblY1
            // 
            this.lblY1.AutoSize = true;
            this.lblY1.Location = new System.Drawing.Point(306, 425);
            this.lblY1.Name = "lblY1";
            this.lblY1.Size = new System.Drawing.Size(24, 13);
            this.lblY1.TabIndex = 7;
            this.lblY1.Text = "Y1:";
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(336, 422);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(60, 20);
            this.txtY1.TabIndex = 8;
            this.txtY1.Text = "0";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(410, 420);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 9;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(491, 420);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // trkZoom
            // 
            this.trkZoom.Location = new System.Drawing.Point(572, 412);
            this.trkZoom.Maximum = 400;
            this.trkZoom.Minimum = 10;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Size = new System.Drawing.Size(200, 45);
            this.trkZoom.TabIndex = 11;
            this.trkZoom.TickFrequency = 10;
            this.trkZoom.Value = 100;
            this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(572, 445);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(63, 13);
            this.lblZoom.TabIndex = 12;
            this.lblZoom.Text = "Zoom: 100%";
            // 
            // lstPixels
            // 
            this.lstPixels.FormattingEnabled = true;
            this.lstPixels.HorizontalScrollbar = true;
            this.lstPixels.Location = new System.Drawing.Point(628, 12);
            this.lstPixels.Name = "lstPixels";
            this.lstPixels.Size = new System.Drawing.Size(160, 407);
            this.lstPixels.TabIndex = 13;
            // 
            // lblPixelCount
            // 
            this.lblPixelCount.AutoSize = true;
            this.lblPixelCount.Location = new System.Drawing.Point(628, 425);
            this.lblPixelCount.Name = "lblPixelCount";
            this.lblPixelCount.Size = new System.Drawing.Size(45, 13);
            this.lblPixelCount.TabIndex = 14;
            this.lblPixelCount.Text = "Pixels: 0";
            // 
            // FrmMidPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.lblPixelCount);
            this.Controls.Add(this.lstPixels);
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.trkZoom);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.txtY1);
            this.Controls.Add(this.lblY1);
            this.Controls.Add(this.txtX1);
            this.Controls.Add(this.lblX1);
            this.Controls.Add(this.txtY0);
            this.Controls.Add(this.lblY0);
            this.Controls.Add(this.txtX0);
            this.Controls.Add(this.lblX0);
            this.Controls.Add(this.pnlCanvas);
            this.Name = "FrmMidPoint";
            this.Text = "Mid Point Line";
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.TextBox txtX0;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.TextBox txtY0;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.Label lblY1;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TrackBar trkZoom;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.ListBox lstPixels;
        private System.Windows.Forms.Label lblPixelCount;
    }
}