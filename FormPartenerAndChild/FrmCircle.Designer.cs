namespace FormPartenerAndChild
{
    partial class FrmCircle
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
            this.lblRadius = new System.Windows.Forms.Label();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblCenterX = new System.Windows.Forms.Label();
            this.txtCenterX = new System.Windows.Forms.TextBox();
            this.lblCenterY = new System.Windows.Forms.Label();
            this.txtCenterY = new System.Windows.Forms.TextBox();
            this.lblPixelCount = new System.Windows.Forms.Label();
            this.lstPixels = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            
            // pnlCanvas
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanvas.Location = new System.Drawing.Point(12, 12);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(776, 380);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            
            // lblRadius
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new System.Drawing.Point(12, 405);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(47, 13);
            this.lblRadius.TabIndex = 1;
            this.lblRadius.Text = "Radius:";
            
            // txtRadius
            this.txtRadius.Location = new System.Drawing.Point(65, 402);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(100, 20);
            this.txtRadius.TabIndex = 2;
            this.txtRadius.Text = "100";
            
            // btnDraw
            this.btnDraw.Location = new System.Drawing.Point(171, 402);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 3;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            
            // btnClear
            this.btnClear.Location = new System.Drawing.Point(252, 402);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            
            // lblCenterX
            this.lblCenterX.AutoSize = true;
            this.lblCenterX.Location = new System.Drawing.Point(333, 405);
            this.lblCenterX.Name = "lblCenterX";
            this.lblCenterX.Size = new System.Drawing.Size(48, 13);
            this.lblCenterX.TabIndex = 5;
            this.lblCenterX.Text = "Center X:";
            
            // txtCenterX
            this.txtCenterX.Location = new System.Drawing.Point(386, 402);
            this.txtCenterX.Name = "txtCenterX";
            this.txtCenterX.Size = new System.Drawing.Size(80, 20);
            this.txtCenterX.TabIndex = 6;
            
            // lblCenterY
            this.lblCenterY.AutoSize = true;
            this.lblCenterY.Location = new System.Drawing.Point(480, 405);
            this.lblCenterY.Name = "lblCenterY";
            this.lblCenterY.Size = new System.Drawing.Size(48, 13);
            this.lblCenterY.TabIndex = 7;
            this.lblCenterY.Text = "Center Y:";
            
            // txtCenterY
            this.txtCenterY.Location = new System.Drawing.Point(533, 402);
            this.txtCenterY.Name = "txtCenterY";
            this.txtCenterY.Size = new System.Drawing.Size(80, 20);
            this.txtCenterY.TabIndex = 8;
            
            // lblPixelCount
            this.lblPixelCount.AutoSize = true;
            this.lblPixelCount.Location = new System.Drawing.Point(620, 405);
            this.lblPixelCount.Name = "lblPixelCount";
            this.lblPixelCount.Size = new System.Drawing.Size(45, 13);
            this.lblPixelCount.TabIndex = 9;
            this.lblPixelCount.Text = "Pixels: 0";
            
            // lstPixels
            this.lstPixels.FormattingEnabled = true;
            this.lstPixels.HorizontalScrollbar = true;
            this.lstPixels.Location = new System.Drawing.Point(12, 430);
            this.lstPixels.Name = "lstPixels";
            this.lstPixels.Size = new System.Drawing.Size(776, 82);
            this.lstPixels.TabIndex = 10;
            
            // FrmCircle
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.lstPixels);
            this.Controls.Add(this.lblPixelCount);
            this.Controls.Add(this.txtCenterY);
            this.Controls.Add(this.lblCenterY);
            this.Controls.Add(this.txtCenterX);
            this.Controls.Add(this.lblCenterX);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.txtRadius);
            this.Controls.Add(this.lblRadius);
            this.Controls.Add(this.pnlCanvas);
            this.Name = "FrmCircle";
            this.Text = "Círculo - Algoritmo Punto Medio";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblCenterX;
        private System.Windows.Forms.TextBox txtCenterX;
        private System.Windows.Forms.Label lblCenterY;
        private System.Windows.Forms.TextBox txtCenterY;
        private System.Windows.Forms.Label lblPixelCount;
        private System.Windows.Forms.ListBox lstPixels;
    }
}