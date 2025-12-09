namespace FormPartenerAndChild
{
    partial class FrmHome
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lineasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ddaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bresenhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.puntoMedioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.figurasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circuloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.círculoPuntoMedioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.círculoAlgoritmoParamétricoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.círculoBresenhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rellenoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floodFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanLineFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boundaryFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteDeLíneasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cohenSutherlandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liangBarskyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cyrusBeckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteDePolígonosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sutherlandHodgmanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weilerAthertonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greinerHormannToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineasToolStripMenuItem,
            this.figurasToolStripMenuItem,
            this.rellenoToolStripMenuItem,
            this.recorteDeLíneasToolStripMenuItem,
            this.recorteDePolígonosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lineasToolStripMenuItem
            // 
            this.lineasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddaToolStripMenuItem,
            this.bresenhamToolStripMenuItem,
            this.puntoMedioToolStripMenuItem});
            this.lineasToolStripMenuItem.Name = "lineasToolStripMenuItem";
            this.lineasToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.lineasToolStripMenuItem.Text = "Líneas";
            // 
            // ddaToolStripMenuItem
            // 
            this.ddaToolStripMenuItem.Name = "ddaToolStripMenuItem";
            this.ddaToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.ddaToolStripMenuItem.Text = "DDA";
            this.ddaToolStripMenuItem.Click += new System.EventHandler(this.ddaToolStripMenuItem_Click);
            // 
            // bresenhamToolStripMenuItem
            // 
            this.bresenhamToolStripMenuItem.Name = "bresenhamToolStripMenuItem";
            this.bresenhamToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.bresenhamToolStripMenuItem.Text = "Bresenham";
            this.bresenhamToolStripMenuItem.Click += new System.EventHandler(this.bresenhamToolStripMenuItem_Click);
            // 
            // puntoMedioToolStripMenuItem
            // 
            this.puntoMedioToolStripMenuItem.Name = "puntoMedioToolStripMenuItem";
            this.puntoMedioToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.puntoMedioToolStripMenuItem.Text = "Punto Medio";
            this.puntoMedioToolStripMenuItem.Click += new System.EventHandler(this.puntoMedioToolStripMenuItem_Click);
            // 
            // figurasToolStripMenuItem
            // 
            this.figurasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.circuloToolStripMenuItem,
            this.círculoPuntoMedioToolStripMenuItem,
            this.círculoAlgoritmoParamétricoToolStripMenuItem,
            this.círculoBresenhamToolStripMenuItem});
            this.figurasToolStripMenuItem.Name = "figurasToolStripMenuItem";
            this.figurasToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.figurasToolStripMenuItem.Text = "Figuras";
            // 
            // circuloToolStripMenuItem
            // 
            this.circuloToolStripMenuItem.Name = "circuloToolStripMenuItem";
            this.circuloToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.circuloToolStripMenuItem.Text = "Circulo";
            this.circuloToolStripMenuItem.Click += new System.EventHandler(this.circuloToolStripMenuItem_Click);
            // 
            // círculoPuntoMedioToolStripMenuItem
            // 
            this.círculoPuntoMedioToolStripMenuItem.Name = "círculoPuntoMedioToolStripMenuItem";
            this.círculoPuntoMedioToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.círculoPuntoMedioToolStripMenuItem.Text = "Círculo - Punto Medio";
            this.círculoPuntoMedioToolStripMenuItem.Click += new System.EventHandler(this.círculoPuntoMedioToolStripMenuItem_Click);
            // 
            // círculoAlgoritmoParamétricoToolStripMenuItem
            // 
            this.círculoAlgoritmoParamétricoToolStripMenuItem.Name = "círculoAlgoritmoParamétricoToolStripMenuItem";
            this.círculoAlgoritmoParamétricoToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.círculoAlgoritmoParamétricoToolStripMenuItem.Text = "Círculo - Algoritmo Paramétrico";
            this.círculoAlgoritmoParamétricoToolStripMenuItem.Click += new System.EventHandler(this.círculoAlgoritmoParamétricoToolStripMenuItem_Click);
            // 
            // círculoBresenhamToolStripMenuItem
            // 
            this.círculoBresenhamToolStripMenuItem.Name = "círculoBresenhamToolStripMenuItem";
            this.círculoBresenhamToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.círculoBresenhamToolStripMenuItem.Text = "Círculo - Bresenham";
            this.círculoBresenhamToolStripMenuItem.Click += new System.EventHandler(this.círculoBresenhamToolStripMenuItem_Click);
            // 
            // rellenoToolStripMenuItem
            // 
            this.rellenoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.floodFillToolStripMenuItem,
            this.scanLineFillToolStripMenuItem,
            this.boundaryFillToolStripMenuItem});
            this.rellenoToolStripMenuItem.Name = "rellenoToolStripMenuItem";
            this.rellenoToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.rellenoToolStripMenuItem.Text = "Relleno";
            // 
            // floodFillToolStripMenuItem
            // 
            this.floodFillToolStripMenuItem.Name = "floodFillToolStripMenuItem";
            this.floodFillToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.floodFillToolStripMenuItem.Text = "FloodFill";
            this.floodFillToolStripMenuItem.Click += new System.EventHandler(this.floodFillToolStripMenuItem_Click);
            // 
            // scanLineFillToolStripMenuItem
            // 
            this.scanLineFillToolStripMenuItem.Name = "scanLineFillToolStripMenuItem";
            this.scanLineFillToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.scanLineFillToolStripMenuItem.Text = "Scan Line Fill";
            this.scanLineFillToolStripMenuItem.Click += new System.EventHandler(this.scanLineFillToolStripMenuItem_Click);
            // 
            // boundaryFillToolStripMenuItem
            // 
            this.boundaryFillToolStripMenuItem.Name = "boundaryFillToolStripMenuItem";
            this.boundaryFillToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.boundaryFillToolStripMenuItem.Text = "Boundary Fill";
            this.boundaryFillToolStripMenuItem.Click += new System.EventHandler(this.boundaryFillToolStripMenuItem_Click);
            // 
            // recorteDeLíneasToolStripMenuItem
            // 
            this.recorteDeLíneasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cohenSutherlandToolStripMenuItem,
            this.liangBarskyToolStripMenuItem,
            this.cyrusBeckToolStripMenuItem});
            this.recorteDeLíneasToolStripMenuItem.Name = "recorteDeLíneasToolStripMenuItem";
            this.recorteDeLíneasToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.recorteDeLíneasToolStripMenuItem.Text = "Recorte de Líneas";
            // 
            // cohenSutherlandToolStripMenuItem
            // 
            this.cohenSutherlandToolStripMenuItem.Name = "cohenSutherlandToolStripMenuItem";
            this.cohenSutherlandToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cohenSutherlandToolStripMenuItem.Text = "Cohen - Sutherland";
            this.cohenSutherlandToolStripMenuItem.Click += new System.EventHandler(this.cohenSutherlandToolStripMenuItem_Click);
            // 
            // liangBarskyToolStripMenuItem
            // 
            this.liangBarskyToolStripMenuItem.Name = "liangBarskyToolStripMenuItem";
            this.liangBarskyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.liangBarskyToolStripMenuItem.Text = "Liang - Barsky";
            this.liangBarskyToolStripMenuItem.Click += new System.EventHandler(this.liangBarskyToolStripMenuItem_Click);
            // 
            // cyrusBeckToolStripMenuItem
            // 
            this.cyrusBeckToolStripMenuItem.Name = "cyrusBeckToolStripMenuItem";
            this.cyrusBeckToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cyrusBeckToolStripMenuItem.Text = "Cyrus - Beck";
            this.cyrusBeckToolStripMenuItem.Click += new System.EventHandler(this.cyrusBeckToolStripMenuItem_Click);
            // 
            // recorteDePolígonosToolStripMenuItem
            // 
            this.recorteDePolígonosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sutherlandHodgmanToolStripMenuItem,
            this.weilerAthertonToolStripMenuItem,
            this.greinerHormannToolStripMenuItem});
            this.recorteDePolígonosToolStripMenuItem.Name = "recorteDePolígonosToolStripMenuItem";
            this.recorteDePolígonosToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.recorteDePolígonosToolStripMenuItem.Text = "Recorte de Polígonos";
            // 
            // sutherlandHodgmanToolStripMenuItem
            // 
            this.sutherlandHodgmanToolStripMenuItem.Name = "sutherlandHodgmanToolStripMenuItem";
            this.sutherlandHodgmanToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.sutherlandHodgmanToolStripMenuItem.Text = "Sutherland - Hodgman";
            this.sutherlandHodgmanToolStripMenuItem.Click += new System.EventHandler(this.sutherlandHodgmanToolStripMenuItem_Click);
            // 
            // weilerAthertonToolStripMenuItem
            // 
            this.weilerAthertonToolStripMenuItem.Name = "weilerAthertonToolStripMenuItem";
            this.weilerAthertonToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.weilerAthertonToolStripMenuItem.Text = "Weiler - Atherton";
            this.weilerAthertonToolStripMenuItem.Click += new System.EventHandler(this.weilerAthertonToolStripMenuItem_Click);
            // 
            // greinerHormannToolStripMenuItem
            // 
            this.greinerHormannToolStripMenuItem.Name = "greinerHormannToolStripMenuItem";
            this.greinerHormannToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.greinerHormannToolStripMenuItem.Text = "Greiner - Hormann";
            this.greinerHormannToolStripMenuItem.Click += new System.EventHandler(this.greinerHormannToolStripMenuItem_Click);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmHome";
            this.Text = "FrmHome";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lineasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ddaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bresenhamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem puntoMedioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem figurasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circuloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem círculoPuntoMedioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem círculoAlgoritmoParamétricoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem círculoBresenhamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rellenoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floodFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanLineFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boundaryFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteDeLíneasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cohenSutherlandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liangBarskyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cyrusBeckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteDePolígonosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sutherlandHodgmanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weilerAthertonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greinerHormannToolStripMenuItem;
    }
}