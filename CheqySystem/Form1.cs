using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheqySystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void اضافةToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Main.AddSec frm = new Main.AddSec();
            frm.Show();
        }

        private void تعديلToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Edit.EditSec frm = new Edit.EditSec();
            frm.ShowDialog();
        }

        private void عرضToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowAll.SecShow frm = new ShowAll.SecShow();
            frm.ShowDialog();
        }

        private void اضافةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.AddBank frm = new Main.AddBank();
            frm.ShowDialog();

        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit.EditBank frm = new Edit.EditBank();
            frm.ShowDialog();

        }

        private void عرضToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ShowAll.BanShow frm = new ShowAll.BanShow();
            frm.ShowDialog();
        }

        private void صرفالصكToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.chequ_report frm = new Reports.chequ_report();
            frm.ShowDialog();

        }
    }
}
