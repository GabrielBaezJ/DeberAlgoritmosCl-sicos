namespace FormPartenerAndChild
{
    partial class FrmScanlineFill
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.lblRule = new System.Windows.Forms.Label();
            this.cmbRule = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnSelectColor = new System.Windows.Forms.Button();
            this.pnlSelectedColor = new System.Windows.Forms.Panel();
            this.lblVertices = new System.Windows.Forms.Label();
            this.txtVertexX = new System.Windows.Forms.TextBox();
            this.txtVertexY = new System.Windows.Forms.TextBox();
            this.btnAddVertex = new System.Windows.Forms.Button();
            this.btnClearVertices = new System.Windows.Forms.Button();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnClearCanvas = new System.Windows.Forms.Button();
            this.lstVertices = new System.Windows.Forms.ListBox();
            this.lstSegments = new System.Windows.Forms.ListBox();
            this.lblSegments = new System.Windows.Forms.Label();
            this.trkZoom = new System.Windows.Forms.TrackBar();
            this.lblZoom = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.grpControls.SuspendLayout();
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
            this.pnlCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseClick);
            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.lblRule);
            this.grpControls.Controls.Add(this.cmbRule);
            this.grpControls.Controls.Add(this.lblColor);
            this.grpControls.Controls.Add(this.btnSelectColor);
            this.grpControls.Controls.Add(this.pnlSelectedColor);
            this.grpControls.Controls.Add(this.lblVertices);
            this.grpControls.Controls.Add(this.txtVertexX);
            this.grpControls.Controls.Add(this.txtVertexY);
            this.grpControls.Controls.Add(this.btnAddVertex);
            this.grpControls.Controls.Add(this.btnClearVertices);
            this.grpControls.Controls.Add(this.btnFill);
            this.grpControls.Controls.Add(this.btnClearCanvas);
            this.grpControls.Controls.Add(this.lstVertices);
            this.grpControls.Controls.Add(this.lblSegments);
            this.grpControls.Controls.Add(this.lstSegments);
            this.grpControls.Controls.Add(this.trkZoom);
            this.grpControls.Controls.Add(this.lblZoom);
            this.grpControls.Location = new System.Drawing.Point(620, 12);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(260, 400);
            this.grpControls.TabIndex = 1;
            this.grpControls.TabStop = false;
            this.grpControls.Text = "Controls";
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Location = new System.Drawing.Point(6, 16);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(61, 13);
            this.lblRule.TabIndex = 0;
            this.lblRule.Text = "Fill Rule:";
            // 
            // cmbRule
            // 
            this.cmbRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule.Items.AddRange(new object[] { "Even-Odd", "Non-Zero" });
            this.cmbRule.Location = new System.Drawing.Point(80, 13);
            this.cmbRule.Name = "cmbRule";
            this.cmbRule.Size = new System.Drawing.Size(170, 21);
            this.cmbRule.TabIndex = 1;
            this.cmbRule.SelectedIndex = 0;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(6, 46);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(40, 13);
            this.lblColor.TabIndex = 2;
            this.lblColor.Text = "Color:";
            // 
            // btnSelectColor
            // 
            this.btnSelectColor.Location = new System.Drawing.Point(80, 41);
            this.btnSelectColor.Name = "btnSelectColor";
            this.btnSelectColor.Size = new System.Drawing.Size(80, 23);
            this.btnSelectColor.TabIndex = 3;
            this.btnSelectColor.Text = "Select";
            this.btnSelectColor.UseVisualStyleBackColor = true;
            this.btnSelectColor.Click += new System.EventHandler(this.btnSelectColor_Click);
            // 
            // pnlSelectedColor
            // 
            this.pnlSelectedColor.BackColor = System.Drawing.Color.Red;
            this.pnlSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelectedColor.Location = new System.Drawing.Point(166, 41);
            this.pnlSelectedColor.Name = "pnlSelectedColor";
            this.pnlSelectedColor.Size = new System.Drawing.Size(84, 23);
            this.pnlSelectedColor.TabIndex = 4;
            // 
            // lblVertices
            // 
            this.lblVertices.AutoSize = true;
            this.lblVertices.Location = new System.Drawing.Point(6, 80);
            this.lblVertices.Name = "lblVertices";
            this.lblVertices.Size = new System.Drawing.Size(48, 13);
            this.lblVertices.TabIndex = 5;
            this.lblVertices.Text = "Vertices:";
            // 
            // txtVertexX
            // 
            this.txtVertexX.Location = new System.Drawing.Point(9, 100);
            this.txtVertexX.Name = "txtVertexX";
            this.txtVertexX.Size = new System.Drawing.Size(60, 20);
            this.txtVertexX.TabIndex = 6;
            this.txtVertexX.Text = "0";
            // 
            // txtVertexY
            // 
            this.txtVertexY.Location = new System.Drawing.Point(75, 100);
            this.txtVertexY.Name = "txtVertexY";
            this.txtVertexY.Size = new System.Drawing.Size(60, 20);
            this.txtVertexY.TabIndex = 7;
            this.txtVertexY.Text = "0";
            // 
            // btnAddVertex
            // 
            this.btnAddVertex.Location = new System.Drawing.Point(141, 98);
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(50, 23);
            this.btnAddVertex.TabIndex = 8;
            this.btnAddVertex.Text = "Add";
            this.btnAddVertex.UseVisualStyleBackColor = true;
            this.btnAddVertex.Click += new System.EventHandler(this.btnAddVertex_Click);
            // 
            // btnClearVertices
            // 
            this.btnClearVertices.Location = new System.Drawing.Point(197, 98);
            this.btnClearVertices.Name = "btnClearVertices";
            this.btnClearVertices.Size = new System.Drawing.Size(53, 23);
            this.btnClearVertices.TabIndex = 9;
            this.btnClearVertices.Text = "Clear";
            this.btnClearVertices.UseVisualStyleBackColor = true;
            this.btnClearVertices.Click += new System.EventHandler(this.btnClearVertices_Click);
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(9, 130);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(80, 23);
            this.btnFill.TabIndex = 10;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnClearCanvas
            // 
            this.btnClearCanvas.Location = new System.Drawing.Point(95, 130);
            this.btnClearCanvas.Name = "btnClearCanvas";
            this.btnClearCanvas.Size = new System.Drawing.Size(80, 23);
            this.btnClearCanvas.TabIndex = 11;
            this.btnClearCanvas.Text = "Clear Canvas";
            this.btnClearCanvas.UseVisualStyleBackColor = true;
            this.btnClearCanvas.Click += new System.EventHandler(this.btnClearCanvas_Click);
            // 
            // lstVertices
            // 
            this.lstVertices.FormattingEnabled = true;
            this.lstVertices.HorizontalScrollbar = true;
            this.lstVertices.Location = new System.Drawing.Point(9, 160);
            this.lstVertices.Name = "lstVertices";
            this.lstVertices.Size = new System.Drawing.Size(241, 95);
            this.lstVertices.TabIndex = 12;
            // 
            // lstSegments
            // 
            this.lstSegments.FormattingEnabled = true;
            this.lstSegments.HorizontalScrollbar = true;
            this.lstSegments.Location = new System.Drawing.Point(9, 270);
            this.lstSegments.Name = "lstSegments";
            this.lstSegments.Size = new System.Drawing.Size(241, 95);
            this.lstSegments.TabIndex = 13;
            // 
            // lblSegments
            // 
            this.lblSegments.AutoSize = true;
            this.lblSegments.Location = new System.Drawing.Point(6, 254);
            this.lblSegments.Name = "lblSegments";
            this.lblSegments.Size = new System.Drawing.Size(53, 13);
            this.lblSegments.TabIndex = 14;
            this.lblSegments.Text = "Segments:";
            // 
            // trkZoom
            // 
            this.trkZoom.Location = new System.Drawing.Point(9, 370);
            this.trkZoom.Maximum = 400;
            this.trkZoom.Minimum = 10;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Size = new System.Drawing.Size(200, 45);
            this.trkZoom.TabIndex = 15;
            this.trkZoom.TickFrequency = 10;
            this.trkZoom.Value = 100;
            this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(215, 375);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(63, 13);
            this.lblZoom.TabIndex = 16;
            this.lblZoom.Text = "Zoom: 100%";
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.Red;
            // 
            // FrmScanlineFill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 424);
            this.Controls.Add(this.grpControls);
            this.Controls.Add(this.pnlCanvas);
            this.Name = "FrmScanlineFill";
            this.Text = "Scanline Fill";
            this.grpControls.ResumeLayout(false);
            this.grpControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.ComboBox cmbRule;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnSelectColor;
        private System.Windows.Forms.Panel pnlSelectedColor;
        private System.Windows.Forms.Label lblVertices;
        private System.Windows.Forms.TextBox txtVertexX;
        private System.Windows.Forms.TextBox txtVertexY;
        private System.Windows.Forms.Button btnAddVertex;
        private System.Windows.Forms.Button btnClearVertices;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnClearCanvas;
        private System.Windows.Forms.ListBox lstVertices;
        private System.Windows.Forms.ListBox lstSegments;
        private System.Windows.Forms.Label lblSegments;
        private System.Windows.Forms.TrackBar trkZoom;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}