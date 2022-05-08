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


namespace CheqySystem.Edit
{
    public partial class EditSec : Form
    {
        Model1 db = new Model1();

        public EditSec()
        {
            InitializeComponent();

            var ser = (from p in db.SectorsInfoes
                       select new
                       {
                           sectorId = p.sectorId,
                           sectorName = p.sectorName,
                           sectorAddress = p.sectorAddress,
                           sectorNote = p.sectorNote
                       }).ToList();
            dataGridView1.DataSource = ser.ToList();
            dataGridView1.Columns[0].HeaderText = "ر.م";
            dataGridView1.Columns[1].HeaderText = "الاسم";
            dataGridView1.Columns[2].HeaderText = "العنوان";
            dataGridView1.Columns[3].HeaderText = "ملاحظات";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bind = new SectorsInfo
            {
                sectorId= int.Parse(textBox1.Text),
                sectorName = string.IsNullOrEmpty(txtname.Text) ? "" : txtname.Text,
                sectorAddress = string.IsNullOrEmpty(txtaddress.Text) ? "" : txtaddress.Text,
                sectorNote = string.IsNullOrEmpty(txtaddress.Text) ? "" : txtaddress.Text
            };
            db.SectorsInfoes.AddOrUpdate(bind);
            db.SaveChanges();
            MessageBox.Show("تم التعديل");
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

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            var deletedemply = (from c in db.SectorsInfoes
                                where c.sectorId == id
                                select c).Single();
            db.SectorsInfoes.Remove(deletedemply);
            db.SaveChanges();
            MessageBox.Show("تم الحذف");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtname.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtaddress.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtnote.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}
