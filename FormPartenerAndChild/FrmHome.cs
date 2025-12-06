using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormPartenerAndChild
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void ddaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDDA frm = new FrmDDA();
            frm.MdiParent = this;
            frm.Show();
        }

        private void circuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCircle frm = new FrmCircle();
            frm.MdiParent = this;
            frm.Show();
        }

        private void bresenhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBresenham frm = new FrmBresenham();
            frm.MdiParent = this;
            frm.Show();
        }

        private void círculoPuntoMedioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMidpointCircle frm = new frmMidpointCircle();
            frm.MdiParent = this;
            frm.Show();
        }

        private void círculoAlgoritmoParamétricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCircleParametric frm = new FrmCircleParametric();
            frm.MdiParent = this;
            frm.Show();
        }

        private void círculoBresenhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBresenhamCircle frm = new FrmBresenhamCircle();
            frm.MdiParent = this;
            frm.Show();
        }

        private void puntoMedioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMidPoint frm = new FrmMidPoint();
            frm.MdiParent = this;
            frm.Show();
        }

        private void floodFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFloodFill frm = new FrmFloodFill();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
