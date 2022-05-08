using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Migrations;

namespace CheqySystem.Main
{
    public partial class AddSec : Form
    {
        Model1 db = new Model1();

        public AddSec()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bind = new SectorsInfo
            {
                sectorName = string.IsNullOrEmpty(txtname.Text) ? "" : txtname.Text,
                sectorAddress = string.IsNullOrEmpty(txtaddress.Text) ? "" : txtaddress.Text,
                sectorNote = string.IsNullOrEmpty(txtaddress.Text) ? "" : txtaddress.Text
            };
            db.SectorsInfoes.Add(bind);
            db.SaveChanges();
            MessageBox.Show("تم الحفظ");
            ClearTextBoxes();
        }
        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtnote_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
